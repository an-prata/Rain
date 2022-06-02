using OpenTK.Mathematics;

namespace Rain.Engine.Graphics.Renderables;

class Cube : IRenderable
{
	public float LengthX { get => GetSideLength(); }

	public float LengthY { get => GetSideLength(); }

	public float LengthZ { get => GetSideLength(); }

	public Vector3 Position { get => new(Vertices[0], Vertices[1], Vertices[3]); }

	public float[] Vertices { get; private set; }

	/// <summary>
	/// Creates a cube from a side length and position.
	/// </summary>
	/// <param name="sideLength">Length of each of the cube's sides.</param>
	/// <param name="position">
	/// The cubes position, will be the vertex with the smallest values for all of X, Y, and Z.
	/// </param>
	public Cube(Vector3 position, float sideLength)
	{
		Vertices = Assemble(position, sideLength);
	}

	/// <summary>Creates a cube from an array of vertices.</summary>
	/// <remarks>
	/// List MUST be a valid cube, meaning 8 vertices, 24 elements,
	/// and that there should only ever be 6 different values in the array. If any of these are not the case
	/// you may encounter unexpected behavior, as an exception is not neccassarily thrown. It is recommended
	/// that you use <c>Cube(Vector3 position, float sideLength)</c>.
	/// </remarks>
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

		// For the sake of having a sorted array of vertices we reassemble the cube.
		// All length properties and tthe position property rely on this array being sorted to return an accurate value.
		// This is done to save memory on redundant information but means that it's organization must follow strict rules.
		Vertices = Assemble(new(positionX, positionY, positionZ), sideLength);
	}

	/// <summary>
	/// Resizes the cube by adding <c>resizeFactor</c> to specific elements (use negative value to decrease size).
	/// </summary>
	/// <param name="resizeFactor">The factor to change cube size by.</param>
	public void Resize(float resizeFactor)
	{
		Vertices = AddSideLength(resizeFactor);
	}

	/// <summary>
	/// Assembles a sorted array of floats representing vertices that would be rendered as a cube.
	/// </summary>
	/// <param name="position">The position of the cube.</param>
	/// <param name="sideLength">Side length of the cube.</param>
	/// <returns>A sorted array of floats representing vertices that would be rendered as a cube.</returns>
	private static float[] Assemble(Vector3 position, float sideLength) => new float[]
	{
		position.X, 				position.Y, 				position.Z,
		position.X + sideLength,	position.Y,					position.Z,
		position.X, 				position.Y + sideLength, 	position.Z,
		position.X, 				position.Y, 				position.Z + sideLength,
		position.X + sideLength,	position.Y + sideLength,	position.Z,
		position.X,					position.Y + sideLength,	position.Z + sideLength,
		position.X + sideLength,	position.Y,					position.Z + sideLength,
		position.X + sideLength,	position.Y + sideLength,	position.Z + sideLength
	};

	/// <summary>
	/// Resizes the cube by adding <c>sideLength</c> to specific elements.
	/// </summary>
	/// <param name="sideLength">The factor to change cube size by.</param>
	/// <returns>A new array of vertices to render a cube from.</returns>
	private float[] AddSideLength(float sideLength) => new float[]
	{
		Vertices[0], 				Vertices[1], 				Vertices[2],
		Vertices[3] + sideLength,	Vertices[4],				Vertices[5],
		Vertices[6], 				Vertices[7] + sideLength, 	Vertices[8],
		Vertices[9], 				Vertices[10], 				Vertices[11] + sideLength,
		Vertices[12] + sideLength,	Vertices[13] + sideLength,	Vertices[14],
		Vertices[15],				Vertices[16] + sideLength,	Vertices[17] + sideLength,
		Vertices[18] + sideLength,	Vertices[19],				Vertices[20] + sideLength,
		Vertices[21] + sideLength,	Vertices[22] + sideLength,	Vertices[23] + sideLength
	};

	/// <summary>
	/// Gets the side length of the cube using the sorted float array <c>Vertices</c>.
	/// </summary>
	/// <returns>The cubes side length.</returns>
	private float GetSideLength() => Vertices[3] - Vertices[0];
}