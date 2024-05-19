using System;
using System.Collections.Generic;

namespace ZenithEngine.MIDI
{
    public interface IMidiTrack : IDisposable
    {
        long NoteCount { get; }
        long TickLength { get; }
        long LastNoteTick { get; }
        int ID { get; }
        IEnumerable<Tempo> TempoEvents { get; }
        IEnumerable<TimeSignature> TimesigEvents { get; }
        NoteColor[] InitialTrackColors { get; }
    }
}
