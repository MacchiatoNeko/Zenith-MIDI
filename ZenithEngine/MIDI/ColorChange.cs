using SharpDX;

namespace ZenithEngine.MIDI
{
    public class ColorChange : PositionedEvent
    {
        public ColorChange(double pos, IMidiPlaybackTrack track, byte channel, Color4 col1, Color4 col2) : base(pos)
        {
            Track = track;
            Channel = channel;
            Col1 = col1;
            Col2 = col2;
        }

        public Color4 Col1 { get; internal set; }
        public Color4 Col2 { get; internal set; }
        public byte Channel { get; internal set; }
        public IMidiPlaybackTrack Track { get; internal set; }
    }
}
