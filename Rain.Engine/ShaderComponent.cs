using OpenTK.Graphics.OpenGL;

namespace Rain.Engine;

public class ShaderComponent : IDisposable
{
	public int Handle { get; }

	public ShaderCompenentType Type { get; }

	public ShaderComponent(ShaderCompenentType type, string pathToSource)
	{
		Type = type;

		Handle = GL.CreateShader((ShaderType)Type);
		GL.ShaderSource(Handle, File.ReadAllText(pathToSource));
		GL.CompileShader(Handle);

		var componentErrors = GL.GetShaderInfoLog(Handle);
		
		if (componentErrors != string.Empty)
			throw new Exception(componentErrors);
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
			GL.DeleteShader(Handle);

		disposed = true;
	}

	~ShaderComponent() => Dispose(false);

	#endregion
}