using OpenTK.Graphics.OpenGL;

namespace Rain.Engine;

/// <summary> A class for managing OpenGL Vertex Array Objects. </summary>
public class BufferGroup : IDisposable
{
	/// <summary> The Vertex Array Object's OpenGL handle for use with OpenGL functions. </summary>
	/// <value> An integer representing the OpenGL Vertex Array Object. </value>
	public int Handle { get; }

	/// <summary> Creates a new OpenGL Vertex Array Object. </summary>
	public BufferGroup() => Handle = GL.GenVertexArray();

	/// <summary> Binds this OpenGL Vertex Array Object. </summary>
	public void Bind() => GL.BindVertexArray(Handle);

	/// <summary> Unbinds any currently bound OpenGL Vertex Array Objects. </summary>
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