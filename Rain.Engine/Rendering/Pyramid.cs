// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Geometry;
using Rain.Engine.Geometry.TwoDimensional;
using Rain.Engine.Texturing;

namespace Rain.Engine.Rendering;

public class Pyramid : RenderableBase
{
	public override IReadOnlyList<Face> Faces { get; }

	/// <summary>
	/// Creates a new <c>Pyramid</c> from a base and height.
	/// </summary>
	/// 
	/// <param name="shapeBase">
	/// The base of the <c>Pyramid</c>.
	/// </param>
	/// 
	/// <param name="lengthZ">
	/// The length along the Z axis for this <c>Pyramid</c>.
	/// </param>
	public Pyramid(Face shapeBase, float lengthZ) :
		base(shapeBase.Width, shapeBase.Height, lengthZ, shapeBase.RotationX, shapeBase.RotationY, shapeBase.RotationZ)
	{
		var rotateBackX = shapeBase.RotationX;
		var rotateBackY = shapeBase.RotationY;
		var rotateBackZ = shapeBase.RotationZ;

		shapeBase.Rotate(-shapeBase.RotationX, Axes.X);
		shapeBase.Rotate(-shapeBase.RotationY, Axes.Y);
		shapeBase.Rotate(-shapeBase.RotationZ, Axes.Z);

		var translateUp = TransformMatrix.CreateTranslationMatrix(0, 0, lengthZ);
		var tip = new Point(shapeBase.Location * translateUp, shapeBase.Points[0].Color, TextureCoordinate.TopMiddle); 

		var faces = new Face[1 + shapeBase.Sides.Length];
		faces[0] = shapeBase;

		for (var point = 0; point < shapeBase.Points.Length; point++)
		{
			var adjacentPoint = point == shapeBase.Points.Length - 1 ? 0 : point + 1;

			var facePoints = new Point[]
			{
				shapeBase.Points[point],
				tip,
				shapeBase.Points[adjacentPoint]
			};

			facePoints[0].TextureCoordinate = TextureCoordinate.BottomRight;
			facePoints[2].TextureCoordinate = TextureCoordinate.BottomLeft;

			var face = new Face(new Triangle(facePoints));
			faces[point + 1] = face;
		}

		foreach (var face in faces)
		{
			face.Rotate(rotateBackX, Axes.X, shapeBase.Location);
			face.Rotate(rotateBackY, Axes.Y, shapeBase.Location);
			face.Rotate(rotateBackZ, Axes.Z, shapeBase.Location);
		}

		Faces = new List<Face>(faces);
	}

	/// <summary>
	/// Creates a new <c>Pyramid</c> from a base, height, and <c>Color</c> for the tip.
	/// </summary>
	/// 
	/// <param name="shapeBase">
	/// The base of the <c>Pyramid</c>.
	/// </param>
	/// 
	/// <param name="color">
	/// <c>Color</c>s to use on the <c>Pyramid</c>'s tip.
	/// </param>
	/// 
	/// <param name="lengthZ">
	/// The length along the Z axis for this <c>Pyramid</c>.
	/// </param>
	public Pyramid(Face shapeBase, float lengthZ, Color color) :
		base(shapeBase.Width, shapeBase.Height, lengthZ, shapeBase.RotationX, shapeBase.RotationY, shapeBase.RotationZ)
	{
		var rotateBackX = shapeBase.RotationX;
		var rotateBackY = shapeBase.RotationY;
		var rotateBackZ = shapeBase.RotationZ;

		shapeBase.Rotate(-shapeBase.RotationX, Axes.X);
		shapeBase.Rotate(-shapeBase.RotationY, Axes.Y);
		shapeBase.Rotate(-shapeBase.RotationZ, Axes.Z);

		var translateUp = TransformMatrix.CreateTranslationMatrix(0, 0, lengthZ);
		var tip = new Point(shapeBase.Location * translateUp, color, TextureCoordinate.TopLeft); 

		var faces = new Face[1 + shapeBase.Sides.Length];
		faces[0] = shapeBase;

		// USE SIDES RATHER THAN POINTS
		for (var side = 0; side < shapeBase.Sides.Length; side++)
		{
			var facePoints = new Point[]
			{
				shapeBase.Sides[side].Item1,
				tip,
				shapeBase.Sides[side].Item2
			};

			facePoints[0].TextureCoordinate = TextureCoordinate.BottomRight;
			facePoints[2].TextureCoordinate = TextureCoordinate.BottomLeft;

			var face = new Face(new Triangle(facePoints));
			faces[side + 1] = face;
		}

		foreach (var face in faces)
		{
			face.Rotate(rotateBackX, Axes.X, shapeBase.Location);
			face.Rotate(rotateBackY, Axes.Y, shapeBase.Location);
			face.Rotate(rotateBackZ, Axes.Z, shapeBase.Location);
		}

		Faces = new List<Face>(faces);
	}

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
	public Pyramid(TexturedFace shapeBase, EfficientTextureGroup[] textures, float lengthZ) :
		base(shapeBase.Width, shapeBase.Height, lengthZ, shapeBase.RotationX, shapeBase.RotationY, shapeBase.RotationZ)
	{
		if (textures.Length != shapeBase.Sides.Length)
			throw new Exception($"{nameof(textures)} should be of length equal to {shapeBase.Sides}");

		var rotateBackX = shapeBase.RotationX;
		var rotateBackY = shapeBase.RotationY;
		var rotateBackZ = shapeBase.RotationZ;

		shapeBase.Rotate(-shapeBase.RotationX, Axes.X);
		shapeBase.Rotate(-shapeBase.RotationY, Axes.Y);
		shapeBase.Rotate(-shapeBase.RotationZ, Axes.Z);

		var translateUp = TransformMatrix.CreateTranslationMatrix(0, 0, lengthZ);
		var tip = new Point(shapeBase.Location * translateUp, shapeBase.Points[0].Color, new(0.5f, 1.0f)); 

		var faces = new TexturedFace[1 + shapeBase.Sides.Length];
		faces[0] = shapeBase;

		for (var side = 0; side < shapeBase.Sides.Length; side++)
		{
			var facePoints = new Point[]
			{
				shapeBase.Sides[side].Item1,
				tip,
				shapeBase.Sides[side].Item2
			};

			facePoints[0].TextureCoordinate = TextureCoordinate.BottomRight;
			facePoints[2].TextureCoordinate = TextureCoordinate.BottomLeft;

			var face = new TexturedFace(new Triangle(facePoints), textures[side].ToArray());
			faces[side + 1] = face;
		}

		foreach (var face in faces)
		{
			face.Rotate(rotateBackX, Axes.X, shapeBase.Location);
			face.Rotate(rotateBackY, Axes.Y, shapeBase.Location);
			face.Rotate(rotateBackZ, Axes.Z, shapeBase.Location);
		}

		Faces = new TexturedFaceGroup(faces);
	}
}