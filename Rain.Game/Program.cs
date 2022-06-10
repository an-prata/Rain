using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Rain.Game;

class Program
{
	static void Main(string[] args)
	{
		using var game = new Game("game", new(1280, 720));
		game.Run(); // starts rendering cycle.
	}
}

public class Game : GameWindow
{
	private int vertexBuffer;

	private int shaderProgram;

	private int vertexArray;

	public Game(string title, Vector2i size) : base(GameWindowSettings.Default, new NativeWindowSettings 
	{
		Title = title,
		Size = size,
		WindowBorder = WindowBorder.Fixed,
		StartVisible = false,
		StartFocused = true,
		API = ContextAPI.OpenGL,
		Profile = ContextProfile.Core,
		APIVersion = new Version(3, 3)
	})
	{
		CenterWindow(new Vector2i(1280, 720));
	}

	protected override void OnResize(ResizeEventArgs e)
	{
		GL.Viewport(0, 0, e.Width, e.Height);
		base.OnResize(e);
	}

	protected override void OnLoad()
	{
		IsVisible = true;
		// Set the color to clear to. Needs to be set before calling GL.Clear().
		GL.ClearColor(new Color4(0.4f, 0.1f, 0.6f, 1.0f)); 
		base.OnLoad();

		float[] vertices = {
			0.0f,	0.5f,	0.0f,	1.0f, 	0.0f,	0.0f,	1.0f,
			0.5f,	-0.5f,	0.0f,	0.0f,	1.0f,	0.0f,	1.0f,
			-0.5f,	-0.5f,	0.0f,	0.0f,	0.0f, 	1.0f,	1.0f
		};

		vertexBuffer = GL.GenBuffer(); // OpenGL creates a vertex buffer and then returns a handle to it.
		GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer); // Defines the buffer type.
		// Sends data to graphics card through buffer.
		GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
		GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // Binding to 0 unbinds.

		vertexArray = GL.GenVertexArray();
		GL.BindVertexArray(vertexArray);

		GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);

		// layout (location = 0)
		// size is how many elements (vertices).
		// type of elements
		// normalize (false)
		// size of vertex in bytes, (3 times element size)
		// where to start in the array
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
		GL.EnableVertexAttribArray(0);

		GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));
		GL.EnableVertexAttribArray(1);

		GL.BindVertexArray(0);

		var vertexShader = GL.CreateShader(ShaderType.VertexShader);
		GL.ShaderSource(vertexShader, File.ReadAllText("vertex_shader.glsl"));
		GL.CompileShader(vertexShader);

		var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
		GL.ShaderSource(fragmentShader, File.ReadAllText("fragment_shader.glsl"));
		GL.CompileShader(fragmentShader);

		shaderProgram = GL.CreateProgram();
		GL.AttachShader(shaderProgram, vertexShader);
		GL.AttachShader(shaderProgram, fragmentShader);

		GL.LinkProgram(shaderProgram);

		GL.DetachShader(shaderProgram, vertexShader);
		GL.DetachShader(shaderProgram, fragmentShader);

		GL.DeleteShader(vertexShader);
		GL.DeleteShader(fragmentShader);
	}

	protected override void OnUnload()
	{
		GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		GL.DeleteBuffer(vertexBuffer);
		GL.UseProgram(0);
		GL.DeleteProgram(shaderProgram);
		
		base.OnUnload();
	}

	protected override void OnUpdateFrame(FrameEventArgs args)
	{
		base.OnUpdateFrame(args);
	}

	protected override void OnRenderFrame(FrameEventArgs args)
	{
		GL.Clear(ClearBufferMask.ColorBufferBit); // Apply clear color to render.

		GL.UseProgram(shaderProgram);
		GL.BindVertexArray(vertexArray);

		GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

		Context.SwapBuffers();
		base.OnRenderFrame(args);
	}
}