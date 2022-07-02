using OpenTK.Graphics.OpenGL;

namespace Rain.Engine.Buffering;

/// <summary>
/// A class for managing supported OpenGL Buffer Objects.
/// </summary>
public class Buffer : IDisposable
{
	/// <summary>
	/// The Buffer Object's OpenGL handle for use with OpenGL functions.
	/// </summary>
	///
	/// <value>
	/// An integer representing the OpenGL Buffer Object.
	/// </value>
	public int Handle { get; }

	/// <summary>
	/// The Buffer Object's type.
	/// </summary>
	///
	/// <value>
	/// A <c>BufferType</c> enum value.
	/// </value>
	public BufferType Type { get; }

	/// <summary>
	/// Creates a new OpenGL Buffer Object from a <c>Scene</c> instance.
	/// </summary>
	///
	/// <param name="type">
	/// The type of Buffer Object to create.
	/// </param>
	public Buffer(BufferType type)
	{
		Type = type;

		Handle = GL.GenBuffer();
		GL.BindBuffer((BufferTarget)Type, Handle);
	}

	/// <summary>
	/// Uploads given data to the graphics card.
	/// </summary>
	///
	/// <param name="data">
	/// The data to upload.
	/// </param>
	public void BufferData(Array data)
	{
		GL.BindBuffer((BufferTarget)Type, Handle);

		if (data.GetType() == typeof(float[]) && Type == BufferType.VertexBuffer)
			GL.BufferData((BufferTarget)Type, data.Length * sizeof(float), (float[])data, BufferUsageHint.StreamDraw);
		else if (data.GetType() == typeof(uint[]) && Type == BufferType.ElementBuffer)
			GL.BufferData((BufferTarget)Type, data.Length * sizeof(uint), (uint[])data, BufferUsageHint.StreamDraw);
		else
			throw new Exception($"Cannot buffer data not of type {typeof(uint)} with a buffer of type {Type}.");
	}

	/// <summary>
	/// Uploads data from the specified <c>IntPtr</c> to the graphics card.
	/// </summary>
	///
	/// <param name="dataPointer">
	/// A pointer to the data to upload.
	/// </param>
	///
	/// <param name="size">
	/// The number of elements to upload.
	/// </param>
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