using System.Buffers;
using System.Runtime.InteropServices;
using Rain.Engine.Geometry;

namespace Rain.Engine;

public class Scene : IDisposable
{
	/// <summary> A <c>Span</c> with this <c>Scene</c>'s vertex data. </summary>
	public Span<float> VertexMemorySpan { get => vertexMemory.Span; }

	/// <summary> A <c>Span</c> with this <c>Scene</c>'s element data. </summary>
	public Span<uint> ElementMemorySpan { get => elementMemory.Span; }

	private readonly IMemoryOwner<float> vertexOwner;

	private readonly IMemoryOwner<uint> elementOwner;

	private readonly Memory<float> vertexMemory;

	private readonly Memory<uint> elementMemory;

	private GCHandle vertexHandle;

	private GCHandle elementHandle;

	private int[] modelVertexIndices;

	private int[] modelElementIndices;

	/// <summary> Creates a new <c>Scene</c> from an array of <c>IModel</c>. </summary>
	/// <param name="models"> The array of <c>IModel</c>s tto render with this <c>Scene</c>. </param>
	public Scene(IModel[] models)
	{
		modelVertexIndices = new int[models.Length];
		modelElementIndices = new int[models.Length];

		var sceneBufferSize = 0;
		int elements = 0;

		var verticesAdded = 0;
		var elementsAdded = 0;
		
		for (var i = 0; i < models.Length; i++)
		{
			sceneBufferSize += models[i].Points.Length * Point.BufferSize;
			elements += models[i].Elements.Length;
		}

		var vertexData = new float[sceneBufferSize];
		var elementData = new uint[elements];

		for (var model = 0; model < models.Length; model++)
		{
			modelVertexIndices[model] = verticesAdded;
			modelElementIndices[model] = elementsAdded;

			var modelBufferArray = models[model].GetBufferableArray();

			for (var i = 0; i < modelBufferArray.Length; i++)
				vertexData[verticesAdded + i] = modelBufferArray[i];

			for (var i = 0; i < models[model].Elements.Length; i++)
				elementData[elementsAdded + i] = (uint)verticesAdded / Point.BufferSize + models[model].Elements[i];

			verticesAdded += modelBufferArray.Length;
			elementsAdded += models[model].Elements.Length;
		}

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
		// large in size.
		if (type == BufferType.VertexBuffer)
		{
			vertexHandle = GCHandle.Alloc(vertexMemory.ToArray(), GCHandleType.Pinned);
			return vertexHandle.AddrOfPinnedObject();
		}
		else
		{
			elementHandle = GCHandle.Alloc(vertexMemory.ToArray(), GCHandleType.Pinned);
			return elementHandle.AddrOfPinnedObject();
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