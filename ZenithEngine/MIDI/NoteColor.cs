using SharpDX;

namespace ZenithEngine.MIDI
{
    public class NoteColor
    {
        public Color4 Left { get; set; }
        public Color4 Right { get; set; }
        internal bool isDefault { get; set; } = true;

        public void Alter(Color4 left, Color4 right)
        {
            if (!isDefault) return;
            Left = left;
            Right = right;
        }

        public void Set(Color4 left, Color4 right)
        {
            isDefault = false;
            Left = left;
            Right = right;
        }
    }
}
