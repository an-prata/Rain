using Rain.Engine.Geometry;

namespace Rain.Engine.Texturing;

public struct TexturedFaceGroup
{
	public TexturedFace this[int index]
	{
		get => new()
		{ 
			Face = Faces[index], 
			Texture = Textures[index] 
		};

		set
		{
			Faces[index] = value.Face;
			textures[index] = value.Texture;
		}
	}

	private EfficientTextureGroup textures;

	public int Length { get => Faces.Length; }

	public ITwoDimensional[] Faces { get; }

	public EfficientTextureGroup Textures { get => textures; }

	public TexturedFaceGroup(TexturedFace[] texturedFaces)
	{
		var faces = new List<ITwoDimensional>();
		var textures = new List<Texture>();

		for (var face = 0; face < texturedFaces.Length; face++)
		{
			faces.Add(texturedFaces[face].Face);
			textures.Add(texturedFaces[face].Texture);
		}

		Faces = faces.ToArray();
		this.textures = new(textures.ToArray());
	}
}