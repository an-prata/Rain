// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Geometry;
using Rain.Engine.Texturing;

namespace Rain.Engine.Rendering;

public class Prism : RenderableBase
{
	public override TexturedFaceGroup Faces { get; }

	/// <summary>
	/// Creates a new <c>Prism</c> from a base, height, and <c>Texture</c>s for each new face.
	/// </summary>
	/// 
	/// <param name="shapeBase">
	/// The base of the <c>Prism</c>.
	/// </param>
	/// 
	/// <param name="textures">
	/// <c>Texture</c>s to use on ecah newly created face.
	/// </param>
	/// 
	/// <param name="lengthZ">
	/// The length along the Z axis for this <c>Prism</c>.
	/// </param>
	public Prism(TexturedFace shapeBase, EfficientTextureGroup[] textures, float lengthZ) :
		base(shapeBase.Width, shapeBase.Height, lengthZ, shapeBase.RotationX, shapeBase.RotationY, shapeBase.RotationZ)
	{
		if (textures.Length != shapeBase.Sides)
			throw new Exception($"{nameof(textures)} should be of length equal to {shapeBase.Sides}");

		var rotateBackX = shapeBase.RotationX;
		var rotateBackY = shapeBase.RotationY;
		var rotateBackZ = shapeBase.RotationZ;

		shapeBase.Rotate(-shapeBase.RotationX, Axes.X);
		shapeBase.Rotate(-shapeBase.RotationY, Axes.Y);
		shapeBase.Rotate(-shapeBase.RotationZ, Axes.Z);

		shapeBase.CopyTo(out TexturedFace basePrime);
		basePrime.Translate(0, 0, lengthZ);

		var faces = new TexturedFace[textures.Length + 2];

		faces[0] = shapeBase;
		faces[1] = basePrime;

		for (var point = 0; point < shapeBase.Points.Length; point++)
		{
			var adjacentPoint = point == shapeBase.Points.Length - 1 ? 0 : point + 1;
			var facePoints = new Point[4];
			
			shapeBase.Points[point].CopyTo(out facePoints[0]);
			facePoints[0].TextureCoordinate = new(0.0f, 0.0f);

			basePrime.Points[point].CopyTo(out facePoints[1]);
			facePoints[1].TextureCoordinate = new(0.0f, 1.0f);
			
			basePrime.Points[adjacentPoint].CopyTo(out facePoints[2]);
			facePoints[2].TextureCoordinate = new(1.0f, 1.0f);

			shapeBase.Points[adjacentPoint].CopyTo(out facePoints[3]);
			facePoints[3].TextureCoordinate = new(1.0f, 0.0f);

			var face = new TexturedFace(new Rectangle(facePoints), textures[point].ToArray());

			faces[point + 2] = face;
		}

		foreach (var face in faces)
		{
			face.Rotate(rotateBackX, Axes.X, shapeBase.Location);
			face.Rotate(rotateBackY, Axes.Y, shapeBase.Location);
			face.Rotate(rotateBackZ, Axes.Z, shapeBase.Location);
		}

		Faces = new(faces);
	}
}