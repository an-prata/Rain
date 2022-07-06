using Rain.Engine.Geometry;

namespace Rain.Engine.Texturing;

/// <summary>
/// An efficient group of <c>TexturedFace</c> objects.
/// </summary>
public struct TexturedFaceGroup
{
	public TexturedFace this[int index]
	{
		get => new()
		{ 
			Face = Faces[index], 
			Textures = Textures[index] 
		};
	}

	public int Length { get => Faces.Length; }

	/// <summary>
	/// The faces to be textured.
	/// </summary>
	public ITwoDimensional[] Faces { get; }

	/// <summary>
	/// An <c>EfficientTextureArrayGroup</c> with an array for each face.
	/// </summary>
	public EfficientTextureArrayGroup Textures { get; private set; }

	/// <summary>
	/// Creates a new <c>TexturedFaceGroup</c> from an array of <c>TexturedFace</c> objects.
	/// </summary>
	/// 
	/// <param name="texturedFaces">
	/// The <c>TexturedFace</c>s to be made into a group.
	/// </param>
	public TexturedFaceGroup(TexturedFace[] texturedFaces)
	{
		var faces = new ITwoDimensional[texturedFaces.Length];
		var textures = new Texture[texturedFaces.Length][];

		for (var face = 0; face < texturedFaces.Length; face++)
		{
			faces[face] = texturedFaces[face].Face;
			textures[face] = texturedFaces[face].Textures;
		}

		Faces = faces.ToArray();
		Textures = new(textures.ToArray());
	}
}