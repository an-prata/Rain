using OpenTK.Graphics.OpenGL;

namespace Rain.Engine;

/// <summary> A class for managing supported OpenGL Buffer Objects. </summary>
public class Buffer : IDisposable
{
	/// <summary> The Buffer Object's OpenGL handle for use with OpenGL functions. </summary>
	/// <value> An integer representing the OpenGL Buffer Object. </value>
	public int Handle { get; }

	/// <summary> The Buffer Object's type. </summary>
	/// <value> A <c>BufferType</c> enum value. </value>
	public BufferType Type { get; }

	/// <summary> Creates a new OpenGL Buffer Object from a <c>Scene</c> instance. </summary>
	/// <param name="type"> The type of Buffer Object to create. </param>
	/// <param name="scene"> The <c>Scene</c> who's data will be buffered. </param>
	public Buffer(BufferType type)
	{
		Type = type;

		Handle = GL.GenBuffer();
		GL.BindBuffer((BufferTarget)type, Handle);
	}

	/// <summary> Buffers given data. </summary>
	/// <param name="data"> Data to buffer. </param>
	public void BufferData(Array data)
	{
		GL.BindBuffer((BufferTarget)Type, Handle);

		if (data.GetType() == typeof(float[]) && Type == BufferType.VertexBuffer)
			GL.BufferData((BufferTarget)Type, data.Length * sizeof(float), (float[])data, BufferUsageHint.StreamDraw);
		else if (data.GetType() == typeof(uint[]) && Type == BufferType.ElementBuffer)
			GL.BufferData((BufferTarget)Type, data.Length * sizeof(uint), (uint[])data, BufferUsageHint.StreamDraw);
		else
			throw new Exception($"Cannot buffer data not of type {typeof(uint)} with a buffer of type {Type}.");
		
		GL.BindBuffer((BufferTarget)Type, 0);
	}

	/// <summary> Buffer data from the specified <c>IntPtr</c>. </summary>
	/// <param name="dataPointer"> A pointer to the data to buffer. </param>
	/// <param name="size"> The number of elements to buffer. </param>
	public void BufferData(IntPtr dataPointer, int size)
	{
		GL.BindBuffer((BufferTarget)Type, Handle);

		if (Type == BufferType.VertexBuffer)
			GL.BufferData((BufferTarget)Type, size * sizeof(float), dataPointer, BufferUsageHint.StreamDraw);
		else
			GL.BufferData((BufferTarget)Type, size * sizeof(uint), dataPointer, BufferUsageHint.StreamDraw);
		
		GL.BindBuffer((BufferTarget)Type, 0);
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
			GL.BindBuffer((BufferTarget)Type, 0);
			GL.DeleteBuffer(Handle);
		}

		disposed = true;
	}

	~Buffer() => Dispose(false);

	#endregion
} 