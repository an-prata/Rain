using OpenTK.Graphics.OpenGL;

namespace Rain.Engine;

/// <summary> 
/// A class for managing OpenGL Shaders. 
/// </summary>
public class ShaderComponent : IDisposable
{
	/// <summary> 
	/// The Shader's OpenGL handle for use with OpenGL functions. 
	/// </summary>
	/// 
	/// <value> 
	/// An integer representing the OpenGL Shader. 
	/// </value>
	public int Handle { get; }

	/// <summary> 
	/// The type of OpenGL Shader. 
	/// </summary>
	/// 
	/// <value> 
	/// A <c>ShaderComponentType</c> enum value. 
	/// </value>
	public ShaderCompenentType Type { get; }

	/// <summary> 
	/// Creates a new OpenGL shader from a GLSL source file. 
	/// </summary>
	/// 
	/// <param name="type"> 
	/// The type of shader to create. 
	/// </param>
	/// 
	/// <param name="pathToSource"> 
	/// A path to the shader's source file. 
	/// </param>
	public ShaderComponent(ShaderCompenentType type, string pathToSource)
	{
		Type = type;

		Handle = GL.CreateShader((ShaderType)Type);
		GL.ShaderSource(Handle, File.ReadAllText(pathToSource));
		GL.CompileShader(Handle);

		var componentErrors = GL.GetShaderInfoLog(Handle);
		
		if (componentErrors != string.Empty)
			throw new Exception(componentErrors);
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
			GL.DeleteShader(Handle);

		disposed = true;
	}

	~ShaderComponent() => Dispose(false);

	#endregion
}