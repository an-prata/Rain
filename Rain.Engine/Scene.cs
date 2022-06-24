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

	private readonly Memory<float> vertexMemory;

	private readonly Memory<uint> elementMemory;

	private GCHandle vertexHandle;

	private GCHandle elementHandle;

	/// <summary> Creates a new <c>Scene</c> from an array of <c>IModel</c>. </summary>
	/// <param name="models"> The array of <c>IModel</c>s tto render with this <c>Scene</c>. </param>
	public Scene(IModel[] models)
	{
		GetModelData(models, out var vertexData, out var elementData);

		vertexHandle = GCHandle.Alloc(vertexData, GCHandleType.Pinned);
		elementHandle = GCHandle.Alloc(elementData, GCHandleType.Pinned);

		vertexMemory = new(vertexData);
		elementMemory = new(elementData);
	}

	/// <summary>  Gets a pointer to the data for a buffer of <c>type</c>. </summary>
	/// <param name="type"> The type of <c>Buffer</c> this pointer should be used in. </param>
	/// <returns> An <c>IntPtr</c> object. </returns>
	public IntPtr GetPointer(BufferType type)
	{
		if (type == BufferType.VertexBuffer)
			return vertexHandle.AddrOfPinnedObject();
		else
			return elementHandle.AddrOfPinnedObject();
	}

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
		}

		disposed = true;
	}

	~Scene() => Dispose(false);

	#endregion
}