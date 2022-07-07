// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

using Rain.Engine.Geometry;
using Rain.Engine.Texturing;
using Rain.Engine.Buffering;
using Rain.Engine.Rendering;

using TextureUnit = Rain.Engine.Texturing.TextureUnit;

namespace Rain.Engine;

/// <summary>
/// A collection of three dimensional models and methods to draw them to the screen.
/// </summary>
public class Scene : IDisposable
{
	private readonly Memory<float> vertexMemory;

	private readonly Memory<uint> elementMemory;

	private GCHandle vertexHandle;

	private GCHandle elementHandle;

	private readonly int[] modelVertexIndices;

	private readonly int[] modelElementIndices;

	/// <summary> 
	/// A <c>Span</c> with this <c>Scene</c>'s vertex data. 
	/// </summary>
	public Span<float> VertexMemorySpan { get => vertexMemory.Span; }

	/// <summary> 
	/// A <c>Span</c> with this <c>Scene</c>'s element data. 
	/// </summary>
	public Span<uint> ElementMemorySpan { get => elementMemory.Span; }

	/// <summary>
	/// The array of <c>IRenderable</c>s to render with this <c>Scene</c>.
	/// </summary>
	public IRenderable[] Models { get; private set; }

	/// <summary> 
	/// Creates a new <c>Scene</c> from an array of <c>IRenderable</c>. 
	/// </summary>
	/// 
	/// <param name="models"> 
	/// The array of <c>IRenderable</c>s to render with this <c>Scene</c>. 
	/// </param>
	public Scene(IRenderable[] models)
	{
		Models = models;
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
				elementData[elementsAdded + i] = ((uint)verticesAdded / Point.BufferSize) + models[model].Elements[i];

			verticesAdded += modelBufferArray.Length;
			elementsAdded += models[model].Elements.Length;
		}

		vertexHandle = GCHandle.Alloc(vertexData, GCHandleType.Pinned);
		elementHandle = GCHandle.Alloc(elementData, GCHandleType.Pinned);

		vertexMemory = new(vertexData);
		elementMemory = new(elementData);
	}

	/// <summary> 
	/// Creates a new <c>Scene</c> from an array of <c>IModel</c>. 
	/// </summary>
	/// 
	/// <param name="models"> 
	/// The array of <c>IModel</c>s to render with this <c>Scene</c>. 
	/// </param>
	/// 
	/// <param name="textures"> 
	/// An array of <c>Texture</c>s with indices coorelating to <c>models</c>'s. 
	/// </param>
	public Scene(IRenderable[] models, Texture[] textures)
	{
		Models = models;

		if (models.Length != textures.Length)
			throw new Exception($"{nameof(textures)} must be same length as {nameof(models)}.");

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
				elementData[elementsAdded + i] = ((uint)verticesAdded / Point.BufferSize) + models[model].Elements[i];

			verticesAdded += modelBufferArray.Length;
			elementsAdded += models[model].Elements.Length;
		}

		vertexHandle = GCHandle.Alloc(vertexData, GCHandleType.Pinned);
		elementHandle = GCHandle.Alloc(elementData, GCHandleType.Pinned);

		vertexMemory = new(vertexData);
		elementMemory = new(elementData);
	}

	/// <summary> 
	/// Gets a pointer to the data for a buffer of <c>type</c>. 
	/// </summary>
	/// 
	/// <param name="type"> 
	/// The type of <c>Buffer</c> this pointer should be used in. 
	/// </param>
	/// 
	/// <returns> 
	/// An <c>IntPtr</c> object. 
	/// </returns>
	public IntPtr GetPointer(BufferType type)
	{
		if (type == BufferType.VertexBuffer)
			return vertexHandle.AddrOfPinnedObject();
		else
			return elementHandle.AddrOfPinnedObject();
	}

	/// <summary>
	/// Draws the <c>Scene</c> to the <c>GameWindow</c>.
	/// </summary>
	/// 
	/// <param name="bufferGroup">
	/// The <c>BufferGroup</c> to use to upload data to the GPU.
	/// </param>
	/// 
	/// <param name="program">
	/// The <c>ShaderProgram</c> to draw with.
	/// </param>
	public void Draw(BufferGroup bufferGroup, ShaderProgram program)
	{
		for (var model = 0; model < Models.Length; model++)
		{
			for (var face = 0; face < Models[model].Faces.Length; face++)
			{
				var vertexArray = Models[model].Faces[face].GetBufferableArray(BufferType.VertexBuffer);
				var elementArray = Models[model].Faces[face].GetBufferableArray(BufferType.ElementBuffer);

				bufferGroup.BufferData(BufferType.VertexBuffer, vertexArray);
				bufferGroup.BufferData(BufferType.ElementBuffer, elementArray);
				
				for (var texture = 0; texture < Models[model].Faces[face].Textures.Length && texture < Texture.MaximumBoundTextures; texture++)
				{
					if (!Models[model].Faces[face].Textures[texture].IsEmpty)
					{
						if (!Models[model].Faces[face].Textures[texture].IsUploaded)
							Models[model].Faces[face].Textures[texture].Upload(TextureUnit.Unit0 + texture, program.GetUniformByName($"texture{texture}"));
						
						if (texture != 0)
							program.GetUniformByName($"opacity{texture}").SetToFloat(Models[model].Faces[face].Textures[texture].Opacity);

						Models[model].Faces[face].Textures[texture].Bind();
					}
				}

				GL.DrawElements(PrimitiveType.Triangles, elementArray.Length, DrawElementsType.UnsignedInt, 0);

				for (var texture = 0; texture < Models[model].Faces[face].Textures.Length && 
					 texture < Texture.MaximumBoundTextures; texture++)
				{
					Models[model].Faces[face].Textures[texture].Unbind();

					if (texture != 0)
							program.GetUniformByName($"opacity{texture}").SetToFloat(0.0f);
				}
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