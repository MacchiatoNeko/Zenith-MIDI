using System.Collections.Generic;
using System.Linq;

namespace MidiTrailRender.Structs
{
    struct BoundingDimention
    {
        public BoundingDimention(IEnumerable<float> data)
        {
            Min = data.Min();
            Max = data.Max();
            Range = Max - Min;
            Middle = (Max + Min) / 2;
        }

        public float Min { get; }
        public float Max { get; }
        public float Range { get; }
        public float Middle { get; }
    }
}
