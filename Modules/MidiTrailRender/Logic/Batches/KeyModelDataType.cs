using ZenithEngine.DXHelper;

namespace MidiTrailRender.Logic.Batches
{
    public class KeyModelDataType : DeviceInitiable
    {
        public KeyModelDataType(KeyModelDataEdge normal, KeyModelDataEdge left, KeyModelDataEdge right)
        {
            Normal = init.Add(normal);
            Left = init.Add(left);
            Right = init.Add(right);
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
        }

        public KeyModelDataEdge Normal { get; }
        public KeyModelDataEdge Left { get; }
        public KeyModelDataEdge Right { get; }
    }
}
