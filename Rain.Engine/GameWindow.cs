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
	/// <summary> The currently active <c>Scene</c> object for the <c>GameWindow</c>. </summary>
	public Scene ActiveScene { get; set; }

	public Color ClearColor { get; }

	private Buffer vertexBuffer;

	private Buffer elementBuffer;

	private int vertexArray;

	private int shaderProgram;

	public GameWindow(GameOptions options) : base(new GameWindowSettings
	{
		RenderFrequency = options.RenderFrequency ?? 0.0d,
		UpdateFrequency = options.UpdateFrequency ?? 0.0d
	}, 
	new NativeWindowSettings
	{
		Size 			= new(options.Width, options.Height),
		Title 			= options.WindowTitle ?? "Rain",
		WindowBorder 	= options.WindowBorder ?? WindowBorder.Resizable,
		StartVisible 	= options.StartVisible ?? true,
		StartFocused 	= options.StartFocused ?? true,
		API 			= ContextAPI.OpenGL,
		Profile 		= ContextProfile.Core,
		APIVersion 		= new Version(3, 3)
	})
	{
		if (options.CenterWindow ?? false)
			CenterWindow(new(options.Width, options.Height));
		
		ActiveScene = new(Array.Empty<IModel>());
		ClearColor = options.ClearColor ?? new(255, 255, 255);
	}

	protected override void OnResize(ResizeEventArgs e)
	{
		GL.Viewport(0, 0, e.Width, e.Height);
		base.OnResize(e);
	}

	protected override void OnLoad()
	{
		GL.ClearColor(ClearColor.ToColor4()); 

		base.OnLoad();

		vertexBuffer = new(BufferType.VertexBuffer, ActiveScene);
		elementBuffer = new(BufferType.ElementBuffer, ActiveScene);

		vertexArray = GL.GenVertexArray();
		GL.BindVertexArray(vertexArray);

		vertexBuffer.Bind();

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
		vertexBuffer.Dispose();
		elementBuffer.Dispose();

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

		vertexBuffer.BufferData();
		elementBuffer.BufferData();

		GL.BindVertexArray(vertexArray);
		vertexBuffer.Bind();

		GL.UseProgram(shaderProgram);
		GL.BindVertexArray(vertexArray);
		
		elementBuffer.Bind();

		GL.DrawElements(PrimitiveType.Triangles, ActiveScene.ElementMemorySpan.Length, DrawElementsType.UnsignedInt, 0);
		
		Context.SwapBuffers();

		base.OnRenderFrame(args);
	}
}
