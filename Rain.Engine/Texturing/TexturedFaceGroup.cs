using Rain.Engine.Geometry;

namespace Rain.Engine.Texturing;

public struct TexturedFaceGroup
{
	public ITwoDimensional[] Faces { get; }

	public EfficientTextureGroup Textures { get; }

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
		Textures = new(textures.ToArray());
	}
}