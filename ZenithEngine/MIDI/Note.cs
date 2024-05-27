namespace ZenithEngine.MIDI
{
    public class Note
    {
        public double Start { get; internal set; }
        public double End { get; internal set; }
        public bool HasEnded { get; internal set; }
        public byte Channel { get; internal set; }
        public byte Key { get; internal set; }
        public byte Vel { get; internal set; }
        public bool Delete { get; set; } = false;
        public object Meta { get; set; } = null;
        public int Track { get; internal set; }
        public NoteColor Color { get; internal set; }
    }
}
