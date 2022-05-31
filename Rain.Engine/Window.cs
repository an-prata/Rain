using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Rain.Engine;

public class Window : GameWindow
{
	public Color4 ClearColor;

	private int vertexArrayObject;

	private int vertexBufferObject;

	private int elementBufferObject;

	private float[] vertices;

	private uint[] indices;

	public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
		: base(gameWindowSettings, nativeWindowSettings)
	{
		vertices = Array.Empty<float>();
		indices = Array.Empty<uint>();
	}

	protected override void OnLoad()
	{
		base.OnLoad();

		GL.ClearColor(ClearColor);

		vertexArrayObject = GL.GenVertexArray();
		GL.BindVertexArray(vertexArrayObject);

		vertexBufferObject = GL.GenBuffer();
		GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
		GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

		elementBufferObject = GL.GenBuffer();
		GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
		GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

		
	}

}
