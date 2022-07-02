using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace Rain.Engine.Texturing;

/// <summary> 
/// A class for managing and creating textures. 
/// </summary>
public class Texture : IDisposable
{
	/// <summary>
	/// The Texture's OpenGL handle for use with OpenGL functions.
	/// </summary>
	///
	/// <value>
	/// An integer representing the OpenGL Texture.
	/// </value>
	public int Handle { get; private set; }

	/// <summary>
	/// The texture unit of 16 possible units that this <c>Texture</c> occupies.
	/// </summary>
	///
	/// <value>
	/// A <c>TextureUnit</c> enum value.
	/// </value>
	public TextureUnit Unit { get; set; }

	/// <summary>
	/// A byte array representing the image.
	/// </summary>
	public byte[] Image { get; }

	/// <summary>
	/// The image's width.
	/// </summary>
	public int Width { get; }

	/// <summary>
	/// The images height.
	/// </summary>
	public int Height { get; }

	/// <summary>
	/// Whether or not image data has been attatched to this <c>Texture</c>.
	/// </summary>
	public bool IsEmpty { get; private set; }

	/// <summary>
	/// Creates an Empty <c>Texture</c>.
	/// </summary>
	public Texture()
	{
		Handle = -1;
		Unit = TextureUnit.None;
		IsEmpty = true;

		Image = Array.Empty<byte>();
		Width = 0;
		Height = 0;
	}

	/// <summary>
	/// Creates a new <c>Texture</c>.
	/// </summary>
	/// 
	/// <param name="imagePath">
	/// The image to make the new <c>Texture</c> from.
	/// </param>
	public Texture(string imagePath)
	{
		Handle = -1;
		Unit = TextureUnit.None;
		IsEmpty = false;

		using var image = SixLabors.ImageSharp.Image.Load<Rgb24>(imagePath);

		image.Mutate(x => x.Flip(FlipMode.Vertical));
		image.DangerousTryGetSinglePixelMemory(out var pixelMemory);

		var pixelSpan = pixelMemory.Span;
		var pixelBytes = new byte[image.Width * image.Height * 4];

		for (var i = 0; i < pixelSpan.Length; i++)
		{
			pixelBytes[i * 3] = pixelSpan[i].R;
			pixelBytes[i * 3 + 1] = pixelSpan[i].G;
			pixelBytes[i * 3 + 2] = pixelSpan[i].B;
		}

		Image = pixelBytes;
		Width = image.Width;
		Height = image.Height;
	}

	/// <summary>
	/// Reserves a Texture handle with OpenGL.
	/// </summary>
	public void ReserveHandle()
	{
		if (Handle != -1)
			throw new Exception("Texture has already reserved a handle.");

		Handle = GL.GenTexture();
	}

	/// <summary>
	/// Uploads this <c>Texture</c> to the GPU.
	/// </summary>
	///
	/// <param name="unit">
	/// The <c>TextureUnit</c> to occupy.
	/// </param>
	///
	/// <param name="uniform">
	/// The <c>Uniform</c> to upload the <c>Texture</c> to.
	/// </param>
	public void Upload(TextureUnit unit, Uniform uniform)
	{
		Unit = unit;
		Bind(bindEmpty: true);

		GL.TexParameter(
			TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);

		GL.TexParameter(
			TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

		GL.TexParameter(
			TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureFilter.NearestMipmapFiltered);

		GL.TexParameter(
			TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureFilter.Linear);

		uniform.SetToTexture(this);

		GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, Width, Height, 0, PixelFormat.Rgb,
					  PixelType.UnsignedByte, Image);
		GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

		IsEmpty = false;
	}

	/// <summary>
	/// Binds the <c>Texture</c> to its current <c>Texture.Unit</c>.
	/// </summary>
	///
	/// <param name="bindEmpty">
	/// Whether or not to allow binding an empty <c>Textture</c>.
	/// </param>
	public void Bind(bool bindEmpty = false)
	{
		if (!bindEmpty && IsEmpty)
			throw new Exception("Cannot bind empty Texture.");

		GL.ActiveTexture((OpenTK.Graphics.OpenGL.TextureUnit)Unit);
		GL.BindTexture(TextureTarget.Texture2D, Handle);
	}

	/// <summary>
	/// Unbinds any currently bound <c>Texture</c>.
	/// </summary>
	/// 
	/// <param name="bindEmpty">
	/// Whether or not to allow unbinding an from an empty <c>Textture</c>.
	/// </param>
	public void Unbind(bool unbindEmpty = false)
	{
		if (!unbindEmpty && IsEmpty)
			throw new Exception("Cannot unbind from empty Texture.");

		GL.ActiveTexture((OpenTK.Graphics.OpenGL.TextureUnit)TextureUnit.Unit0);
		GL.BindTexture(TextureTarget.Texture2D, 0);
	}

	#region IDisposable

	private bool disposed = false;

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposed)
			return;

		if (disposing)
		{
			Unbind();
			GL.DeleteTexture(Handle);
		}

		disposed = true;
	}

	~Texture() => Dispose(false);

	#endregion
}