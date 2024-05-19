using MIDITrailRender.Logic;
using SharpDX;
using System;
using ZenithEngine.ModuleUtil;

namespace MidiTrailRender.Structs
{
    public struct PhysicsKey
    {
        public float Press { get; set; }
        public float Vel { get; set; }
        public bool Touching { get; set; }
        public Color4 LastLeftColor { get; set; }
        public Color4 LastRightColor { get; set; }
        public Color4 OriginalLeftColor { get; set; }
        public Color4 OriginalRightColor { get; set; }

        public int Key { get; }

        public PhysicsKey(int key)
        {
            Press = 0;
            Vel = 0;
            Touching = false;
            LastLeftColor = new Color4(0, 0, 0, 1);
            LastRightColor = new Color4(0, 0, 0, 1);
            Key = key;
            var black = KeyboardState.IsBlackKey(key);
            var original = black ? new Color4(0, 0, 0, 1) : new Color4(1, 1, 1, 1);
            OriginalLeftColor = original;
            OriginalRightColor = original;
        }

        static Color4 LerpCol(Color4 a, Color4 b, float fac)
        {
            return new Color4(
                Util.Lerp(a.Red, b.Red, fac),
                Util.Lerp(a.Green, b.Green, fac),
                Util.Lerp(a.Blue, b.Blue, fac),
                Util.Lerp(a.Alpha, b.Alpha, fac)
                );
        }

        public Color4 GetLeftCol(bool blend)
        {
            if (!blend) return Touching ? LastLeftColor : OriginalLeftColor;
            return LerpCol(OriginalLeftColor, LastLeftColor, Press);
        }

        public Color4 GetRightCol(bool blend)
        {
            if (!blend) return Touching ? LastRightColor : OriginalRightColor;
            return LerpCol(OriginalRightColor, LastRightColor, Press);
        }

        public void Step(KeyboardState state, float delta)
        {
            Touching = state.Pressed[Key];
            if (Touching)
            {
                LastLeftColor = state.Colors[Key].Left;
                LastRightColor = state.Colors[Key].Right;
            }
            if (Touching)
            {
                Vel += 1.0f * delta;
            }
            else
            {
                Vel += -Press / 1 * delta;
            }
            Vel *= (float)Math.Pow(0.5f, delta);
            Press += Vel * delta;

            float maxPress = 1;
            if (Press > maxPress)
            {
                Press = maxPress;
                Vel = 0;
            }
        }
    }
}
