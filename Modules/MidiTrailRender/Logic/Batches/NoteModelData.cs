using ZenithEngine.DXHelper;

namespace MidiTrailRender.Logic.Batches
{
    public class NoteModelData : DeviceInitiable
    {
        public NoteModelData(NoteBufferParts flat, NoteBufferParts cube, NoteBufferParts rounded)
        {
            Flat = init.Add(flat);
            Cube = init.Add(cube);
            Rounded = init.Add(rounded);
        }

        public NoteBufferParts Flat { get; }
        public NoteBufferParts Cube { get; }
        public NoteBufferParts Rounded { get; }
    }
}
