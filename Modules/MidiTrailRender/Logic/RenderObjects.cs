using MIDITrailRender.Models;
using SharpDX;
using SharpDX.Direct3D11;

namespace MIDITrailRender.Logic
{
    public abstract class RenderObject
    {
        public RenderObject(BaseModel config, Vector3 pos, Matrix transform)
        {
            Config = config;
            Position = pos;
            Transform = transform;
        }

        public Vector3 Position { get; }
        public Matrix Transform { get; }
        public BaseModel Config { get; }

        public abstract void Render(DeviceContext context, Camera camera);
    }
}
