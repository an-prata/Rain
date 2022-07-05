using Rain.Engine.Geometry;

namespace Rain.Engine.Texturing;

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

	public ITwoDimensional[] Faces { get; }

	public EfficientTextureArrayGroup Textures { get; private set; }

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