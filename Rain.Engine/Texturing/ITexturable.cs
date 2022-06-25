using Rain.Engine.Geometry;

namespace Rain.Engine.Texturing;

public interface ITexturable : IModel, IDisposable
{
	/// <summary> A two dimensional array representing the <c>ITexturable</c>'s faces.  </summary>
	/// <remarks> The first index is the face number, and the second is the elements it is comprised of. </remarks>
	uint[,] Faces { get; }

	/// <summary> An array of <c>Texture</c>s to be used by the <c>IRenderable</c>. </summary>
	/// <remarks> A duplicate <c>Texture</c> instance should never be present, see <c>TextureIndices</c>. </remarks>
	Texture[] Textures { get; }

	/// <summary> An array with indices to elements in <c>Textures</c>. </summary>
	/// <remarks> 
	/// Index correlates with the first dimension of <c>Faces</c>, and the value is an index for <c>Textures</c>.
	/// </remarks>
	int[] TextureIndices { get; }
}