using SharpDX.Direct3D11;

namespace ZenithEngine.DXHelper
{
    public interface IBufferFlusher<T> : IDeviceInitiable
        where T : struct
    {
        public int Length { get; }
        public void FlushArray(DeviceContext context, T[] verts, int count);
    }
}
