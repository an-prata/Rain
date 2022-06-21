namespace Rain.Engine;

public enum TextureWrapMode
{
	Clamp = OpenTK.Graphics.OpenGL.TextureWrapMode.ClampToEdge,

	Repeat = OpenTK.Graphics.OpenGL.TextureWrapMode.Repeat,

	MirroredRepeat = OpenTK.Graphics.OpenGL.TextureWrapMode.MirroredRepeat,
}