namespace Rain.Engine.Geometry;

/// <summary>
/// Represents a two dimensional object in three dimensional space.
/// </summary>
public interface ITwoDimensional
{
	/// <summary>
	/// An array of indices in groups of three, correlating to triangles.
	/// </summary>
	uint[] Elements { get; }

	/// <summary>
	/// An array of points representing the object.
	/// </summary>
	Point[] Points { get; set; }

	/// <summary>
	/// The location of the <c>ITwoDimensional</c> in 3D space.
	/// </summary>
	Vertex Location { get; set; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s width.
	/// </summary>
	float Width { get; set; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s height.
	/// </summary>
	float Height { get; set; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s counter-clockwise rotation on the X axis.
	/// </summary>
	float RotationX { get; set; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s counter-clockwise rotation on the Y axis.
	/// </summary>
	float RotationY { get; set; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s counter-clockwise rotation on the Z axis.
	/// </summary>
	float RotationZ { get; set; }

	/// <summary>
	/// Gets the <c>ITwoDimensional</c>'s center point.
	/// </summary>
	///
	/// <returns>
	/// A <c>Vertex</c> positioned at the center of the <c>ITwoDimensional</c>.
	/// </returns>
	public Vertex GetCenterVertex()
	{
		var greatestPointX = 0.0f;
		var leastPointX = 0.0f;
		
		var greatestPointY = 0.0f;
		var leastPointY = 0.0f;

		var greatestPointZ = 0.0f;
		var leastPointZ = 0.0f;

		for (var point = 0; point < Points.Length; point++)
		{
			if (Points[point].Vertex.X > greatestPointX)
				greatestPointX = Points[point].Vertex.X;
			else if (Points[point].Vertex.X < leastPointX)
				leastPointX = Points[point].Vertex.X;
			
			if (Points[point].Vertex.Y > greatestPointY)
				greatestPointY = Points[point].Vertex.Y;
			else if (Points[point].Vertex.Y < leastPointY)
				leastPointY = Points[point].Vertex.Y;

			if (Points[point].Vertex.Z > greatestPointZ)
				greatestPointZ = Points[point].Vertex.Z;
			else if (Points[point].Vertex.Z < leastPointZ)
				leastPointZ = Points[point].Vertex.Z;
		}

		var midPointX = (greatestPointX + leastPointX) / 2;
		var midPointY = (greatestPointY + leastPointY) / 2;
		var midPointZ = (greatestPointZ + leastPointZ) / 2;

		return new Vertex(midPointX, midPointY, midPointZ);
	}

	/// <summary>
	/// Translate the <c>ITwoDimensional</c> through three dimensional space.
	/// </summary>
	/// 
	/// <param name="x">
	/// The amount to translate on the X axis.
	/// </param>
	/// 
	/// <param name="y">
	/// The amount to translate on the Y axis.
	/// </param>
	/// 
	/// <param name="z">
	/// The amount to translate on the Z axis.
	/// </param>
	public void Translate(float x, float y, float z)
		=> Points = (this * TransformMatrix.CreateTranslationMatrix(x, y, z)).Points;

	/// <summary>
	/// Translate the <c>ITwoDimensional</c> through three dimensional space.
	/// </summary>
	/// 
	/// <param name="vertex">
	/// A <c>Vertex</c> who's X, Y, and Z values will be used to translate the <c>ITwoDimensional</c>. 
	/// </param>
	public void Translate(Vertex vertex) => Translate(vertex.X, vertex.Y, vertex.Z);

	/// <summary>
	/// Scale the <c>ITwoDimensional</c> by factors of <c>x</c> and <c>y</c>.
	/// </summary>
	/// 
	/// <param name="x">
	/// Scale factor along the <c>ITwoDimensional</c>'s width.
	/// </param>
	/// 
	/// <param name="y">
	/// Scale factor along the <c>ITwoDimensional</c>'s height.
	/// </param>
	public void Scale(float x, float y)
		=> Points = (this * TransformMatrix.CreateScaleMatrix(x, y, 1)).Points;
	/// <summary>
	/// Rotates the <c>ITwoDimensional</c> about its center.
	/// </summary>
	/// 
	/// <remarks>
	/// Updates the rotation property of the same axis.
	/// </remarks>
	///
	/// <param name="angle">
	/// The angle of rotation.
	/// </param>
	/// 
	/// <param name="axis">
	/// The axis to rotate on.
	/// </param>
	public void Rotate(float angle, Axes axis)
	{
		var center = GetCenterVertex();
		var rotationMatrix = TransformMatrix.CreateRotationMatrix(angle, axis);

		Translate(-center.X, -center.Y, -center.Z);
		Points = (this * rotationMatrix).Points;
		Translate(center.X, center.Y, center.Z);

		switch(axis)
		{
			case Axes.X: 
				RotationX += angle; 
				break;
			
			case Axes.Y: 
				RotationY += angle; 
				break;
			
			case Axes.Z: 
				RotationZ += angle; 
				break;
		}
	}

	/// <summary>
	/// Rotates the <c>ITwoDimensional</c> about its center.
	/// </summary>
	///
	/// <remarks>
	/// Updates the rotation property of the same axis.
	/// </remarks>
	/// 
	/// <param name="angle">
	/// The angle of rotation.
	/// </param>
	///
	/// <param name="axis">
	/// The axis to rotate on.
	/// </param>
	/// 
	/// <param name="direction">
	/// The direction to rotate in.
	/// </param>
	public void Rotate(float angle, Axes axis, RotationDirection direction)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis);
		else
			Rotate(-angle, axis);
	}
}