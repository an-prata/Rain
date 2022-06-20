using OpenTK.Graphics.OpenGL;

namespace Rain.Engine;

public class ShaderProgram : IDisposable
{
	public int Handle { get; }

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

	public void Use() => GL.UseProgram(Handle);

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