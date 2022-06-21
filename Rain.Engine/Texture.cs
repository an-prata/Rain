using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace Rain.Engine;

/// <summary> A class for managing and creating OpenGL textures. </summary>
public class Texture
{
	/// <summary> The Texture's OpenGL handle for use with OpenGL functions. </summary>
	/// <value> An integer representing the OpenGL Texture. </value>
	public int Handle { get; }

	/// <summary> The texture unit of 16 possible units that this <c>Texture</c> occupies. </summary>
	/// <value> A <c>TextureUnit</c> enum value. </value>
	public TextureUnit Unit { get; set; }

	/// <summary> Creates a new <c>Texture</c> object from a <c>TextureOptions</c> struct. </summary>
	/// <param name="options"> The <c>TextureOptions</c> to be used in construction. </param>
	/// <param name="shaderProgram"> The <c>ShaderProgram</c> that will use this <c>Texture</c>. </param>
	public Texture(TextureOptions options, ref ShaderProgram shaderProgram)
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

		UploadTexture(pixelBytes, image.Width, image.Height);
		GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
	}

	/// <summary> Binds the <c>Texture</c> to its current <c>Texture.Unit</c>. </summary>
	public void Bind()
	{
		GL.ActiveTexture((OpenTK.Graphics.OpenGL.TextureUnit)Unit);
		GL.BindTexture(TextureTarget.Texture2D, Handle);
	}

	private static void UploadTexture(byte[] image, int width, int height)
		=> GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, 
		PixelFormat.Rgb, PixelType.UnsignedByte, image);
}