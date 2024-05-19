using MIDITrailRender.Logic;
using ZenithEngine.DXHelper;

namespace MidiTrailRender.Logic.Batches
{
    public class KeyModelDataEdge : DeviceInitiable
    {
        public KeyModelDataEdge(ModelBuffer<KeyVert>[] models)
        {
            Models = models;
            foreach (var m in Models) init.Add(m);
        }

        public ModelBuffer<KeyVert>[] Models { get; }

        public ModelBuffer<KeyVert> GetKey(int key) => Models[key % 12];
    }
}
