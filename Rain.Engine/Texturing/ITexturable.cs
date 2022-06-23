using Rain.Engine.Geometry;

namespace Rain.Engine.Texturing;

public interface ITexturable : IModel
{
	/// <summary> A two dimensional array representing the <c>ITexturable</c>'s faces.  </summary>
	/// <remarks> The first index is the face number, and the second is the elements it is comprised of. </remarks>
	uint[,] Faces { get; }

	/// <summary> An array of <c>Texture</c>s to be used by the <c>IRenderable</c>. </summary>
	/// <remarks>
	/// The index correlates to the first index of <c>IRenderable.Faces</c>. For an untextured face use 
	/// <c>Texture.Empty()</c>.
	/// </remarks>
	Texture[] Textures { get; }
}