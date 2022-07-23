// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Geometry;
using Rain.Engine.Rendering;

namespace Rain.Engine.Texturing;

/// <summary>
/// A two dimensional face with a set of one or more <c>Textures</c>.
/// </summary>
public class TexturedFace : Face, ITwoDimensional
{
	/// <summary>
	/// The <c>Textures</c> that will be rendered onto <c>Face</c>.
	/// </summary>
	/// 
	/// <remarks>
	/// <c>Texture</c>s are applied in ascending order.
	/// </remarks>
	public Texture[] Textures { get; set; }

	public TexturedFace(TwoDimensionalBase face, Texture[] textures) : base(face)
	{
		Textures = textures;
	}

	private TexturedFace(TexturedFace texturedFace) : base(texturedFace)
	{
		Textures = new Texture[texturedFace.Textures.Length];

		for (var texture = 0; texture < Textures.Length; texture++)
			texturedFace.Textures[texture].CopyTo(out Textures[texture]);
	}

	/// <summary>
	/// Copies data from this <c>TexturedFace</c> to another.
	/// </summary>
	/// 
	/// <param name="face">
	/// The <c>TexturedFace</c> to copy data to.
	/// </param>
	public void CopyTo(out TexturedFace face)
		=> face = new TexturedFace(this);
}