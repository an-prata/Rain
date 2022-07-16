// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Mathematics;

using static System.Math;

namespace Rain.Engine.Geometry;

public struct TransformMatrix
{
	public float this[int index0, int index1]
	{
		get => Matrix[index0, index1];
		set => Matrix[index0, index1] = value;
	}
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

	public static TransformMatrix CreateTranslationMatrix(Vertex translation)
		=> CreateTranslationMatrix(translation.X, translation.Y, translation.Z);

	public static TransformMatrix CreateRotationMatrix(float angle, Axes axis)
	{
		var matrix = new float[Size, Size];
		var sinTheta = (float)Sin(Angle.DegreesToRadians(angle));
		var cosTheta = (float)Cos(Angle.DegreesToRadians(angle));

		if (axis == Axes.X)
		{
			matrix = new float[,]
			{
				{ 1.0f,	0.0f, 		0.0f,		0.0f },
				{ 0.0f,	cosTheta, 	-sinTheta, 	0.0f },
				{ 0.0f,	sinTheta, 	cosTheta, 	0.0f },
				{ 0.0f,	0.0f, 		0.0f, 		1.0f }
			};
		}
		else if (axis == Axes.Y)
		{
			matrix = new float[,]
			{
				{ cosTheta,		0.0f, 	sinTheta, 	0.0f },
				{ 0.0f,			1.0f, 	0.0f,		0.0f },
				{ -sinTheta,	0.0f, 	cosTheta, 	0.0f },
				{ 0.0f,			0.0f, 	0.0f, 		1.0f }
			};
		}
		else if (axis == Axes.Z)
		{
			matrix = new float[,]
			{
				{ cosTheta,	-sinTheta, 	0.0f,	0.0f },
				{ sinTheta,	cosTheta, 	0.0f, 	0.0f },
				{ 0.0f,		0.0f, 		1.0f, 	0.0f },
				{ 0.0f,		0.0f, 		0.0f, 	1.0f }
			};
		}

		return new TransformMatrix(matrix, TransformType.Rotation);
	}

	public static TransformMatrix CreatePerspectiveProjectionMatrix(float fovX, float fovY, float nearClip, float farClip)
	{
		var upperBound = nearClip * Tan(0.5f * fovY);
		var lowerBound = -upperBound;
		
		var rightBound = nearClip * Tan(0.5f * fovX);
		var leftBound = -rightBound;

		var x = 2.0f * nearClip / (rightBound - leftBound);
		var y = 2.0f * nearClip / (upperBound - lowerBound);

		var z0 = (rightBound + leftBound) / (rightBound - leftBound);
		var z1 = (upperBound + lowerBound) / (upperBound - lowerBound);
		var z2 = -(farClip + nearClip) / (farClip - nearClip);

		var w = -(2.0f * farClip * nearClip) / (farClip - nearClip);

		var matrix = new float[,]
		{
			{ (float)x,		0.0f, 		0.0f,	0.0f	},
			{ 0.0f,			(float)y,	0.0f, 	0.0f	},
			{ (float)z0,	(float)z1, 	z2, 	-1.0f	},
			{ 0.0f,			0.0f,		w, 		0.0f	}
		};

		return new TransformMatrix(matrix, TransformType.Perspective);
	}

	public Matrix4 ToOpenGLMatrix4()
	{
		return new(
			Matrix[0,0], Matrix[0,1], Matrix[0,2], Matrix[0,3],
			Matrix[1,0], Matrix[1,1], Matrix[1,2], Matrix[1,3],
			Matrix[2,0], Matrix[2,1], Matrix[2,2], Matrix[2,3],
			Matrix[3,0], Matrix[3,1], Matrix[3,2], Matrix[3,3]
		);
	}

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
		var vertex = new float[]
		{
			(a.Matrix[0, 0] * b.X) + (a.Matrix[0, 1] * b.Y) + (a.Matrix[0, 2] * b.Z) + (a.Matrix[0, 3] * b.W),
			(a.Matrix[1, 0] * b.X) + (a.Matrix[1, 1] * b.Y) + (a.Matrix[1, 2] * b.Z) + (a.Matrix[1, 3] * b.W),
			(a.Matrix[2, 0] * b.X) + (a.Matrix[2, 1] * b.Y) + (a.Matrix[2, 2] * b.Z) + (a.Matrix[2, 3] * b.W),
			(a.Matrix[3, 0] * b.X) + (a.Matrix[3, 1] * b.Y) + (a.Matrix[3, 2] * b.Z) + (a.Matrix[3, 3] * b.W),
		};

		return new Vertex(vertex);
	}

	public static Vertex operator *(Vertex a, TransformMatrix b) => b * a;

	public static ITwoDimensional operator *(ITwoDimensional a, TransformMatrix b) => b * a;

	public static ITwoDimensional operator *(TransformMatrix a, ITwoDimensional b)
	{
		for (var i = 0; i < b.Points.Length; i++)
			b.Points[i].Vertex *= a;

		return b;
	}

	public static bool operator ==(TransformMatrix a, TransformMatrix b)
	{
		for (var row = 0; row < Size; row++)
			for (var collum = 0; collum < Size; collum++)
				if (a.Matrix[row, collum] != b.Matrix[row, collum])
					return false;

		return true;
	}

	public static bool operator !=(TransformMatrix a, TransformMatrix b) => !(a == b);
}