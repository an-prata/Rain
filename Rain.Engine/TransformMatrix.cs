namespace Rain.Engine;

public struct TransformMatrix
{
	/// <summary> The side size the matrix, every ITransformMatrix should be a Size * Size matrix. </summary>
	public const int Size = 4;

	public float[,] Matrix { get; }

	/// <summary> Creates a new TransformMatrix representing the Identity Matrix. </summary>
	/// <remarks> Multiplying by an Identity Matrix will leave a Vertex unchanged. </remarks>
	public TransformMatrix()
	{
		Matrix = new float[,]
		{
			{ 1.0f, 0.0f, 0.0f, 0.0f,},
			{ 0.0f, 1.0f, 0.0f, 0.0f,},
			{ 0.0f, 0.0f, 1.0f, 0.0f,},
			{ 0.0f, 0.0f, 0.0f, 1.0f,},
		};
	}

	public TransformMatrix(float[,] matrix)
	{
		if (matrix.Length != Size * Size)
			throw new Exception("2-Dimensional array " + nameof(matrix) + " was not correct size.");
		
		Matrix = matrix;
	}

	/// TODO: Add static methods for creating tranfrom matrices for rotations, scaling, and movement.

	public override int GetHashCode()
		=> Matrix.GetHashCode();

	public override bool Equals(object? obj)
	{
		if (obj == null)
			return false;

		if (obj.GetType() != typeof(TransformMatrix))
			return false;

        return (TransformMatrix)obj == this;
	}

	public static TransformMatrix operator *(TransformMatrix a, TransformMatrix b) 
	{
		var matrix = new float[4,4];
		matrix.Initialize();

		for (var row = 0; row < Size; row++)
			for (var collum = 0; collum < Size; collum++)
				matrix[row, collum] = 0.0f;

		for (var aRow = 0; aRow < Size; aRow++)
			for (var bCollum = 0; bCollum < Size; bCollum++)
				for (var i = 0; i < Size; i++)
					matrix[aRow, bCollum] += a.Matrix[aRow, i] * b.Matrix[i, bCollum];

		var transformMatrix = new TransformMatrix(matrix);
		return transformMatrix;
	}

	public static bool operator ==(TransformMatrix a, TransformMatrix b)
	{
		for (var row = 0; row < Size; row++)
			for (var collum = 0; collum < Size; collum++)
				if (a.Matrix[row, collum] != b.Matrix[row, collum])
					return false;
	
		return true;
	}

	public static bool operator !=(TransformMatrix a, TransformMatrix b)
	{
		for (var row = 0; row < Size; row++)
			for (var collum = 0; collum < Size; collum++)
				if (a.Matrix[row, collum] != b.Matrix[row, collum])
					return !false;
	
		return !true;
	}
}