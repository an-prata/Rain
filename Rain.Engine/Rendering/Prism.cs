// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Geometry;
using Rain.Engine.Texturing;

namespace Rain.Engine.Rendering;

public class Prism
{
	public static IRenderable MakePrism(TexturedFace shapeBase, EfficientTextureGroup[] textures, float lengthZ)
	{
		if (textures.Length != shapeBase.Face.Sides)
			throw new Exception($"{nameof(textures)} should be of length equal to {shapeBase.Face.Sides}");
		
		var options = new SolidOptions()
		{
			LengthX = shapeBase.Face.Width,
			LengthY = shapeBase.Face.Height,
			LengthZ = lengthZ,
			RotationX = 0,
			RotationY = 0,
			RotationZ = 0
		};

		var rotateBackX = shapeBase.Face.RotationX;
		var rotateBackY = shapeBase.Face.RotationY;
		var rotateBackZ = shapeBase.Face.RotationZ;

		shapeBase.Face.Rotate(-shapeBase.Face.RotationX, Axes.X);
		shapeBase.Face.Rotate(-shapeBase.Face.RotationY, Axes.Y);
		shapeBase.Face.Rotate(-shapeBase.Face.RotationZ, Axes.Z);

		shapeBase.CopyTo(out TexturedFace basePrime);
		basePrime.Face.Translate(0, 0, lengthZ);

		var faces = new TexturedFace[textures.Length + 2];

		faces[0] = shapeBase;
		faces[1] = basePrime;

		for (var point = 0; point < shapeBase.Face.Points.Length; point++)
		{
			var adjacentPoint = point == shapeBase.Face.Points.Length - 1 ? 0 : point + 1;

			var facePoints = new Point[4];
			
			shapeBase.Face.Points[point].CopyTo(out facePoints[0]);
			facePoints[0].TextureCoordinate = new(0.0f, 0.0f);

			basePrime.Face.Points[point].CopyTo(out facePoints[1]);
			facePoints[1].TextureCoordinate = new(0.0f, 1.0f);
			
			basePrime.Face.Points[adjacentPoint].CopyTo(out facePoints[2]);
			facePoints[2].TextureCoordinate = new(1.0f, 1.0f);

			shapeBase.Face.Points[adjacentPoint].CopyTo(out facePoints[3]);
			facePoints[3].TextureCoordinate = new(1.0f, 0.0f);

			var face = new TexturedFace(new Rectangle(facePoints), textures[point].ToArray());

			faces[point + 2] = face;
		}

		var solid = new Solid(new(faces), options);

		solid.Rotate(rotateBackX, Axes.X);
		solid.Rotate(rotateBackY, Axes.Y);
		solid.Rotate(rotateBackZ, Axes.Z);

		return solid;
	}
}