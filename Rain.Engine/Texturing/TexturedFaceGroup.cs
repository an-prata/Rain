// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Geometry;

namespace Rain.Engine.Texturing;

/// <summary>
/// An efficient group of <c>TexturedFace</c> objects.
/// </summary>
public struct TexturedFaceGroup
{
	public TexturedFace this[int index]
	{
		get => new(Faces[index], Textures[index]);
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

		Faces = faces;
		Textures = new(textures.ToArray());
	}
}