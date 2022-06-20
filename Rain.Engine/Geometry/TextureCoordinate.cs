namespace Rain.Engine.Geometry;

/// <summary> Represents coordinates for texturing an object. </summary>
public class TextureCoordinate
{
	/// <summary> The length of any array outputed by <c>TextureCoordinate.Array</c>. </summary>
	public const int BufferSize = 2;

	/// <summary> The TextureCoordinate's X cooridinate. </summary>
	public float X { get; set; }

	/// <summary> The TextureCoordinate's Y cooridinate. </summary>
	public float Y { get; set; }

	public float[] Array { get => new float[BufferSize] { X, Y }; }

	/// <summary> Creates a new TextureCoordinate from X and Y cooridinates. </summary>
	/// <param name="x"> The X coordinate. </param>
	/// <param name="y"> The Y coordinate. </param>
	public TextureCoordinate(float x, float y)
	{
		X = x;
		Y = y;
	}

	/// <summary> Creates a new coordinate from an array of floats. </summary>
	/// <remarks> Note that this array must have length equal to <c>TextureCoordinate.BufferSize</c>. </remarks>
	/// <param name="coordinateArray"> The array to assemble a TextureCoordinate from. </param>
	public TextureCoordinate(float[] coordinateArray)
	{
		X = coordinateArray[0];
		Y = coordinateArray[1];
	}

	public override int GetHashCode()
		=> Array.GetHashCode();

	public override bool Equals(object? obj)
	{
		if (obj == null)
			return false;

		if (obj.GetType() != typeof(TextureCoordinate))
			return false;

        return (TextureCoordinate)obj == this;
	}

	public static TextureCoordinate operator +(TextureCoordinate a, TextureCoordinate b) => new(a.X + b.X, a.Y + b.Y);

	public static TextureCoordinate operator -(TextureCoordinate a, TextureCoordinate b) => new(a.X - b.X, a.Y - b.Y);

	public static TextureCoordinate operator *(TextureCoordinate a, TextureCoordinate b) => new(a.X * b.X, a.Y * b.Y);

	public static TextureCoordinate operator /(TextureCoordinate a, TextureCoordinate b) => new(a.X / b.X, a.Y / b.Y);

	public static bool operator ==(TextureCoordinate a, TextureCoordinate b)
	{
		for (var i = 0; i < BufferSize; i++)
			if (a.Array[i] != b.Array[i])
				return false;
	
		return true;
	}

	public static bool operator !=(TextureCoordinate a, TextureCoordinate b)
	{
		for (var i = 0; i < BufferSize; i++)
			if (a.Array[i] != b.Array[i])
				return !false;
	
		return !true;
	}
}