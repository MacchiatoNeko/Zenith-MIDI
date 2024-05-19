using SharpDX;

namespace MidiTrailRender.Structs
{
    struct BasicVert
    {
        public BasicVert(Vector3 pos, Vector3 normal)
        {
            Pos = pos;
            Normal = normal;
        }

        public Vector3 Pos;
        public Vector3 Normal;
    }
}
