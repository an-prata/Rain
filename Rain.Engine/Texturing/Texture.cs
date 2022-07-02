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
	public byte[] Image { get; private set; }

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
	/// Whether or not the <c>Texture</c>'s image data has been uploaded to the GPU.
	/// </summary>
	///
	/// <remarks>
	/// While <c>IsUploaded</c> is true, <c>Image</c> will always be an empty array.
	/// </remarks>
	public bool IsUploaded { get; private set; }

	private TextureOptions options;

	/// <summary>
	/// Creates an Empty <c>Texture</c>.
	/// </summary>
	public Texture()
	{
		Handle = -1;
		Unit = TextureUnit.None;
		IsEmpty = true;
		IsUploaded = false;

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
		IsUploaded = false;

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
		
		options = new TextureOptions
		{
			WrapMode = TextureWrapMode.Clamp,
			MagnificationFilter = TextureFilter.Linear,
			MinimizationFilter = TextureFilter.Linear
		};
	}

	/// <summary>
	/// Creates a new <c>Texture</c>.
	/// </summary>
	///
	/// <param name="imagePath">
	/// The image to make the new <c>Texture</c> from.
	/// </param>
	/// 
	/// <param name="options">
	/// The settings to use when minimizing, magnifying, and wrapping the <c>Texture</c>.
	/// </param>
	public Texture(string imagePath, TextureOptions options)
	{
		Handle = -1;
		Unit = TextureUnit.None;
		IsEmpty = false;
		IsUploaded = false;

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
		this.options = options;
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
	/// <remarks>
	/// <c>Upload()</c> may be called more than once on the same <c>Texture</c> without throwing any exceptions, however it
	/// will simply return if <c>IsUploaded</c> is true, and not upload any data to the GPU.
	/// </remarks>
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
		if (IsUploaded)
			return;

		Unit = unit;
		Bind(bindEmpty: true);
		uniform.SetToTexture(this);

		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)options.WrapMode);
		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)options.WrapMode);
		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)options.MagnificationFilter);
		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)options.MinimizationFilter);

		GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, Width, Height, 0, PixelFormat.Rgb,
					  PixelType.UnsignedByte, Image);

		GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

		Image = Array.Empty<byte>();

		IsEmpty = false;
		IsUploaded = true;
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