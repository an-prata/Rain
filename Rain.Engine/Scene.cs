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
			for (var face = 0; face < Models[model].Faces.Count; face++)
			{
				var vertexArray = Models[model].Faces[face].GetBufferableArray(BufferType.VertexBuffer);
				var elementArray = Models[model].Faces[face].GetBufferableArray(BufferType.ElementBuffer);
				var texturedUniform = program.GetUniformByName("textured");

				bufferGroup.BufferData(BufferType.VertexBuffer, vertexArray);
				bufferGroup.BufferData(BufferType.ElementBuffer, elementArray);
				texturedUniform.SetToBool(Models[model].Faces[face].TryUploadAndBindTextures(program));

				GL.DrawElements(PrimitiveType.Triangles, elementArray.Length, DrawElementsType.UnsignedInt, 0);

				Models[model].Faces[face].TryUnbindTextures();
			}			
		}
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
	/// 
	/// <param name="perspective">
	/// A <c>PerspectiveProjection</c> to create a perspective effect with. 
	/// </param>
	public void Draw(BufferGroup bufferGroup, ShaderProgram program, PerspectiveProjection perspective)
	{
		program.GetUniformByName("perspectiveProjection").SetToPerspectiveProjection(perspective);

		Draw(bufferGroup, program);
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
			foreach (var model in Models)
				model.Dispose();

		disposed = true;
	}

	~Scene() => Dispose(false);

	#endregion
}