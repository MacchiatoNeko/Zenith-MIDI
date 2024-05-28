using System;
using System.Collections.Generic;
using System.Linq;

namespace ZenithEngine.MIDI.Disk
{
    class DiskMidiPlayback : MidiPlayback
    {
        public override double ParserPosition => TimeBased ? SecondsParsed * 1000 : TicksParsed;
        public override double PlayerPosition => TimeBased ? TimeSeconds * 1000 : TimeTicksFractional;
        public override double PlayerPositionSeconds => TimeSeconds;

        public double TimeSeconds { get; internal set; }
        public double TimeTicksFractional { get; internal set; }
        public long TimeTicks => (long)TimeTicksFractional;

        public long TicksParsed { get; internal set; }
        public double SecondsParsed { get; internal set; }

        DiskMidiFile midi;
        public override MidiFile Midi => midi;
        DiskMidiTrack[] tracks;
        public override IMidiPlaybackTrack[] Tracks => tracks;
        bool disposed = false;
        int remainingTracks = 0;
        readonly object parseLock = new();
        protected long lastNoteCount = 0;
        public override long LastIterateNoteCount => lastNoteCount;

        public DiskMidiPlayback(DiskMidiFile file, DiskReadProvider reader, double startDelay, bool timeBased, long? maxAllocation = null) : base(file, file.TempoEvents[0].rawTempo, timeBased)
        {
            midi = file;
            tracks = new DiskMidiTrack[file.TrackCount];
            int maxSize = 1024 * 8;

            if (maxAllocation != null)
            {
                while (maxSize > 100000)
                {
                    long sum = file.TrackPositions
                        .Select(t => (long)t.length)
                        .Select(l => l <= maxSize ? l : maxSize * 2)
                        .Sum();
                    if (sum < maxAllocation) break;
                    maxSize -= 1000;
                }
            }

            for (int i = 0; i < file.TrackCount; i++)
            {
                var trackReader = new BufferByteReader(reader, maxSize, midi.TrackPositions[i].start, midi.TrackPositions[i].length);
                tracks[i] = DiskMidiTrack.NewPlayerTrack(i, trackReader, this, midi.Tracks[i].InitialTrackColors);
            }

            ColorChanges = new FastList<ColorChange>();
            PlaybackEvents = new FastList<PlaybackEvent>();
            Tempo = midi.TempoEvents[0];
            TimeSignature = midi.TimeSignatureEvents[0];
            TimeSeconds = -startDelay;
            TimeTicksFractional = -startDelay / ParserTempoTickMultiplier;
        }

        public override bool ParseUpTo(double time)
        {
            if (ParserPosition > time || stopped) // Exit early if already past the desired time or if playback is stopped
            {
                return false;
            }

            lock (parseLock)
            {
                double timeLimit = time + 1;
                double tickIncrement = 1 * ParserTempoTickMultiplier;

                while (ParserPosition <= timeLimit && !stopped)
                {
                    TicksParsed++;
                    SecondsParsed += tickIncrement;
                    int activeTracks = 0;
                    unsafe
                    {
                        fixed (DiskMidiTrack* trackPtr = &tracks[0])
                        {
                            for (int i = 0; i < tracks.Length; i++)
                            {
                                var track = trackPtr[i];
                                if (!track.Ended)
                                {
                                    activeTracks++;
                                    track.Step(TicksParsed);
                                }
                            }
                        }
                    }
                    remainingTracks = activeTracks;

                    if (remainingTracks == 0) // Exit early if no active tracks are left
                    {
                        return false;
                    }
                }
                unsafe
                {
                    fixed (DiskMidiTrack* trackPtr = &tracks[0])
                    {
                        foreach (var track in tracks) // Check if any tracks are still active
                        {
                            if (!track.Ended)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }

        int tempoEventId = 0;
        int timesigEventId = 0;
        public override void AdvancePlaybackTo(double time)
        {
            var offset = time - TimeSeconds;
            if (offset < 0) return;

            var multiplier = ((double)Tempo.rawTempo / Midi.PPQ) / 1000000;
            while (
                tempoEventId < midi.TempoEvents.Length &&
                TimeTicksFractional + offset / multiplier > midi.TempoEvents[tempoEventId].pos
            )
            {
                Tempo = midi.TempoEvents[tempoEventId];
                tempoEventId++;
                var diff = Tempo.pos - TimeTicksFractional;
                if (diff * multiplier > offset || diff < 0)
                { }
                TimeTicksFractional += diff;
                offset -= diff * multiplier;
                TimeSeconds += diff * multiplier;
                multiplier = ((double)Tempo.rawTempo / Midi.PPQ) / 1000000;
            }
            TimeTicksFractional += offset / multiplier;
            TimeSeconds += offset;

            while (timesigEventId != midi.TimeSignatureEvents.Length &&
                midi.TimeSignatureEvents[timesigEventId].Position < TimeTicksFractional)
            {
                TimeSignature = midi.TimeSignatureEvents[timesigEventId++];
            }
            if (SecondsParsed < TimeSeconds) ParseUpTo(PlayerPosition);
            FlushColorEvents();
        }

        IEnumerable<Note> SingleNoteListFromSource(Func<IEnumerable<Note>> getNotes)
        {
            NotesKeysSeparated = false;
            var notes = getNotes();
            long nc = 0;
            foreach (var n in notes)
            {
                nc++;
                yield return n;
            }
            lastNoteCount = nc;
            CheckEnded();
        }

        public override IEnumerable<Note> IterateNotes()
        {
            return SingleNoteListFromSource(() =>
            {
                unsafe
                {
                    fixed (DiskMidiTrack* trackPtr = &tracks[0])
                    {
                        return IterateNotesList(NotesSingle);
                    }
                }
            });
        }

        public override IEnumerable<Note> IterateNotes(double bottomCutoffOffset, double topCutoffOffset)
        {
            return SingleNoteListFromSource(() => IterateNotesListWithCutoffs(NotesSingle, bottomCutoffOffset, topCutoffOffset));
        }

        public override IEnumerable<Note> IterateNotesCustomDelete()
        {
            return SingleNoteListFromSource(() => IterateNotesListWithCustomDelete(NotesSingle));
        }

        IEnumerable<Note>[] KeyedNoteListFromSource(Func<int, IEnumerable<Note>> getNotes)
        {
            NotesKeysSeparated = true;
            long nc = 0;
            return GenerateNotesListArrays(
                getNotes,
                n =>
                {
                    nc += n;
                    lastNoteCount = nc;
                    CheckEnded();
                });
        }

        public override IEnumerable<Note>[] IterateNotesKeyed()
        {
            return KeyedNoteListFromSource(key => IterateNotesList(NotesKeyed[key]));
        }

        public override IEnumerable<Note>[] IterateNotesKeyed(double bottomCutoffOffset, double topCutoffOffset)
        {
            return KeyedNoteListFromSource(key => IterateNotesListWithCutoffs(NotesKeyed[key], bottomCutoffOffset, topCutoffOffset));
        }

        public override IEnumerable<Note>[] IterateNotesCustomDeleteKeyed()
        {
            return KeyedNoteListFromSource(key => IterateNotesListWithCustomDelete(NotesKeyed[key]));
        }

        void CheckEnded()
        {
            if (remainingTracks == 0 && lastNoteCount == 0) Ended = true;
        }

        public override void Dispose()
        {
            if (disposed) return;
            ForceStop();
            disposed = true;
            NotesKeyed = null;
            NotesSingle = null;
            ColorChanges.Unlink();
            PlaybackEvents.Unlink();
            ColorChanges = null;
            PlaybackEvents = null;
            unsafe
            {
                fixed (DiskMidiTrack* trackPtr = &tracks[0])
                {
                    for (int i = 0; i < tracks.Length; i++)
                    {
                        var t = trackPtr[i];
                        t.Dispose();
                    }
                }
            }
        }
    }
}
