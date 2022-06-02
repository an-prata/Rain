using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace Rain.Engine.Graphics.Textures;

public class CubeTexture : ITextureRenderable
{
	public int Faces { get; }

	public float[] TextureCooridinates { get; }

	public Texture<Rgba32>[] Textures { get; }
}