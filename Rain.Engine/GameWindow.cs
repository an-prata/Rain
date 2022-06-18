using System.Numerics;
using OpenTK.Windowing;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using Rain.Engine.Geometry;
using Rain.Engine.Extensions;

namespace Rain.Engine;

public class GameWindow : OpenTK.Windowing.Desktop.GameWindow
{
	public Scene ActiveScene { get; set; }

	private int vertexBuffer;

	private int indexBuffer;

	private int vertexArray;

	private int shaderProgram;

	private IntPtr elementPointer;

	private IntPtr vertexPointer;

	public GameWindow(Scene scene, string title, int width, int height) : base(GameWindowSettings.Default, new NativeWindowSettings
	{
		Title = title,
		Size = new(width, height),
		WindowBorder = WindowBorder.Fixed,
		StartVisible = false,
		StartFocused = true,
		API = ContextAPI.OpenGL,
		Profile = ContextProfile.Core,
		APIVersion = new Version(3, 3)
	})
	{
		CenterWindow(new(width, height));
		ActiveScene = scene;
	}

	protected override void OnResize(ResizeEventArgs e)
	{
		GL.Viewport(0, 0, e.Width, e.Height);
		base.OnResize(e);
	}

	protected override void OnLoad()
	{
		IsVisible = true;
		GL.ClearColor(new Color(255, 128, 128, 255).ToColor4()); 

		base.OnLoad();

		vertexPointer = ActiveScene.GetVertexPointer();
		elementPointer = ActiveScene.GetElementPointer();

		vertexBuffer = GL.GenBuffer(); // OpenGL creates a vertex buffer and then returns a handle to it.
		GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer); // Defines the buffer type.

		// Sends data to graphics card through buffer.
		GL.BufferData(BufferTarget.ArrayBuffer,
			ActiveScene.VertexMemorySpan.Length * sizeof(float),
			vertexPointer,
			BufferUsageHint.DynamicDraw);

		GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // Binding to 0 unbinds.

		indexBuffer = GL.GenBuffer();
		GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);

		GL.BufferData(BufferTarget.ElementArrayBuffer,
			ActiveScene.ElementMemorySpan.Length * sizeof(uint),
			elementPointer,
			BufferUsageHint.DynamicDraw);

		GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0); // Binding to 0 unbinds.

		vertexArray = GL.GenVertexArray();
		GL.BindVertexArray(vertexArray);

		GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);

		// layout (location = 0)
		// size is how many elements (vertices).
		// type of elements
		// normalize (false)
		// size of vertex in bytes, (3 times element size)
		// where to start in the array
		GL.VertexAttribPointer(0,
						 Vertex.BufferSize,
						 VertexAttribPointerType.Float,
						 false,
						 (Vertex.BufferSize + Color.BufferSize) * sizeof(float),
						 0);
		GL.EnableVertexAttribArray(0);

		GL.VertexAttribPointer(1,
						 Color.BufferSize,
						 VertexAttribPointerType.Float,
						 false,
						 (Vertex.BufferSize + Color.BufferSize) * sizeof(float),
						 Vertex.BufferSize * sizeof(float));
		GL.EnableVertexAttribArray(1);

		GL.BindVertexArray(0);

		var vertexShader = GL.CreateShader(ShaderType.VertexShader);
		GL.ShaderSource(vertexShader, File.ReadAllText("vertex_shader.glsl"));
		GL.CompileShader(vertexShader);

		var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
		GL.ShaderSource(fragmentShader, File.ReadAllText("fragment_shader.glsl"));
		GL.CompileShader(fragmentShader);

		var vertexShaderInfo = GL.GetShaderInfoLog(vertexShader);
		
		if (vertexShaderInfo != string.Empty)
		{
			Console.WriteLine(vertexShaderInfo);
		}

		var fragmentShaderInfo = GL.GetShaderInfoLog(fragmentShader);
		
		if (fragmentShaderInfo != string.Empty)
		{
			Console.WriteLine(fragmentShaderInfo);
		}

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

		GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
		GL.DeleteBuffer(indexBuffer);

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

		GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer); // Defines the buffer type.
		GL.BufferData(BufferTarget.ArrayBuffer, ActiveScene.VertexMemorySpan.Length * sizeof(float), vertexPointer, BufferUsageHint.DynamicDraw);
		GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // Binding to 0 unbinds.

		GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
		GL.BufferData(BufferTarget.ElementArrayBuffer, ActiveScene.ElementMemorySpan.Length * sizeof(uint), elementPointer, BufferUsageHint.DynamicDraw);
		GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0); // Binding to 0 unbinds.

		GL.BindVertexArray(vertexArray);
		GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);

		GL.UseProgram(shaderProgram);
		GL.BindVertexArray(vertexArray);
		GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
		GL.DrawElements(PrimitiveType.Triangles, ActiveScene.ElementMemorySpan.Length, DrawElementsType.UnsignedInt, 0);
		
		Context.SwapBuffers();

		base.OnRenderFrame(args);
	}
}
