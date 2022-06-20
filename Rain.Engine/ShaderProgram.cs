using OpenTK.Graphics.OpenGL;

namespace Rain.Engine;

/// <summary> A class for managing OpenGL Shader Programs. </summary>
public class ShaderProgram : IDisposable
{
	/// <summary> The Shader Program's OpenGL handle for use with OpenGL functions. </summary>
	/// <value> An integer representing the OpenGL Shader Program. </value>
	public int Handle { get; }

	/// <summary> Creates a new OpenGL Shader Program from <c>ShaderComponent</c>s. </summary>
	/// <param name="components"> An array of <c>ShaderComponents</c> to create a ShaderProgram from. </param>
	public ShaderProgram(ShaderComponent[] components)
	{
		Handle = GL.CreateProgram();

		for (var i = 0; i < components.Length; i++)
			GL.AttachShader(Handle, components[i].Handle);
		
		GL.LinkProgram(Handle);

		for (var i = 0; i < components.Length; i++)
		{
			GL.DetachShader(Handle, components[i].Handle);
			components[i].Dispose();
		}
	}

	/// <summary> Tells OpenGL to use this Shader Program for rendering. </summary>
	public void Use() => GL.UseProgram(Handle);

	/// <summary> Tells OpenGL to stop using any Shader Program. </summary>
	public void StopUsing() => GL.UseProgram(0);

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
			StopUsing();
			GL.DeleteProgram(Handle);
		}

		disposed = true;
	}

	~ShaderProgram() => Dispose(false);

	#endregion
}