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
		GL.BindVertexArray(Handle);
		this.buffers = new();

		for (var buffer = 0; buffer < buffers.Length; buffer++)
		{
			GL.BindBuffer((BufferTarget)buffers[buffer].Type, buffers[buffer].Handle);
			this.buffers[buffers[buffer].Type] = buffers[buffer];
		}
		
		GL.BindVertexArray(0);
	}

	/// <summary> Buffers all given values of <c>data</c> using the <c>Buffer</c> that matches its key. </summary>
	/// <param name="data"> A <c>Dictionary</c> with <c>BufferType</c>s as keys and arrays as values. </param>
	public void BufferData(Dictionary<BufferType, Array> data)
	{
		GL.BindVertexArray(Handle);

		foreach (var bufferData in data)
			buffers[bufferData.Key].BufferData(bufferData.Value);
		
		GL.BindVertexArray(0);
	}

	/// <summary> Calls <c>Buffer.BufferData()</c> for the <c>Buffer</c> of the given <c>BufferType</c>. </summary>
	/// <param name="buffer"> The <c>BufferType</c> used to specify the <c>BufferObject</c>. </param>
	/// <param name="data"> The data to buffer. </param>
	public void BufferData(BufferType buffer, Array data) 
	{
		GL.BindVertexArray(Handle);
		buffers[buffer].BufferData(data);
		GL.BindVertexArray(0);
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
			foreach (var buffer in buffers)
				buffer.Value.Dispose();

			GL.BindVertexArray(0);
			GL.DeleteVertexArray(Handle);
		}

		disposed = true;
	}

	~BufferGroup() => Dispose(false);

	#endregion
}