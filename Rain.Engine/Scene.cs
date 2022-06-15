using System.Buffers;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Rain.Engine;

public class Scene<T> : IDisposable where T : INumber<T>
{
	private readonly IMemoryOwner<T> vertexOwner;

	private readonly IMemoryOwner<uint> elementOwner;

	private readonly Memory<T> vertexMemory;

	private readonly Memory<uint> elementMemory;

	private GCHandle vertexHandle;

	private GCHandle elementHandle;

	public Span<T> VertexMemorySpan { get => vertexMemory.Span; }

	public Span<T> ElementMemorySpan { get => vertexMemory.Span; }

	public Scene(IModel<T>[] models)
	{
		GetModelData(models, out var vertexData, out var elementData);

		vertexOwner = MemoryPool<T>.Shared.Rent(vertexData.Length);
		vertexMemory = vertexOwner.Memory;

		elementOwner = MemoryPool<uint>.Shared.Rent(elementData.Length);
		elementMemory = elementOwner.Memory;

		vertexData.CopyTo(vertexMemory);
		elementData.CopyTo(elementMemory);
	}

	public IntPtr GetVertexPointer()
	{
		// If I am not mistaken, using ToArray() will copy the data, rather than reference it, effectively doubling the
		// required memory. TODO: find a way to remove duplicates of vertex and element data as they could easily be very
		// larg in size.
		vertexHandle = GCHandle.Alloc(vertexMemory.ToArray(), GCHandleType.Pinned);
		return vertexHandle.AddrOfPinnedObject();
	}

	public IntPtr GetElementPointer()
	{
		// If I am not mistaken, using ToArray() will copy the data, rather than reference it, effectively doubling the
		// required memory. TODO: find a way to remove duplicates of vertex and element data as they could easily be very
		// larg in size.
		elementHandle = GCHandle.Alloc(elementMemory.ToArray(), GCHandleType.Pinned);
		return elementHandle.AddrOfPinnedObject();
	}

	public void FreeVertexHandleToGC() => vertexHandle.Free();

	public void FreeElementHandleToGC() => elementHandle.Free();

	private static void GetModelData(IModel<T>[] models, out T[] vertexData, out uint[] elementData)
	{
		// Get vertex data.
		var sceneSizeInT = 0;
		for (var i = 0; i < models.Length; i++)
			sceneSizeInT += models[i].Points.Length * Point<T>.SizeInT;

		vertexData = new T[sceneSizeInT];

		var verticesAdded = 0;

		for (var model = 0; model < models.Length; model++)
		{
			for (var i = 0; i < models[model].Array.Length; i++)
				vertexData[verticesAdded + i] = models[model].Array[i];

			verticesAdded += models[model].Array.Length;
		}

		// Get element data.
		int elements = 0;
		
		for (var i = 0; i < models.Length; i++)
			elements += models[i].Elements.Length;

		elementData = new uint[elements];

		var elementsAdded = 0;

		for (var model = 0; model < models.Length; model++)
		{
			for (var i = 0; i < models[model].Elements.Length; i++)
				elementData[elementsAdded + i] = models[model].Elements[i];

			elementsAdded += models[model].Elements.Length;
		}
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
			vertexHandle.Free();
			elementHandle.Free();
			vertexOwner.Dispose();
			elementOwner.Dispose();
		}

		disposed = true;
	}

	~Scene() => Dispose(false);

	#endregion
}