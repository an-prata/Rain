using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

using Rain.Engine.Graphics;

namespace Rain.Engine;

public class Window : GameWindow
{
	public Color4 ClearColor { get; init; }

	public string VertexShaderPath { get; init; }

	public string FragmentShaderPath { get; init; }

	private int vertexArrayObject;

	private int vertexBufferObject;

	private int elementBufferObject;

	private float[] vertices;

	private uint[] indices;

	private Shader shader;

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

		var shaderComponents = new List<ShaderComponent>
		{
			new ShaderComponent(VertexShaderPath, ShaderType.VertexShader),
			new ShaderComponent(FragmentShaderPath, ShaderType.FragmentShader)
		};

		shader = new Shader(shaderComponents);
		shader.Use();

		// Upload vertex and texturee data to the shaders.

		/* // Because there's now 5 floats between the start of the first vertex and the start of the second,
           // we modify the stride from 3 * sizeof(float) to 5 * sizeof(float).
           // This will now pass the new vertex array to the buffer.
           var vertexLocation = _shader.GetAttribLocation("aPosition");
           GL.EnableVertexAttribArray(vertexLocation);
           GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            // Next, we also setup texture coordinates. It works in much the same way.
            // We add an offset of 3, since the texture coordinates comes after the position data.
            // We also change the amount of data to 2 because there's only 2 floats for texture coordinates.
            var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            _texture = Texture.LoadFromFile("Resources/container.png");
            _texture.Use(TextureUnit.Texture0); */
	}

}
