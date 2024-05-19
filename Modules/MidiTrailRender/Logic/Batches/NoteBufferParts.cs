using MIDITrailRender.Logic;
using ZenithEngine.DXHelper;

namespace MidiTrailRender.Logic.Batches
{
    public class NoteBufferParts : DeviceInitiable
    {
        public NoteBufferParts(ShapeBuffer<NoteInstance> body, ShapeBuffer<NoteInstance> cap)
        {
            Body = init.Add(body);
            Cap = init.Add(cap);
        }


        public NoteBufferParts(ShapeBuffer<NoteInstance> body)
        {
            Body = init.Add(body);
            Cap = null;
        }

        public bool HasCap => Cap != null;

        public ShapeBuffer<NoteInstance> Body { get; }
        public ShapeBuffer<NoteInstance> Cap { get; }
    }
}
