namespace Rain.Engine.Geometry;

public interface IModel
{
	/// <summary> The location of the IModel in 3D space. </summary>
	Vertex Location { get; }

	/// <summary> An array of points representing the object. </summary>
	Point[] Points { get; }

	/// <summary> An array of indices in groups of three, correlating to triangles. </summary>
	uint[] Elements { get; }

	/// <summary> The length of the model along the X axis. </summary>
	float Width
	{ 
		get
		{
			var greatest = Points[0].Vertex.X;
			var least = Points[0].Vertex.X;

			foreach (var point in Points)
			{
				if (point.Vertex.X > greatest) greatest = point.Vertex.X;
				if (point.Vertex.X < least) least = point.Vertex.X;
			}

			return greatest - least;
		}
	}

	/// <summary> The length of the model along the Y axis. </summary>
	float Height
	{ 
		get
		{
			var greatest = Points[0].Vertex.Y;
			var least = Points[0].Vertex.Y;

			foreach (var point in Points)
			{
				if (point.Vertex.Y > greatest) greatest = point.Vertex.Y;
				if (point.Vertex.Y < least) least = point.Vertex.Y;
			}

			return greatest - least;
		}
	}

	/// <summary> The length of the model along the Z axis. </summary>
	float Length
	{ 
		get
		{
			var greatest = Points[0].Vertex.Z;
			var least = Points[0].Vertex.Z;

			foreach (var point in Points)
			{
				if (point.Vertex.Z > greatest) greatest = point.Vertex.Z;
				if (point.Vertex.Z < least) least = point.Vertex.Z;
			}

			return greatest - least;
		}
	}

	/// <summary> Gets array representing vertex data needed to render the IModel. </summary>
	float[] GetBufferableArray();

	/// <summary> Rotates the IModel about its center. </summary>
	/// <param name="angle"> The angle of rotation. </param>
	void Rotate(float angle, Axes axis);

	/// <summary> Rotates the IModel about its center. </summary>
	/// <param name="angle"> The angle of rotation. </param>
	/// <param name="direction"> The direction to rotate in. </param>
	void Rotate(float angle, Axes axis, RotationDirection direction);
}