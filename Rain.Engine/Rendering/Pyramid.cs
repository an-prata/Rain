using Rain.Engine.Geometry;
using Rain.Engine.Texturing;

namespace Rain.Engine.Rendering;

public class Pyramid
{
	public static IRenderable MakePyramid(TexturedFace shapeBase, EfficientTextureGroup[] textures, float lengthZ)
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

		var faces = new TexturedFace[textures.Length + 1];

		faces[0] = shapeBase;

		var translateUp = TransformMatrix.CreateTranslationMatrix(0, 0, lengthZ);

		for (var point = 0; point < shapeBase.Face.Points.Length; point++)
		{
			var adjacentPoint = point == shapeBase.Face.Points.Length ? 0 : point + 1;

			var facePoints = new Point[]
			{
				shapeBase.Face.Points[point], 
				shapeBase.Face.Points[adjacentPoint],
				new Point(shapeBase.Face.GetCenterVertex() * translateUp)
			};

			var face = new TexturedFace()
			{
				Face = new Triangle(facePoints),
				Textures = textures[point].ToArray()
			};

			faces[point + 1] = face;
		}

		var solid = new Solid(new(faces), options);

		solid.Rotate(rotateBackX, Axes.X);
		solid.Rotate(rotateBackY, Axes.Y);
		solid.Rotate(rotateBackZ, Axes.Z);

		return solid;
	}
}