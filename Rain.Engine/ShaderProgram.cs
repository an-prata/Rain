using OpenTK.Graphics.OpenGL;

namespace Rain.Engine;

/// <summary> A class for managing OpenGL Shader Programs. </summary>
public class ShaderProgram : IDisposable
{
	/// <summary> The Shader Program's OpenGL handle for use with OpenGL functions. </summary>
	/// <value> An integer representing the OpenGL Shader Program. </value>
	public int Handle { get; }

	/// <summary> Creates a new OpenGL Shader Program from <c>ShaderComponent</c>s. </summary>
	/// <param name="components"> An array of <c>ShaderComponents</c> to create a ShaderProgram from. </param>
	public ShaderProgram(ShaderComponent[] components)
	{
		Handle = GL.CreateProgram();

		for (var i = 0; i < components.Length; i++)
			GL.AttachShader(Handle, components[i].Handle);
		
		GL.LinkProgram(Handle);

		for (var i = 0; i < components.Length; i++)
		{
			GL.DetachShader(Handle, components[i].Handle);
			components[i].Dispose();
		}
	}

	/// <summary> Gets a specific GLSL/OpenGL uniform by name. </summary>
	/// <param name="uniformName"> The Uniform's name in the GLSL shader. </param>
	/// <returns> A <c>Uniform</c> instance representing the GLSL/OpenGL shader. </returns>
	public Uniform GetUniformByName(string uniformName)
	{
		GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var uniforms);

		for (var i = 0; i < uniforms; i++)
		{
			var name = GL.GetActiveUniform(Handle, i, out _, out _);
			if (name != uniformName) continue;

			var handle = GL.GetUniformLocation(Handle, name);
			return new(name, handle);
		}

		throw new NullReferenceException("No uniform of name \"" + uniformName + "\" was found.");
	}

	/// <summary> Gets all active OpenGL Uniforms in the Shader and returns an Array of <c>Unifmorm</c> objects. </summary>
	/// <returns> An Array of <c>Unifmorm</c> objects. </returns>
	public Uniform[] GetUniforms()
	{
		GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var uniforms);
		var uniformArray = new Uniform[uniforms];

		for (var i = 0; i < uniforms; i++)
		{
			var name = GL.GetActiveUniform(Handle, i, out _, out _);
			var handle = GL.GetUniformLocation(Handle, name);
			uniformArray[i] = new(name, handle);
		}

		return uniformArray;
	}

	/// <summary> Gets an OpenGL Vertex Attribute by its name in the shader. </summary>
	/// <param name="attributeName"> The name of the OpenGL Vertex Attribute. </param>
	/// <returns> The OpenGL Vertex Attribute's handle. </returns>
	public int GetAttributeHandleByName(string attributeName) => GL.GetAttribLocation(Handle, attributeName);

	/// <summary> Tells OpenGL to use this Shader Program for rendering. </summary>
	public void Use() => GL.UseProgram(Handle);

	/// <summary> Tells OpenGL to stop using any Shader Program. </summary>
	public void StopUsing() => GL.UseProgram(0);

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
			StopUsing();
			GL.DeleteProgram(Handle);
		}

		disposed = true;
	}

	~ShaderProgram() => Dispose(false);

	#endregion
}