using SharpDX;

namespace ZenithEngine.MIDI
{
    public struct ColorChange
    {
        public double Position { get; }
        public IMidiPlaybackTrack Track { get; }
        public byte Channel { get; }
        public Color4 Col1 { get; }
        public Color4 Col2 { get; }

        public ColorChange(double pos, IMidiPlaybackTrack track, byte channel, Color4 col1, Color4 col2)
        {
            Position = pos;
            Track = track;
            Channel = channel;
            Col1 = col1;
            Col2 = col2;
        }
    }
}
