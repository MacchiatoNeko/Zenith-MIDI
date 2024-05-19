using SharpDX.Direct3D11;

namespace ZenithEngine.DXHelper
{
    public interface ITextureResource
    {
        public ShaderResourceView TextureResource { get; }
        public Texture2D Texture { get; }

        public int Width { get; }
        public int Height { get; }
    }

    public interface IDepthTextureResource
    {
        public ShaderResourceView DepthTextureResource { get; }
        public Texture2D DepthTexture { get; }

        public int Width { get; }
        public int Height { get; }
    }
}
