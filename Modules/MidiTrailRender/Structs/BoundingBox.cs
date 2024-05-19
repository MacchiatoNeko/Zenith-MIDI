using System.Collections.Generic;
using System.Linq;

namespace MidiTrailRender.Structs
{
    struct BoundingBox
    {
        public BoundingBox(IEnumerable<BasicVert> data)
        {
            X = new BoundingDimention(data.Select(d => d.Pos.X));
            Y = new BoundingDimention(data.Select(d => d.Pos.Y));
            Z = new BoundingDimention(data.Select(d => d.Pos.Z));
        }

        public BoundingDimention X { get; }
        public BoundingDimention Y { get; }
        public BoundingDimention Z { get; }
    }
}
