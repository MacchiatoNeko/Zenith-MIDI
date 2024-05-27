namespace ZenithEngine.MIDI
{
    public class TimeSignature : PositionedEvent
    {
        public TimeSignature(long pos, int numerator, int denominator) : base(pos)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public int Numerator { get; internal set; }
        public int Denominator { get; internal set; }
    }
}
