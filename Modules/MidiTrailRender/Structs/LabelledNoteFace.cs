using MidiTrailRender.Enums;
using ObjLoader.Loader.Data.Elements;

namespace MidiTrailRender.Structs
{
    struct LabelledNoteFace
    {
        public LabelledNoteFace(Face face, NoteFaceType type)
        {
            Face = face;
            Type = type;
        }

        public Face Face { get; }
        public NoteFaceType Type { get; }
    }
}
