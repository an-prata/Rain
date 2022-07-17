using Rain.Engine.Geometry;
using Rain.Engine.Texturing;

namespace Rain.Engine.Rendering;

public class Pyramid
{
	public static Solid MakePyramid(TexturedFace shapeBase, EfficientTextureGroup[] textures, float lengthZ)
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

		var solid = new Solid(new(faces), options);

		solid.Rotate(rotateBackX, Axes.X);
		solid.Rotate(rotateBackY, Axes.Y);
		solid.Rotate(rotateBackZ, Axes.Z);

		return solid;
	}
}