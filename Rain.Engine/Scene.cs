using System.Buffers;
using System.Runtime.InteropServices;
using Rain.Engine.Geometry;

namespace Rain.Engine;

public class Scene : IDisposable
{
	private readonly IMemoryOwner<float> vertexOwner;

	private readonly IMemoryOwner<uint> elementOwner;

	private readonly Memory<float> vertexMemory;

	private readonly Memory<uint> elementMemory;

	private GCHandle vertexHandle;

	private GCHandle elementHandle;

	public Span<float> VertexMemorySpan { get => vertexMemory.Span; }

	public Span<uint> ElementMemorySpan { get => elementMemory.Span; }

	/// <summary> Creates a new <c>Scene</c> from an array of <c>IModel</c>. </summary>
	/// <param name="models"> The array of <c>IModel</c>s tto render with this <c>Scene</c>. </param>
	public Scene(IModel[] models)
	{
		GetModelData(models, out var vertexData, out var elementData);

		vertexOwner = MemoryPool<float>.Shared.Rent(vertexData.Length);
		vertexMemory = vertexOwner.Memory;

		elementOwner = MemoryPool<uint>.Shared.Rent(elementData.Length);
		elementMemory = elementOwner.Memory;

		vertexData.CopyTo(vertexMemory);
		elementData.CopyTo(elementMemory);
	}

	/// <summary>  Gets a pointer to the data for a buffer of <c>type</c>. </summary>
	/// <param name="type"> The type of <c>Buffer</c> this pointer should be used in. </param>
	/// <returns> An <c>IntPtr</c> object. </returns>
	public IntPtr GetPointer(BufferType type)
	{
		// If I am not mistaken, using ToArray() will copy the data, rather than reference it, effectively doubling the
		// required memory. TODO: find a way to remove duplicates of vertex and element data as they could easily be very
		// larg in size.
		if (type == BufferType.VertexBuffer)
			vertexHandle = GCHandle.Alloc(vertexMemory.ToArray(), GCHandleType.Pinned);
		else
			elementHandle = GCHandle.Alloc(elementMemory.ToArray(), GCHandleType.Pinned);
		
		return vertexHandle.AddrOfPinnedObject();
	}

	/// <summary> Frees the memory used to store vertices to the Garbage Collector. </summary>
	public void FreeVertexHandleToGC() => vertexHandle.Free();

	/// <summary> Frees the memory used to store elements to the Garbage Collector. </summary>
	public void FreeElementHandleToGC() => elementHandle.Free();

	private static void GetModelData(IModel[] models, out float[] vertexData, out uint[] elementData)
	{
		// Get vertex data.
		var sceneBufferSize = 0;
		int elements = 0;

		var verticesAdded = 0;
		var elementsAdded = 0;
		
		for (var i = 0; i < models.Length; i++)
			sceneBufferSize += models[i].Points.Length * Point.BufferSize;

		for (var i = 0; i < models.Length; i++)
			elements += models[i].Elements.Length;

		vertexData = new float[sceneBufferSize];
		elementData = new uint[elements];

		for (var model = 0; model < models.Length; model++)
		{
			var modelBufferArray = models[model].GetBufferableArray();

			for (var i = 0; i < modelBufferArray.Length; i++)
				vertexData[verticesAdded + i] = modelBufferArray[i];

			for (var i = 0; i < models[model].Elements.Length; i++)
				elementData[elementsAdded + i] = (uint)verticesAdded / Point.BufferSize + models[model].Elements[i];

			verticesAdded += modelBufferArray.Length;
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