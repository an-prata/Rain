using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace Rain.Engine.Texturing;

/// <summary> A class for managing and creating OpenGL textures. </summary>
public class Texture : IDisposable
{
	/// <summary> The Texture's OpenGL handle for use with OpenGL functions. </summary>
	/// <value> An integer representing the OpenGL Texture. </value>
	public int Handle { get; private set; }

	/// <summary> The texture unit of 16 possible units that this <c>Texture</c> occupies. </summary>
	/// <value> A <c>TextureUnit</c> enum value. </value>
	public TextureUnit Unit { get; set; }

	/// <summary> Whether or not image data has been attatched to this <c>Texture</c>. </summary>
	public bool IsEmpty { get; private set; }

	public Texture(ShaderProgram shaderProgram, TextureUnit unit, string glslName)
	{
		if (unit == TextureUnit.None)
			throw new Exception("Cannot create a Texture for TextureUnit.None.");
		
		Unit = unit;
		shaderProgram.Use();
		Handle = GL.GenTexture();

		GL.ActiveTexture((OpenTK.Graphics.OpenGL.TextureUnit)Unit);
		GL.BindTexture(TextureTarget.Texture2D, Handle);

		shaderProgram.GetUniformByName(glslName).SetToTexture(this);

		GL.TexParameter(
			TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);

		GL.TexParameter(
			TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

		GL.TexParameter(
			TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureFilter.NearestMipmapFiltered);
			
		GL.TexParameter(
			TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureFilter.Linear);

		IsEmpty = true;
	}

	/// <summary> Creates a new <c>Texture</c> object from a <c>TextureOptions</c> struct. </summary>
	/// <param name="options"> The <c>TextureOptions</c> to be used in construction. </param>
	/// <param name="shaderProgram"> The <c>ShaderProgram</c> that will use this <c>Texture</c>. </param>
	public Texture(TextureOptions options, ShaderProgram shaderProgram)
	{
		Unit = options.Unit;

		shaderProgram.Use();
		Handle = GL.GenTexture();

		GL.ActiveTexture((OpenTK.Graphics.OpenGL.TextureUnit)Unit);
		GL.BindTexture(TextureTarget.Texture2D, Handle);

		var image = SixLabors.ImageSharp.Image.Load<Rgb24>(options.ImagePath);
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

		shaderProgram.GetUniformByName(options.GlslName).SetToTexture(this);

		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)options.WrapMode);
		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)options.WrapMode);
		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)options.MinimizationFilter);
		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)options.MagnificationFilter);

		GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, image.Width, image.Height, 0, PixelFormat.Rgb,
					  PixelType.UnsignedByte, pixelBytes);
		GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

		IsEmpty = false;
	}

	private Texture()
	{
		// This should be an invalid handle as far as OpenGL is concerned.
		Handle = -1; 
		Unit = TextureUnit.None;
	}

	/// <summary> Returns an empty <c>Texture</c>. </summary>
	public static Texture Empty() => new();

	/// <summary>
	/// Reserves a Texture handle with OpenGL, can only be used with a <c>Texture</c> returned by <c>Texture.Empty()</c>.
	/// </summary>
	public void ReserveHandle()
	{
		if (Handle != -1)
			throw new Exception("Texture has already reserved a handle.");
		
		Handle = GL.GenTexture();
	}

	/// <summary> Loads image data to the <c>Texture</c>. </summary>
	/// <remarks> Works best with square bitmap images at standard resolutions. </remarks>
	/// <param name="imagePath"> The path to the image file. </param>
	public void LoadFromImage(string imagePath)
	{
		Bind();

		var image = SixLabors.ImageSharp.Image.Load<Rgb24>(imagePath);
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

		GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, image.Width, image.Height, 0, PixelFormat.Rgb,
					  PixelType.UnsignedByte, pixelBytes);
		GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

		IsEmpty = false;
	}

	/// <summary> Binds the <c>Texture</c> to its current <c>Texture.Unit</c>. </summary>
	public void Bind()
	{
		if (IsEmpty)
			throw new Exception("Cannot bind empty Texture.");

		GL.ActiveTexture((OpenTK.Graphics.OpenGL.TextureUnit)Unit);
		GL.BindTexture(TextureTarget.Texture2D, Handle);
	}

	/// <summary> Unbinds any currently bound <c>Texture</c>. </summary>
	public void Unbind()
	{
		if (IsEmpty)
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