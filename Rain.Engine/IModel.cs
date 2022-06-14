using System.Numerics;

namespace Rain.Engine;

public interface IModel<T> where T : INumber<T>
{
	/// <summary> The location of the IModel in 3D space. </summary>
	public Vertex<T> Location { get; }

	/// <summary> An array of points representing the object. </summary>
	public Point<T>[] Points { get; }

	/// <summary> The length of the model along the X axis. </summary>
	public INumber<T> Width
	{ 
		get
		{
			T greatest = Points[0].Vertex.X;
			T least = Points[0].Vertex.X;

			foreach (var point in Points)
			{
				if (point.Vertex.X > greatest) greatest = point.Vertex.X;
				if (point.Vertex.X < least) least = point.Vertex.X;
			}

			return greatest - least;
		}
	}

	/// <summary> The length of the model along the Y axis. </summary>
	public INumber<T> Height
	{ 
		get
		{
			T greatest = Points[0].Vertex.Y;
			T least = Points[0].Vertex.Y;

			foreach (var point in Points)
			{
				if (point.Vertex.Y > greatest) greatest = point.Vertex.Y;
				if (point.Vertex.Y < least) least = point.Vertex.Y;
			}

			return greatest - least;
		}
	}

	/// <summary> The length of the model along the Z axis. </summary>
	public INumber<T> Length
	{ 
		get
		{
			T greatest = Points[0].Vertex.Z;
			T least = Points[0].Vertex.Z;

			foreach (var point in Points)
			{
				if (point.Vertex.Z > greatest) greatest = point.Vertex.Z;
				if (point.Vertex.Z < least) least = point.Vertex.Z;
			}

			return greatest - least;
		}
	}
}