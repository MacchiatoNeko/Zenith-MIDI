namespace ZenithEngine.MIDI
{
    public abstract class PositionedEvent
    {
        protected PositionedEvent(double position)
        {
            Position = position;
        }

        public double Position { get; internal set; }
    }
}
