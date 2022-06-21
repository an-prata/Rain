namespace Rain.Engine;

public enum TextureFilter
{
	Nearest,

	Linear,

	NearestMipmap = OpenTK.Graphics.OpenGL.TextureMinFilter.NearestMipmapNearest,

	NearestMipmapFiltered = OpenTK.Graphics.OpenGL.TextureMinFilter.LinearMipmapNearest
}