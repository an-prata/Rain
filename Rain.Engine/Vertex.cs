namespace Rain.Engine;

/// <summary> Represents the location of a point in 3D space. </summary>
public struct Vertex
{
	/// <summary> The length of any array outputed by <c>Vertex.Array</c>. </summary>
	public const int BufferSize = 4;

	/// <summary> The Vertex's X cooridinate. </summary>
	public float X { get; set; }

	/// <summary> The Vertex's Y cooridinate. </summary>
	public float Y { get; set; }

	/// <summary> The Vertex's Z cooridinate. </summary>
	public float Z { get; set; }

	/// <summary> The Vertex's W cooridinate. </summary>
	public float W { get; set; }

	public float[] Array { get => new float[BufferSize] {X, Y, Z, W}; }

	/// <summary> Creates a new Vertex from X, Y, and Z cooridinates. </summary>
	/// <param name="x"> The X coordinate. </param>
	/// <param name="y"> The Y coordinate. </param>
	/// <param name="z"> The Z coordinate. </param>
	public Vertex(float x, float y, float z)
	{
		X = x;
		Y = y;
		Z = z;
		W = 1.0f;
	}

	/// <summary> Creates a new Vertex from X, Y, Z, and W cooridinates. </summary>
	/// <param name="x"> The X coordinate. </param>
	/// <param name="y"> The Y coordinate. </param>
	/// <param name="z"> The Z coordinate. </param>
	/// <param name="w"> The W coordinate. </param>
	public Vertex(float x, float y, float z, float w)
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	public static Vertex operator +(Vertex a, Vertex b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);

	public static Vertex operator -(Vertex a, Vertex b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);

	public static Vertex operator *(Vertex a, Vertex b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);

	public static Vertex operator /(Vertex a, Vertex b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
} 