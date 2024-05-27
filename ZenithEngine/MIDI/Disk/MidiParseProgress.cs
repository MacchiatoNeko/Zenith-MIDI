namespace ZenithEngine.MIDI.Disk
{
    public readonly struct MidiParseProgress
    {
        public int Parsed { get; }
        public int Discovered { get; }
        public MidiParseStep Step { get; }

        public MidiParseProgress(int parsed, int discovered)
        {
            Step = MidiParseStep.Parse;
            Parsed = parsed;
            Discovered = discovered;
        }

        public MidiParseProgress(int discovered)
        {
            Step = MidiParseStep.Discover;
            Parsed = 0;
            Discovered = discovered;
        }
    }
}
