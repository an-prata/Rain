// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Geometry;
using Rain.Engine.Texturing;

namespace Rain.Engine.Rendering;

public class Pyramid : RenderableBase
{
	public override TexturedFaceGroup Faces { get; }

	/// <summary>
	/// Creates a new <c>Pyramid</c> from a base, height, and <c>Texture</c>s for each new face.
	/// </summary>
	/// 
	/// <param name="shapeBase">
	/// The base of the <c>Pyramid</c>.
	/// </param>
	/// 
	/// <param name="textures">
	/// <c>Texture</c>s to use on ecah newly created face.
	/// </param>
	/// 
	/// <param name="lengthZ">
	/// The length along the Z axis for this <c>Pyramid</c>.
	/// </param>
	public Pyramid(TexturedFace shapeBase, EfficientTextureGroup[] textures, float lengthZ)
	{
		if (textures.Length != shapeBase.Face.Sides)
			throw new Exception($"{nameof(textures)} should be of length equal to {shapeBase.Face.Sides}");

		var rotateBackX = shapeBase.Face.RotationX;
		var rotateBackY = shapeBase.Face.RotationY;
		var rotateBackZ = shapeBase.Face.RotationZ;

		shapeBase.Face.Rotate(-shapeBase.Face.RotationX, Axes.X);
		shapeBase.Face.Rotate(-shapeBase.Face.RotationY, Axes.Y);
		shapeBase.Face.Rotate(-shapeBase.Face.RotationZ, Axes.Z);

		var faces = new TexturedFace[textures.Length + 1];

		faces[0] = shapeBase;

		var translateUp = TransformMatrix.CreateTranslationMatrix(0, 0, lengthZ);
		var tip = new Point(shapeBase.Face.Location * translateUp);

		for (var point = 0; point < shapeBase.Face.Points.Length; point++)
		{
			var adjacentPoint = point == shapeBase.Face.Points.Length - 1 ? 0 : point + 1;

			var facePoints = new Point[3];
			
			shapeBase.Face.Points[point].CopyTo(out facePoints[0]);
			facePoints[0].TextureCoordinate = new(1.0f, 0.0f);

			tip.CopyTo(out facePoints[1]);
			facePoints[1].TextureCoordinate = new(0.5f, 1.0f);
			
			shapeBase.Face.Points[adjacentPoint].CopyTo(out facePoints[2]);
			facePoints[2].TextureCoordinate = new(0.0f, 0.0f);

			var face = new TexturedFace(new Triangle(facePoints), textures[point].ToArray());

			faces[point + 1] = face;
		}

		Faces = new(faces);

		Rotate(rotateBackX, Axes.X);
		Rotate(rotateBackY, Axes.Y);
		Rotate(rotateBackZ, Axes.Z);
	}
}