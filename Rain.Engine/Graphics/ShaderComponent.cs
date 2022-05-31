using OpenTK.Graphics.OpenGL4;

namespace Rain.Engine.Graphics;

class ShaderComponent
{
	public int Handle { get; }

	public ShaderComponent(string shaderPath, ShaderType shaderType)
	{
		var shaderSource = File.ReadAllText(shaderPath);
		var Handle = GL.CreateShader(shaderType);

		GL.ShaderSource(Handle, shaderSource);
		CompileShader(Handle);
	}

	private static void CompileShader(int shader)
	{
		GL.CompileShader(shader);
		GL.GetShader(shader, ShaderParameter.CompileStatus, out var errorCode);

		if (errorCode == (int)All.True)
			return;

		var infoLog = GL.GetShaderInfoLog(shader);
		throw new Exception($"Could not compile Shader({shader}).\n\n{infoLog}");
	}
}