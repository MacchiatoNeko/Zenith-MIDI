using ZenithEngine.DXHelper;

namespace MidiTrailRender.Logic.Batches
{
    public class KeyModelData : DeviceInitiable
    {
        public KeyModelData(KeyModelDataType sameWidth, KeyModelDataType differentWidth)
        {
            SameWidth = init.Add(sameWidth);
            DifferentWidth = init.Add(differentWidth);
        }

        public KeyModelDataType SameWidth { get; }
        public KeyModelDataType DifferentWidth { get; }
    }
}
