using OpenTK.Mathematics;

namespace Rain.Engine.Graphics.Renderables;

class Cube : IRenderable
{
	public float LengthX { get; }

	public float LengthY { get; }

	public float LengthZ { get; }

	public Vector3 Position { get; }

	public float[] Vertices { get; }

	/// <summary>
	/// Creates a cube from a side length and position.
	/// </summary>
	/// <param name="sideLength">Length of each of the cube's sides.</param>
	/// <param name="position">
	/// The cubes position, will be the vertex with the smallest values for all of X, Y, and Z.
	/// </param>
	public Cube(float sideLength, Vector3 position)
	{
		LengthX = sideLength;
		LengthY = sideLength;
		LengthZ = sideLength;
		Position = position;
		Vertices = AssembleCube();
	}

	/// <summary>
	/// Creates a cube from an array of vertices. List MUST be a valid cube, meaning 8 vertices, 24 elements,
	/// and that there should only ever be 6 different values in the array. If any of these are not the case
	/// you may encounter unexpected behavior, as an exception is not neccassarily thrown. It is recommended
	/// that you use <c>Cube(float sideLength, Vector3 position)</c>.
	/// </summary>
	/// <param name="vertices">The array of vertices to create a cube from.</param>
	public Cube(float[] vertices)
	{
		float positionX = vertices[0];
		float positionY = vertices[1];
		float positionZ = vertices[2];

		float sideLength = 0;

		for (var i = 3; i < vertices.Length; i += 3)
		{
			if (positionX != vertices[i])
			{
				sideLength = positionX > vertices[i] ? positionX - vertices[i] : vertices[i] = positionX;
				positionX = positionX > vertices[i] ? vertices[i] : positionX;
			}
		}

		for (var i = 4; i < vertices.Length; i += 3)
			if (positionY != vertices[i])
				positionY = positionY > vertices[i] ? vertices[i] : positionY;

		for (var i = 5; i < vertices.Length; i += 3)
			if (positionZ != vertices[i])
				positionZ = positionZ > vertices[i] ? vertices[i] : positionZ;

		LengthX = sideLength;
		LengthY = sideLength;
		LengthZ = sideLength;

		Position = new Vector3(positionX, positionY, positionZ);

		// For the sake of having a sorted list of vertices.
		Vertices = AssembleCube();
	}

	private float[] AssembleCube() => new float[]
	{
		Position.X, 			Position.Y, 			Position.Z,
		Position.X + LengthX,	Position.Y,				Position.Z,
		Position.X, 			Position.Y + LengthY, 	Position.Z,
		Position.X, 			Position.Y, 			Position.Z + LengthZ,
		Position.X + LengthX,	Position.Y + LengthY,	Position.Z,
		Position.X,				Position.Y + LengthY,	Position.Z + LengthZ,
		Position.X + LengthX,	Position.Y,				Position.Z + LengthZ,
		Position.X + LengthX,	Position.Y + LengthY,	Position.Z + LengthZ
	};
}