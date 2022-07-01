using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

using Rain.Engine.Geometry;
using Rain.Engine.Texturing;
using Rain.Engine.Buffering;
using Rain.Engine.Rendering;

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

	private int[] modelVertexIndices;

	private int[] modelElementIndices;

	private IRenderable[] models;

	/// <summary> Creates a new <c>Scene</c> from an array of <c>IRenderable</c>. </summary>
	/// <param name="models"> The array of <c>IRenderable</c>s to render with this <c>Scene</c>. </param>
	public Scene(IRenderable[] models)
	{
		this.models = models;
		var sceneBufferSize = 0;
		int elements = 0;

		var verticesAdded = 0;
		var elementsAdded = 0;
		
		for (var i = 0; i < models.Length; i++)
			sceneBufferSize += models[i].Points.Length * Point.BufferSize;

		for (var i = 0; i < models.Length; i++)
			elements += models[i].Elements.Length;

		var vertexData = new float[sceneBufferSize];
		var elementData = new uint[elements];

		modelVertexIndices = new int[models.Length];
		modelElementIndices = new int[models.Length];

		for (var model = 0; model < models.Length; model++)
		{
			modelVertexIndices[model] = verticesAdded;
			modelElementIndices[model] = elementsAdded;

			var modelBufferArray = (float[])models[model].GetBufferableArray(BufferType.VertexBuffer);

			for (var i = 0; i < modelBufferArray.Length; i++)
				vertexData[verticesAdded + i] = modelBufferArray[i];

			for (var i = 0; i < models[model].Elements.Length; i++)
				elementData[elementsAdded + i] = (uint)verticesAdded / Point.BufferSize + models[model].Elements[i];

			verticesAdded += modelBufferArray.Length;
			elementsAdded += models[model].Elements.Length;
		}

		vertexHandle = GCHandle.Alloc(vertexData, GCHandleType.Pinned);
		elementHandle = GCHandle.Alloc(elementData, GCHandleType.Pinned);

		vertexMemory = new(vertexData);
		elementMemory = new(elementData);
	}

	/// <summary> Creates a new <c>Scene</c> from an array of <c>IModel</c>. </summary>
	/// <param name="models"> The array of <c>IModel</c>s to render with this <c>Scene</c>. </param>
	/// <param name="textures"> An array of <c>Texture</c>s with indices coorelating to <c>models</c>'s. </param>
	public Scene(IRenderable[] models, Texture[] textures)
	{
		this.models = models;
		if (models.Length != textures.Length)
			throw new Exception($"{nameof(textures)} must be same length as {nameof(models)}.");

		var sceneBufferSize = 0;
		int elements = 0;

		var verticesAdded = 0;
		var elementsAdded = 0;
		
		for (var i = 0; i < models.Length; i++)
			sceneBufferSize += models[i].Points.Length * Point.BufferSize;

		for (var i = 0; i < models.Length; i++)
			elements += models[i].Elements.Length;

		var vertexData = new float[sceneBufferSize];
		var elementData = new uint[elements];

		modelVertexIndices = new int[models.Length];
		modelElementIndices = new int[models.Length];

		for (var model = 0; model < models.Length; model++)
		{
			modelVertexIndices[model] = verticesAdded;
			modelElementIndices[model] = elementsAdded;

			var modelBufferArray = (float[])models[model].GetBufferableArray(BufferType.VertexBuffer);

			for (var i = 0; i < modelBufferArray.Length; i++)
				vertexData[verticesAdded + i] = modelBufferArray[i];

			for (var i = 0; i < models[model].Elements.Length; i++)
				elementData[elementsAdded + i] = (uint)verticesAdded / Point.BufferSize + models[model].Elements[i];

			verticesAdded += modelBufferArray.Length;
			elementsAdded += models[model].Elements.Length;
		}

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

	public void Draw(BufferGroup bufferGroup)
	{
		for (var model = 0; model < modelVertexIndices.Length; model++)
		{
			for (var face = 0; face < models[model].Faces.Length; face++)
			{
				bufferGroup.BufferData(BufferType.VertexBuffer, 
									   models[model].Faces[face].GetBufferableArray(BufferType.VertexBuffer));
				bufferGroup.BufferData(BufferType.ElementBuffer, models[model].Faces[face].Face.Elements);
				
				if (!models[model].Faces[face].Texture.IsEmpty)
					models[model].Faces[face].Texture.Bind();
				
				GL.DrawElements(PrimitiveType.Triangles, ElementMemorySpan.Length, DrawElementsType.UnsignedInt, 0);
			}
			
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