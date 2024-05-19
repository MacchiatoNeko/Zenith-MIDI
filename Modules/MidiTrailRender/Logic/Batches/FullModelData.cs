using ZenithEngine.DXHelper;

namespace MidiTrailRender.Logic.Batches
{
    public class FullModelData : DeviceInitiable
    {
        public FullModelData(NoteModelData notes, KeyModelData keys)
        {
            Notes = init.Add(notes);
            Keys = init.Add(keys);
        }

        public NoteModelData Notes { get; }
        public KeyModelData Keys { get; }
    }
}
