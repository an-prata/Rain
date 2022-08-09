// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Graphics.OpenGL;
using Rain.Engine.Rendering;
using Rain.Engine.Texturing;

using TextureUnit = Rain.Engine.Texturing.TextureUnit;

namespace Rain.Engine;

/// <summary>
/// A class for managing and interacting with GLSL/OpenGL uniforms.
/// </summary>
public class Uniform
{
	/// <summary>
	/// The Uniform's OpenGL handle for use with OpenGL functions.
	/// </summary>
	///
	/// <value>
	/// An integer representing the OpenGL Uniform.
	/// </value>
	public int Handle { get; }

	/// <summary>
	/// Uniform's name in GLSL shader source code.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// Creates a new <c>Uniform</c> instance from an OpenGL Handle and Name.
	/// </summary>
	///
	/// <param name="name">
	/// The Uniform's name in the GLSL shader.
	/// </param>
	///
	/// <param name="handle">
	/// The OpenGL handle to the unifrom.
	/// </param>
	public Uniform(string name, int handle)
	{
		Name = name;
		Handle = handle;
	}

	/// <summary>
	/// Creates a new <c>Uniform</c> instance from its GLSL shader name and an OpenGL ShaderProgram handle.
	/// </summary>
	///
	/// <param name="shaderHandle">
	/// The hande of an OpenGL Shader Program.
	/// </param>
	///
	/// <param name="name">
	/// The name of the uniform in a GLSL shader.
	/// </param>
	public Uniform(int shaderHandle, string name)
	{
		Name = name;
		Handle = GL.GetUniformLocation(shaderHandle, Name);
	}

	/// <summary>
	/// Sets the uniform to reference a <c>Texture</c> in GLSL.
	/// </summary>
	///
	/// <param name="texture">
	/// The <c>Texture</c> instance to reference in the GLSL shader.
	/// </param>
	public void SetToTexture(Texture texture)
		=> GL.Uniform1(Handle, (int)texture.Unit - (int)TextureUnit.Unit0);

	/// <summary>
	/// Sets the uniform to reference a float in GLSL.
	/// </summary>
	///
	/// <param name="x">
	/// The <c>float</c> to reference in the GLSL shader.
	/// </param>	
	public void SetToFloat(float x)
		=> GL.Uniform1(Handle, x);

	/// <summary>
	/// Sets the uniform to reference a bool in GLSL.
	/// </summary>
	///
	/// <param name="x">
	/// The <c>bool</c> to reference in the GLSL shader.
	/// </param>
	public void SetToBool(bool x)
		=> GL.Uniform1(Handle, x ? 1 : 0);
	
	/// <summary>
	/// Sets the uniform to a <c>PerspectiveProjection</c>. 
	/// </summary>
	/// 
	/// <param name="projection">
	/// The <c>PerspectiveProjection</c> to use.
	/// </param>
	public void SetToMatrix4(PerspectiveProjection projection)
		=> GL.UniformMatrix4(Handle, true, ref projection.OpenGLMatrixPerspectiveTransform);

	public void SetToMatrix4(Camera projection)
		=> GL.UniformMatrix4(Handle, true, ref projection.OpenGLMatrixCameraTranform);
}