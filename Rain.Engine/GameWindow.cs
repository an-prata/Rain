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

	private BufferGroup bufferGroup;

	private ShaderProgram shaderProgram;

	private System.Diagnostics.Stopwatch stopwatch;

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

		bufferGroup = new();
		vertexBuffer = new(BufferType.VertexBuffer, ActiveScene);
		elementBuffer = new(BufferType.ElementBuffer, ActiveScene);

		bufferGroup.Bind();
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

		//bufferGroup.Unbind();
		var shaderComponents = new ShaderComponent[]
		{
			new ShaderComponent(ShaderCompenentType.VertexShader, "vertex_shader.glsl"),
			new ShaderComponent(ShaderCompenentType.FragmentShader, "fragment_shader.glsl")
		};

		shaderProgram = new(shaderComponents);

		// 0 Disables vertical sync.
		// 1 Enables vertical sync.
		// -1 for adaptive vsync.
		Context.SwapInterval = 0;

		stopwatch = new System.Diagnostics.Stopwatch();
		stopwatch.Start();
	}

	protected override void OnUnload()
	{
		vertexBuffer.Dispose();
		elementBuffer.Dispose();
		bufferGroup.Dispose();
		shaderProgram.Dispose();
		
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

		//bufferGroup.Bind();
		vertexBuffer.Bind();
		elementBuffer.Bind();

		shaderProgram.Use();

		GL.DrawElements(PrimitiveType.Triangles, ActiveScene.ElementMemorySpan.Length, DrawElementsType.UnsignedInt, 0);
		
		Context.SwapBuffers();

		base.OnRenderFrame(args);

		Console.WriteLine(stopwatch.ElapsedMilliseconds);
		stopwatch.Reset();
	}
}
