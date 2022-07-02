using OpenTK.Graphics.OpenGL;

namespace Rain.Engine.Buffering;

public enum BufferType
{
	VertexBuffer = BufferTarget.ArrayBuffer,

	ElementBuffer = BufferTarget.ElementArrayBuffer
}