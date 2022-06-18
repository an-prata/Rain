namespace Rain.Engine;

public struct TransformMatrix
{
	/// <summary> The side size the matrix, every ITransformMatrix should be a Size * Size matrix. </summary>
	public const int Size = 4;

	public TransformType TransformType { get; private set; }

	public float[,] Matrix { get; }

	/// <summary> Creates a new TransformMatrix representing the Identity Matrix. </summary>
	/// <remarks> Multiplying by an Identity Matrix will leave a Vertex unchanged. </remarks>
	public TransformMatrix()
	{
		Matrix = new float[,]
		{
			{ 1.0f, 0.0f, 0.0f, 0.0f },
			{ 0.0f, 1.0f, 0.0f, 0.0f },
			{ 0.0f, 0.0f, 1.0f, 0.0f },
			{ 0.0f, 0.0f, 0.0f, 1.0f }
		};

		TransformType = TransformType.Identity;
	}

	public TransformMatrix(float[,] matrix)
		=> this = new TransformMatrix(matrix, TransformType.Unknown);

	private TransformMatrix(float[,] matrix, TransformType transformType)
	{
		if (matrix.Length != Size * Size)
			throw new Exception("2-Dimensional array " + nameof(matrix) + " was not correct size.");
		
		Matrix = matrix;
		TransformType = transformType;
	}

	public static TransformMatrix CreateScaleMatrix(float xScale, float yScale, float zScale)
	{
		var matrix = new float[,]
		{
			{ xScale,	0.0f,	0.0f,	0.0f },
			{ 0.0f,		yScale,	0.0f, 	0.0f },
			{ 0.0f,		0.0f,	zScale, 0.0f },
			{ 0.0f,		0.0f, 	0.0f, 	1.0f }
		};

		return new TransformMatrix(matrix, TransformType.Scale);
	}

	public static TransformMatrix CreateTranslationMatrix(float xTranslation, float yTranslation, float zTranslation)
	{
		var matrix = new float[,]
		{
			{ 1.0f,	0.0f, 0.0f,	xTranslation },
			{ 0.0f,	1.0f, 0.0f, yTranslation },
			{ 0.0f,	0.0f, 1.0f, zTranslation },
			{ 0.0f,	0.0f, 0.0f, 1.0f		 }
		};

		return new TransformMatrix(matrix, TransformType.Translation);
	}

	// In this state the method could be prone to "Gimbal Lock" (https://en.wikipedia.org/wiki/Gimbal_lock).
	// Potentialy intensive computationaly as well.
	// TODO: Look intto this as potential solution (https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation).
	// There's lots of scary math without numbers, yay... :)
	// This could also be a useful resource: https://math.stackexchange.com/questions/8980/euler-angles-and-gimbal-lock
	// I'm not a mathematition if you couldn't already tell.
	//
	// I have confirmed the gimble lock... damnit. Other than that this works.
	public static TransformMatrix CreateRotationMatrix(float angle, Axes axis)
	{
		var matrix = new float[Size, Size];
		var sinTheta = (float)Math.Sin(DegreesToRadians(angle));
		var cosTheta = (float)Math.Cos(DegreesToRadians(angle));

		if (axis == Axes.X)
			matrix = new float[,]
			{
				{ 1.0f,	0.0f, 		0.0f,		0.0f },
				{ 0.0f,	cosTheta, 	-sinTheta, 	0.0f },
				{ 0.0f,	sinTheta, 	cosTheta, 	0.0f },
				{ 0.0f,	0.0f, 		0.0f, 		1.0f }
			};

		else if (axis == Axes.Y)
			matrix = new float[,]
			{
				{ cosTheta,		0.0f, 	sinTheta, 	0.0f },
				{ 0.0f,			1.0f, 	0.0f,		0.0f },
				{ -sinTheta,	0.0f, 	cosTheta, 	0.0f },
				{ 0.0f,			0.0f, 	0.0f, 		1.0f }
			};

		else if (axis == Axes.Z)
			matrix = new float[,]
			{
				{ cosTheta,	-sinTheta, 	0.0f,	0.0f },
				{ sinTheta,	cosTheta, 	0.0f, 	0.0f },
				{ 0.0f,		0.0f, 		1.0f, 	0.0f },
				{ 0.0f,		0.0f, 		0.0f, 	1.0f }
			};

		return new TransformMatrix(matrix, TransformType.Rotation);
	}

	private static float DegreesToRadians(float angle)
		=> angle * ((float)Math.PI / 180.0f);

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

		if (a.TransformType == b.TransformType)
			return new TransformMatrix(matrix, a.TransformType);

		return new TransformMatrix(matrix, TransformType.Complex);
	}

	public static Vertex operator *(TransformMatrix a, Vertex b) 
	{
		var vertexArray = new float[Vertex.BufferSize];
		vertexArray.Initialize();

		for (var i = 0; i < vertexArray.Length; i++)
			vertexArray[i] = 0;

		for (var row = 0; row < Size; row++)
			for (var collum = 0; collum < b.Array.Length; collum++)
				vertexArray[row] += a.Matrix[row, collum] * b.Array[collum];
		
		return new Vertex(vertexArray);
	}

	public static Vertex operator *(Vertex a, TransformMatrix b) => b * a;

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