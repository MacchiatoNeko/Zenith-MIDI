using MidiTrailRender.Structs;
using ZenithEngine.ModuleUtil;

namespace MIDITrailRender.Logic
{
    public class KeyboardPhysics
    {
        public PhysicsKey[] Keys { get; }

        double lastMidiTime = double.NaN;

        public KeyboardPhysics()
        {
            Keys = new PhysicsKey[256];
            for (int k = 0; k < 256; k++) Keys[k] = new PhysicsKey(k);
        }

        public void UpdateFrom(KeyboardState state, double time)
        {
            if (double.IsNaN(lastMidiTime)) lastMidiTime = time;
            float timeScale = (float)(time - lastMidiTime) * 60;
            for (int k = 0; k < 256; k++)
            {
                Keys[k].Step(state, timeScale);
            }
            lastMidiTime = time;
        }
    }
}
