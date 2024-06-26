﻿using System;

namespace ZenithEngine.MIDI
{
    public abstract class MidiFile : IDisposable
    {
        public ushort PPQ { get; protected set; }
        public int TrackCount { get; protected set; }
        public long NoteCount { get; protected set; } = 0;
        public bool PushPlaybackEvents { get; set; } = false;
        public long TickLength { get; internal set; }
        public double SecondsLength { get; internal set; }
        public abstract MidiPlayback GetMidiPlayback(double startOffset, bool timeBased);
        public abstract MidiPlayback GetMidiPlayback(double startOffsetTicks, double startOffsetSeconds, bool timeBased);
        public abstract double StartTicksToSeconds(double startOffset, bool timeBased);
        public abstract void Dispose();
    }
}
