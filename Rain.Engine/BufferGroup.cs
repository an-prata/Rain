using OpenTK.Graphics.OpenGL;

namespace Rain.Engine;

public class BufferGroup : IDisposable
{
	public int Handle { get; }

	public BufferGroup() => Handle = GL.GenVertexArray();

	public void Bind() => GL.BindVertexArray(Handle);

	public void Unbind() => GL.BindVertexArray(0);

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
			GL.DeleteVertexArray(Handle);
		}

		disposed = true;
	}

	~BufferGroup() => Dispose(false);

	#endregion
}