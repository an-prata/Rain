using OpenTK.Graphics.OpenGL;

namespace Rain.Engine;

/// <summary> A class for managing OpenGL Vertex Array Objects. </summary>
public class BufferGroup : IDisposable
{
	/// <summary> The Vertex Array Object's OpenGL handle for use with OpenGL functions. </summary>
	/// <value> An integer representing the OpenGL Vertex Array Object. </value>
	public int Handle { get; }

	private readonly Dictionary<BufferType, Buffer> buffers;

	/// <summary> Creates a new OpenGL Vertex Array Object. </summary>
	public BufferGroup(Buffer[] buffers)
	{
		Handle = GL.GenVertexArray();
		this.buffers = new();
		Bind();

		for (var i = 0; i < buffers.Length; i++)
		{
			GL.BindBuffer((BufferTarget)buffers[i].Type, buffers[i].Handle);
			this.buffers[buffers[i].Type] = buffers[i];
		}
		
		Unbind();
	}

	/// <summary> Calls <c>Buffer.BufferData()</c> all <c>Buffer</c>s given during construction. </summary>
	public void BufferData()
	{
		foreach (var buffer in buffers)
			buffer.Value.BufferData();
	}

	/// <summary> Calls <c>Buffer.BufferData()</c> for the <c>Buffer</c> of the given <c>BufferType</c>. </summary>
	/// <param name="buffer"> The <c>BufferType</c> used to specify the <c>BufferObject</c>. </param>
	public void BufferData(BufferType buffer) => buffers[buffer].BufferData();

	/// <summary> Calls <c>Buffer.BufferData()</c> for the <c>Buffer</c> of the given <c>BufferType</c>. </summary>
	/// <param name="buffer"> The <c>BufferType</c> used to specify the <c>BufferObject</c>. </param>
	/// <param name="data"> The data to buffer. </param>
	public void BufferData(BufferType buffer, Array data) => buffers[buffer].BufferData(data);

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
			foreach (var buffer in buffers)
				buffer.Value.Dispose();

			Unbind();
			GL.DeleteVertexArray(Handle);
		}

		disposed = true;
	}

	~BufferGroup() => Dispose(false);

	#endregion
}