using SharpDX.Direct3D11;

namespace ZenithEngine.DXHelper
{
    public interface IRenderSurface
    {
        public Texture2D Texture { get; }
        public RenderTargetView RenderTarget { get; }
        public DepthStencilView RenderTargetDepth { get; }

        public int Width { get; }
        public int Height { get; }
    }
}
