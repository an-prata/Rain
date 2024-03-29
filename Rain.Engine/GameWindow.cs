// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Windowing;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

using Rain.Engine.Geometry;
using Rain.Engine.Texturing;
using Rain.Engine.Buffering;
using Rain.Engine.Rendering;
using Rain.Engine.Extensions;

using Point = Rain.Engine.Geometry.Point;
using Buffer = Rain.Engine.Buffering.Buffer;
using TextureUnit = Rain.Engine.Texturing.TextureUnit;

namespace Rain.Engine;

public class GameWindow : OpenTK.Windowing.Desktop.GameWindow
{
	/// <summary> 
	/// The currently active <c>Scene</c> object for the <c>GameWindow</c>. 
	/// </summary>
	public Scene ActiveScene { get; set; }

	private PerspectiveProjection perspective;

	private Camera camera;

	private Color clearColor;

	private BufferGroup bufferGroup;

	private ShaderProgram shaderProgram;

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
		APIVersion 		= new Version(4, 5)
	})
	{
		if (options.CenterWindow ?? false)
			CenterWindow(new(options.Width, options.Height));
		
		ActiveScene = options.StartingScene;
		clearColor = options.ClearColor ?? new(255, 255, 255);
		
		var buffers = new Buffer[] 
		{ 
			new(BufferType.VertexBuffer), 
			new(BufferType.ElementBuffer) 
		};

		bufferGroup = new(buffers);

		var shaderComponents = new ShaderComponent[]
		{
			new ShaderComponent(ShaderCompenentType.VertexShader, "vertex_shader.glsl"),
			new ShaderComponent(ShaderCompenentType.FragmentShader, "fragment_shader.glsl")
		};

		shaderProgram = new(shaderComponents);
		perspective = new(new Angle { Degrees = 45.0f }, options.Width / options.Height, 0.1f, 100.0f);
		camera = new(new(0.0f, 0.0f, 4.0f));
	}

	protected override void OnResize(ResizeEventArgs e)
	{
		GL.Viewport(0, 0, e.Width, e.Height);
		base.OnResize(e);
	}

	protected override void OnLoad()
	{
		GL.ClearColor(clearColor.ToColor4());

		base.OnLoad();

		Point.SetAttributes(shaderProgram);

		// 0 Disables vertical sync.
		// 1 Enables vertical sync.
		// -1 for adaptive vsync.
		Context.SwapInterval = 1;

		// Enable the use of a Texture's alpha component.
		GL.Enable(EnableCap.Blend);
		GL.Enable(EnableCap.DepthTest);

		GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
		CursorState = CursorState.Grabbed;
	}

	protected override void OnUnload()
	{
		bufferGroup.Dispose();
		shaderProgram.Dispose();
		base.OnUnload();
	}

	protected override void OnUpdateFrame(FrameEventArgs args)
	{
		if (!IsFocused)
			return;
		
		if (IsKeyDown(Keys.W))
			camera.Location += camera.Facing * (float)args.Time * 2;
		if (IsKeyDown(Keys.S))
			camera.Location -= camera.Facing * (float)args.Time * 2;
		if (IsKeyDown(Keys.D))
			camera.Location += camera.Right * (float)args.Time * 2;
		if (IsKeyDown(Keys.A))
			camera.Location -= camera.Right * (float)args.Time * 2;
		
		camera.RotationX -= Angle.FromDegrees((MouseState.Y - MouseState.PreviousY) * 0.2f);
		camera.RotationY += Angle.FromDegrees((MouseState.X - MouseState.PreviousX) * 0.2f);

		if (camera.RotationX.Degrees > 89.0)
			camera.RotationX = Angle.FromDegrees(89.0);

		if (camera.RotationX.Degrees < -89.0)
			camera.RotationX = Angle.FromDegrees(-89.0);
		
		base.OnUpdateFrame(args);
	}

	protected override void OnRenderFrame(FrameEventArgs args)
	{
		GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // Apply clear color to render.
		shaderProgram.Use();

		ActiveScene.Draw(bufferGroup, shaderProgram, perspective, camera);
		
		Context.SwapBuffers();
		base.OnRenderFrame(args);

		Console.WriteLine(args.Time);
	}
}
