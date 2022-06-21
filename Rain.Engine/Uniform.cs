using OpenTK.Graphics.OpenGL;

namespace Rain.Engine;

/// <summary> A class for managing and interacting with GLSL/OpenGL uniforms. </summary>
public class Uniform
{
	/// <summary> The Uniform's OpenGL handle for use with OpenGL functions. </summary>
	/// <value> An integer representing the OpenGL Uniform. </value>
	public int Handle { get; }

	/// <summary> Uniform's name in GLSL shader source code. </summary>
	public string Name { get; }

	/// <summary> Creates a new <c>Uniform</c> instance from an OpenGL Handle and Name. </summary>
	/// <param name="name"> The Uniform's name in the GLSL shader. </param>
	/// <param name="handle"> The OpenGL handle to the unifrom. </param>
	public Uniform(string name, int handle)
	{
		Name = name;
		Handle = handle;
	}

	/// <summary> 
	/// Creates a new <c>Uniform</c> instance from its GLSL shader name and an OpenGL ShaderProgram handle. 
	/// </summary>
	/// <param name="shaderHandle"> The hande of an OpenGL Shader Program. </param>
	/// <param name="name"> The name of the uniform in a GLSL shader. </param>
	public Uniform(int shaderHandle, string name)
	{
		Name = name;
		Handle = GL.GetUniformLocation(shaderHandle, Name);
	}

	/// <summary> Sets the uniform to reference a <c>Shader</c> in GLSL. </summary>
	/// <param name="texture"> The <c>Texture</c> instance to reference in the GLSL shader. </param>
	public void SetToTexture(Texture texture) 
		=> GL.Uniform1(Handle, (int)texture.Unit - (int)TextureUnit.Unit0);
}