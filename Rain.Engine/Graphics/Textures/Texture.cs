using System.Buffers;

using OpenTK.Graphics.OpenGL4;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace Rain.Engine.Graphics.Textures;

class Texture<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
	private readonly Image<TPixel> image;

	private readonly Memory<TPixel> memory;

	private readonly IMemoryOwner<TPixel> owner;

	public int Handle { get; }

	public TextureUnit TextureUnit { get; init; }

	public TextureWrapMode WrapMode { get; init; } = TextureWrapMode.Repeat;

	public TextureMinFilter DownScaleFilter { get; init; } = TextureMinFilter.Linear;

	public TextureMagFilter UpScaleFilter { get; init; } = TextureMagFilter.Linear;

	public Texture(string texturePath, TextureUnit textureUnit = TextureUnit.Texture0, bool doFlip = true)
	{
		TextureUnit = textureUnit;

		Handle = GL.GenTexture();
		image = Image.Load<TPixel>(texturePath);

		Use(TextureUnit);

		// Image is read from the top-left pixel by default, OpenGL reads from the bottom left, hence the need to verticaly
		// flip the image. If the image is already flipped set doFlip to false and this step will be skipped.
		if (doFlip)
			image.Mutate(x => x.Flip(FlipMode.Vertical));
		
		owner = ImageMemoryHelper.AllocateContiguousMemory<TPixel>(image.Height * image.Width);
		memory = ImageMemoryHelper.CopyImageToMemory(image, owner);

		GL.TexImage2D(TextureTarget.Texture2D, 
		0, 
		PixelInternalFormat.Rgba, // Using RGBA here kinda defeats the point of <TPixel>. Revisit later.
		image.Width, 
		image.Height, 
		0, 
		PixelFormat.Bgra, 
		PixelType.UnsignedByte, 
		image.Bytes().ToArray());

		SetTextureParameters();
		GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
	}

	public void Use(TextureUnit textureUnit)
	{
		GL.ActiveTexture(textureUnit);
		GL.BindTexture(TextureTarget.Texture2D, Handle);
	}

	private void SetTextureParameters()
	{
		GL.TextureParameter((int)TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)UpScaleFilter);
		GL.TextureParameter((int)TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)DownScaleFilter);
		GL.TextureParameter((int)TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)WrapMode); // X axis.
		GL.TextureParameter((int)TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)WrapMode); // Y axis.
	}
}