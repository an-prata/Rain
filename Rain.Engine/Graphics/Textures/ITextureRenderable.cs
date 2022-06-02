using SixLabors.ImageSharp.PixelFormats;

namespace Rain.Engine.Graphics.Textures;

interface ITextureRenderable
{
	/// <summary>
	/// The number of faces/surfaces on the corrosponding IRenderable object.
	/// </summary>
	int Faces { get; }

	/// <summary>
	/// The 2D cooridinates describing the textures' position on each face.
	/// </summary>
	float[] TextureCooridinates { get; }

	/// <summary>
	/// The texture objects for each face.
	/// </summary>
	Texture<Rgba32>[] Textures { get; }
}