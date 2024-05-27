namespace ZenithEngine.MIDI
{
    public class Tempo
    {
        public Tempo(long pos, int rawTempo)
        {
            this.pos = pos;
            this.rawTempo = rawTempo;
            realTempo = 60000000.0 / rawTempo;
        }

        public long pos { get; internal set; }
        public int rawTempo { get; internal set; }
        public double realTempo { get; internal set; }
    }
}
