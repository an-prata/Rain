using System.Numerics;

namespace Rain.Engine;

public interface IModel<T> where T : INumber<T>
{
	/// <summary> The location of the IModel in 3D space. </summary>
	Vertex<T> Location { get; }

	/// <summary> An array of points representing the object. </summary>
	Point<T>[] Points { get; }

	/// <summary> An array of indices in groups of three, correlating to triangles to draw the model. </summary>
	uint[] Elements { get; }

	/// <summary> The length of the model along the X axis. </summary>
	INumber<T> Width
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
	INumber<T> Height
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
	INumber<T> Length
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

	/// <summary> An array representing the vertex data of the model. </summary>
	T[] Array
	{
		get
		{
			T[] vertexData = new T[Points.Length * Point<T>.SizeInT];

			for (var i = 0; i < Points.Length; i += Point<T>.SizeInT)
				for (var y = 0; y < Point<T>.SizeInT; y ++)
					vertexData[i + y] = Points[i].Array[y];

			return vertexData;
		}
	}
}