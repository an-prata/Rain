namespace Rain.Engine;

/// <summary> A colored point in 3D space that can be rendered by the GPU. </summary>
public struct Point
{
	/// <summary> The length of any array outputed by <c>Point<T>.Array</c>. </summary>
	public const int BufferSize = Color.BufferSize + Vertex.BufferSize;

	/// <summary> The color of the point. </summary>
	public Color Color { get; set; }

	/// <summary> The location of the point in 3D space. </summary>
	public Vertex Vertex { get; set; }

	/// <summary> An array representing the Vertex and Color data of this point. </summary>
	public float[] Array 
	{ 
		get 
		{
			var vertexData = new float[BufferSize];

			for (var i = 0; i < Vertex.Array.Length; i++)
				vertexData[i] = Vertex.Array[i];

			for (var i = 0; i < Color.Array.Length; i++)
				vertexData[i + Vertex.BufferSize] = Color.Array[i];

			return vertexData;
		}
	}

	public Point(Vertex vertex, Color color)
	{
		Vertex = vertex;
		Color = color;
	}
}