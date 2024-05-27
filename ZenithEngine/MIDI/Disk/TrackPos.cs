namespace ZenithEngine.MIDI.Disk
{
    struct TrackPos
    {
        public long start;
        public uint length;

        public TrackPos(long start, uint length)
        {
            this.start = start;
            this.length = length;
        }
    }
}
