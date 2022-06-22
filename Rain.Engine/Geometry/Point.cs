namespace Rain.Engine.Geometry;

/// <summary> A colored point in 3D space that can be rendered by the GPU. </summary>
public struct Point
{
	/// <summary> The length of any array outputed by <c>Point<T>.Array</c>. </summary>
	public const int BufferSize = Color.BufferSize + Vertex.BufferSize + TextureCoordinate.BufferSize;

	/// <summary> The location of the point in 3D space. </summary>
	public Vertex Vertex { get; set; }

	/// <summary> The color of the point. </summary>
	public Color Color { get; set; }

	/// <summary> The point's texture position. </summary>
	public TextureCoordinate TextureCoordinate { get; set; }

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

			for (var i = 0; i < TextureCoordinate.Array.Length; i++)
				vertexData[i + Vertex.BufferSize + Color.BufferSize] = TextureCoordinate.Array[i];

			return vertexData;
		}
	}

	/// <summary> Creates a new <c>Point</c> from a <c>Vertex</c>. </summary>
	/// <param name="vertex"> The <c>Vertex</c> to creates a <c>Point</c> from. </param>
	public Point(Vertex vertex)
	{
		Vertex = vertex;
		Color = new(180, 164, 240);
		TextureCoordinate = new(0.0f, 0.0f);
	}

	/// <summary> Creates a new <c>Point</c> from a <c>Vertex</c> and <c>Color</c>. </summary>
	/// <param name="vertex"> The new <c>Point</c>'s <c>Vertex</c>. </param>
	/// <param name="color"> The new <c>Point</c>'s <c>Color</c>. </param>
	public Point(Vertex vertex, Color color)
	{
		Vertex = vertex;
		Color = color;
		TextureCoordinate = new(0.0f, 0.0f);
	}

	/// <summary> Creates a new <c>Point</c> from a <c>Vertex</c> and <c>TextureCoordinate</c>. </summary>
	/// <param name="vertex"> The new <c>Point</c>'s <c>Vertex</c>. </param>
	/// <param name="textureCoordinate"> The new <c>Point</c>'s <c>TextureCoordinate</c>. </param>
	public Point(Vertex vertex, TextureCoordinate textureCoordinate)
	{
		Vertex = vertex;
		Color = new(180, 160, 240);
		TextureCoordinate = textureCoordinate;
	}

	/// <summary> Creates a new <c>Point</c> from a <c>Vertex</c>, <c>Color</c>, and <c>TextureCoordinate</c>. </summary>
	/// <param name="vertex"> The new <c>Point</c>'s <c>Vertex</c>. </param>
	/// <param name="color"> The new <c>Point</c>'s <c>Color</c>. </param>
	/// <param name="textureCoordinate"> The new <c>Point</c>'s <c>TextureCoordinate</c>. </param>
	public Point(Vertex vertex, Color color, TextureCoordinate textureCoordinate)
	{
		Vertex = vertex;
		Color = color;
		TextureCoordinate = textureCoordinate;
	}
}