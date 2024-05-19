using System;

namespace ZenithEngine.MIDI
{
    public interface IMidiPlaybackTrack : IDisposable
    {
        int ID { get; }
        bool Ended { get; }
        long NoteCount { get; }
        long ParseTimeTicks { get; }
        double ParseTimeSeconds { get; }
        NoteColor[] TrackColors { get; }

        MidiPlayback MidiPlayback { get; }

        void Step(long time);
    }
}
