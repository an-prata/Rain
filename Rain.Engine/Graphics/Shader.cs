using OpenTK.Graphics.OpenGL4;

namespace Rain.Engine.Graphics;

class Shader
{
	public int Handle { get; }

	private readonly Dictionary<string, int> uniformLocations;

	public Shader(ShaderComponent[] shaderComponents)
	{
		Handle = GL.CreateProgram();

		foreach (ShaderComponent shaderComponent in shaderComponents)
		{
			GL.AttachShader(Handle, shaderComponent.Handle);
		}

		LinkProgram(Handle);

		foreach (ShaderComponent shaderComponent in shaderComponents)
		{
			GL.DetachShader(Handle, shaderComponent.Handle);
			GL.DeleteShader(shaderComponent.Handle);
		}

		GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

		uniformLocations = new Dictionary<string, int>();

		for (var i = 0; i < numberOfUniforms; i++)
		{
			var key = GL.GetActiveUniform(Handle, i, out _, out _);
			var location = GL.GetUniformLocation(Handle, key);

			uniformLocations.Add(key, location);
		}
	}

	public void Use()
	{
		GL.UseProgram(Handle);
	}

	public int GetAttributeLocation(string attributeName)
	{
		return GL.GetAttribLocation(Handle, attributeName);
	}

	private static void LinkProgram(int program)
	{
		GL.LinkProgram(program);
		GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var errorCode);

		if (errorCode == (int)All.True)
			return;

		throw new Exception($"Could not link Program({program}).");
	}
}