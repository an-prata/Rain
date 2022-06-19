using OpenTK.Graphics.OpenGL;

namespace Rain.Engine;

public class Buffer : IDisposable
{
	public int BufferHandle { get; }

	public BufferType Type { get; }

	private readonly IntPtr dataPointer;

	private readonly int dataSize;

	public Buffer(BufferType type, Scene scene)
	{
		Type = type;

		BufferHandle = GL.GenBuffer();
		GL.BindBuffer((BufferTarget)type, BufferHandle);

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
	}

	public void Bind() => GL.BindBuffer((BufferTarget)Type, BufferHandle);

	public void Unbind() => GL.BindBuffer((BufferTarget)Type, 0);

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
			GL.BindBuffer((BufferTarget)Type, 0);
			GL.DeleteBuffer(BufferHandle);
		}

		disposed = true;
	}

	~Buffer() => Dispose(false);

	#endregion
} 