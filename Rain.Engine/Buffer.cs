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

	private readonly IntPtr dataPointer;

	private readonly int dataSize;

	/// <summary> Creates a new OpenGL Buffer Object from a <c>Scene</c> instance. </summary>
	/// <param name="type"> The type of Buffer Object to create. </param>
	/// <param name="scene"> The <c>Scene</c> who's data will be buffered. </param>
	public Buffer(BufferType type, Scene scene)
	{
		Type = type;

		Handle = GL.GenBuffer();
		GL.BindBuffer((BufferTarget)type, Handle);

		if (type == BufferType.VertexBuffer)
		{
			dataPointer = scene.GetVertexPointer();
			dataSize = scene.VertexMemorySpan.Length * sizeof(float);
		}
		else
		{
			dataPointer = scene.GetElementPointer();
			dataSize = scene.ElementMemorySpan.Length * sizeof(uint);
		}

		GL.BufferData((BufferTarget)Type, dataSize, dataPointer, BufferUsageHint.StreamDraw);
		GL.BindBuffer((BufferTarget)Type, Handle);
	}

	/// <summary> Makes this OpenGL Buffer Object the currently bound Buffer Object. </summary>
	public void Bind() => GL.BindBuffer((BufferTarget)Type, Handle);

	/// <summary> Unbinds the current Buffer Object of type <c>Buffer.Type</c>. </summary>
	public void Unbind() => GL.BindBuffer((BufferTarget)Type, 0);

	/// <summary> Buffers data from the <c>Scene</c> instance passed in during contruction. </summary>
	/// <remarks> As this is done via a pointer, no data needs to be passed to this method. </remarks>
	public void BufferData()
		=> GL.BufferData((BufferTarget)Type, dataSize * sizeof(float), dataPointer, BufferUsageHint.StreamDraw);

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
			GL.DeleteBuffer(Handle);
		}

		disposed = true;
	}

	~Buffer() => Dispose(false);

	#endregion
} 