// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Mathematics;

using static System.Math;

namespace Rain.Engine.Geometry;

public struct TransformMatrix
{
	private readonly float[,] matrix;

	public float this[int index0, int index1]
	{
		get => matrix[index0, index1];
		set => matrix[index0, index1] = value;
	}

	/// <summary> 
	/// The side size the matrix, every ITransformMatrix should be a Size * Size matrix. 
	/// </summary>
	public const int Size = 4;

	/// <summary>
	/// The <c>TransformType</c> associated with this <c>TransformMatrix</c>.
	/// </summary>
	public TransformType TransformType { get; private set; }

	/// <summary> 
	/// Creates a new TransformMatrix representing the Identity Matrix. 
	/// </summary>
	/// 
	/// <remarks> 
	/// Multiplying by an Identity Matrix will leave a Vertex unchanged. 
	/// </remarks>
	public TransformMatrix()
	{
		matrix = new float[,]
		{
			{ 1.0f, 0.0f, 0.0f, 0.0f },
			{ 0.0f, 1.0f, 0.0f, 0.0f },
			{ 0.0f, 0.0f, 1.0f, 0.0f },
			{ 0.0f, 0.0f, 0.0f, 1.0f }
		};

		TransformType = TransformType.Identity;
	}

	/// <summary>
	/// Creates a new <c>TransformMatrix</c> from a two-dimensional float array.
	/// </summary>
	/// 
	/// <param name="matrix">
	/// A two-dimensional float array who's values will be used to create a <c>TransformMatrix</c>.
	/// </param>
	public TransformMatrix(float[,] matrix)
		=> this = new TransformMatrix(matrix, TransformType.Unknown);

	private TransformMatrix(float[,] matrix, TransformType transformType)
	{
		if (matrix.Length != Size * Size)
			throw new Exception("2-Dimensional array " + nameof(matrix) + " was not correct size.");

		this.matrix = matrix;
		TransformType = transformType;
	}

	/// <summary>
	/// Converts this <c>TransformMatrix</c> to an OpenGL Matrix4.
	/// </summary>
	public Matrix4 ToOpenGLMatrix4()
	{
		return new(
			matrix[0,0], matrix[0,1], matrix[0,2], matrix[0,3],
			matrix[1,0], matrix[1,1], matrix[1,2], matrix[1,3],
			matrix[2,0], matrix[2,1], matrix[2,2], matrix[2,3],
			matrix[3,0], matrix[3,1], matrix[3,2], matrix[3,3]
		);
	}

	/// <summary>
	/// Creates a new <c>TransformMatrix</c> that, when multiplied with a shape's <c>Vertex</c> objects with the shape's 
	/// center at the origin, will scale the object by the given values.
	/// </summary>
	/// 
	/// <remarks>
	/// When a <c>Vertex</c> is multiplied by a <c>TransformMatrix</c> returned by <c>CreateScaleMatrix()</c> it will
	/// multiply the X, Y, and Z values of that <c>Vertex</c> by the <c>xScale</c>, <c>yScale</c>, and <c>zScale</c> values
	/// passed into <c>CreateScaleMatrix()</c>.
	/// </remarks>
	/// 
	/// <param name="xScale">
	/// The scale factor to be applied to the <c>Vertex</c>'s X value.
	/// </param>
	/// 
	/// <param name="yScale">
	/// The scale factor to be applied to the <c>Vertex</c>'s Y value.
	/// </param>
	/// 
	/// <param name="zScale">
	/// The scale factor to be applied to the <c>Vertex</c>'s Z value.
	/// </param>
	public static TransformMatrix CreateScale(float xScale, float yScale, float zScale)
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

	/// <summary>
	/// Creates a new <c>TransformMatrix</c> that, when multiplied with a <c>Vertex</c>, will move that <c>Vertex</c> a
	/// distance specified by given values.
	/// </summary>
	/// 
	/// <param name="xTranslation">
	/// The distance to move along the X axis.
	/// </param>
	/// 
	/// <param name="yTranslation">
	/// The distance to move along the Y axis.
	/// </param>
	/// 
	/// <param name="zTranslation">
	/// The distance to move along the Z axis.
	/// </param>
	public static TransformMatrix CreateTranslation(float xTranslation, float yTranslation, float zTranslation)
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

	/// <summary>
	/// Creates a new <c>TransformMatrix</c> that, when multiplied with a <c>Vertex</c>, will move that <c>Vertex</c> a
	/// distance specified by given values.
	/// </summary>
	/// 
	/// <param name="translation">
	/// A <c>Vertex</c> who's X, Y, and Z values will be used as distances to translate any multiplied vertex by on their
	/// respective axis.
	/// </param>
	public static TransformMatrix CreateTranslation(Vertex translation)
		=> CreateTranslation(translation.X, translation.Y, translation.Z);

	/// <summary>
	/// Creates a new <c>TransformMatrix</c> that, when multiplied with a shape's <c>Vertex</c> objects, will rotate the shape
	/// about the origin.
	/// </summary>
	/// 
	/// <param name="angle">
	/// The angle measer to rotate by in radians.
	/// </param>
	/// 
	/// <param name="axis">
	/// The axis to rotate around.
	/// </param>
	public static TransformMatrix CreateRotation(float angle, Axes axis)
	{
		var matrix = new float[Size, Size];
		var sinTheta = (float)Sin(angle);
		var cosTheta = (float)Cos(angle);

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

	/// <summary>
	/// Creates a new <c>TransformMatrix</c> that, when multiplied with a shape's <c>Vertex</c> objects, will rotate the shape
	/// about the origin.
	/// </summary>
	/// 
	/// <param name="angle">
	/// The <c>Angle</c> to rotate by.
	/// </param>
	/// 
	/// <param name="axis">
	/// The axis to rotate around.
	/// </param>
	public static TransformMatrix CreateRotation(Angle angle, Axes axis)
		=> CreateRotation((float)angle.Radians, axis);
	
	/// <summary>
	/// Creates a new <c>TransformMatrix</c> that, when multiplied with a shape's <c>Vertex</c> objects, will rotate the shape
	/// about the origin.
	/// </summary>
	/// 
	/// <param name="x">
	/// The angle to rotate around the x axis.
	/// </param>
	/// 
	/// <param name="y">
	/// The angle to rotate around the y axis.
	/// </param>
	/// 
	/// <param name="z">
	/// The angle to rotate around the z axis.
	/// </param>
	public static TransformMatrix CreateRotation(float x, float y, float z)
	{
		var xRotation = CreateRotation(x, Axes.X);
		var yRotation = CreateRotation(y, Axes.Y);
		var zRotation = CreateRotation(z, Axes.Z);

		return zRotation * yRotation * xRotation;
	}
	
	/// <summary>
	/// Creates a new <c>TransformMatrix</c> that, when multiplied with a shape's <c>Vertex</c> objects, will rotate the shape
	/// about the origin.
	/// </summary>
	/// 
	/// <param name="x">
	/// The angle to rotate around the x axis.
	/// </param>
	/// 
	/// <param name="y">
	/// The angle to rotate around the y axis.
	/// </param>
	/// 
	/// <param name="z">
	/// The angle to rotate around the z axis.
	/// </param>
	public static TransformMatrix CreateRotation(Angle x, Angle y, Angle z)
		=> CreateRotation((float)x.Radians, (float)y.Radians, (float)z.Radians);

	/// <summary>
	/// Creates a new <c>TransformMatrix</c> that, when multiplied with all of a <c>Scene</c>'s points, will make objects
	/// appear smaller when farther away and larger when closer, giving the illusion of depth. 
	/// </summary>
	/// 
	/// <param name="fovX">
	/// The left-right feild of veiw.
	/// </param>
	/// 
	/// <param name="fovY">
	/// The up-down feild of veiw.
	/// </param>
	/// 
	/// <param name="nearClip">
	/// The minimum distance at which objects will be drawn.
	/// </param>
	/// 
	/// <param name="farClip">
	/// The maximum distance at which objects will be drawn.
	/// </param>
	public static TransformMatrix CreatePerspectiveProjection(float fovX, float fovY, float nearClip, float farClip)
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

	/// <summary>
	/// Creates a new <c>TransformMatrix</c> that can be used to convert any <c>Vertex</c> or <c>Points</c>, or set thereof,
	/// from world space, to relative space.
	/// </summary>
	/// 
	/// <param name="location">
	/// The location in world space to be made the center, or origin, of this relative space.
	/// </param>
	/// 
	/// <param name="facing">
	/// A point specifying the direction to face, should be directly in front of <c>location</c> as this point in space will
	/// be made, regardless of its position in world space, to be the origin in relative space (or <c>location</c> in world 
	/// space), but with its Z component being the distance between it and <c>location</c>.
	/// </param>
	/// 
	/// <param name="above">
	/// A point to be made directly above the origin in relative space, much like <c>facing</c>, this point will be made the
	/// origin in relative space (or <c>location</c> in world space), but with its Y component being the distance between it 
	/// and <c>location</c>.
	/// </param>
	public static TransformMatrix CreateRelativeSpace(Vertex location, Vertex facing, Vertex above)
	{
		var column2 = Vertex.Normalize(location - facing);
		var column0 = Vertex.Normalize(Vertex.CrossProduct(above, column2));
		var column1 = Vertex.Normalize(Vertex.CrossProduct(column2, column0));

		var rotations = new TransformMatrix(new float[,] {
			{ column0.X,	column1.X,	column2.X,	0.0f },
			{ column0.Y,	column1.Y,	column2.Y,	0.0f },
			{ column0.Z,	column1.Z,	column2.Z,	0.0f },
			{ 
				-((column0.X * location.X) + (column0.Y * location.Y) + (column0.Z * location.Z)),
				-((column1.X * location.X) + (column1.Y * location.Y) + (column1.Z * location.Z)),
				-((column2.X * location.X) + (column2.Y * location.Y) + (column2.Z * location.Z)),
				1.0f 
			},
		});

		return rotations;
	}

	public override int GetHashCode()
		=> matrix.GetHashCode();

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
		var matrix = new float[,]
		{
			{
				(a[0, 0] * b[0, 0]) + (a[0, 1] * b[1, 0]) + (a[0, 2] * b[2, 0]) + (a[0, 3] * b[3, 0]),
				(a[0, 0] * b[0, 1]) + (a[0, 1] * b[1, 1]) + (a[0, 2] * b[2, 1]) + (a[0, 3] * b[3, 1]),
				(a[0, 0] * b[0, 2]) + (a[0, 1] * b[1, 2]) + (a[0, 2] * b[2, 2]) + (a[0, 3] * b[3, 2]),
				(a[0, 0] * b[0, 3]) + (a[0, 1] * b[1, 3]) + (a[0, 2] * b[2, 3]) + (a[0, 3] * b[3, 3])
			},
			{
				(a[1, 0] * b[0, 0]) + (a[1, 1] * b[1, 0]) + (a[1, 2] * b[2, 0]) + (a[1, 3] * b[3, 0]),
				(a[1, 0] * b[0, 1]) + (a[1, 1] * b[1, 1]) + (a[1, 2] * b[2, 1]) + (a[1, 3] * b[3, 1]),
				(a[1, 0] * b[0, 2]) + (a[1, 1] * b[1, 2]) + (a[1, 2] * b[2, 2]) + (a[1, 3] * b[3, 2]),
				(a[1, 0] * b[0, 3]) + (a[1, 1] * b[1, 3]) + (a[1, 2] * b[2, 3]) + (a[1, 3] * b[3, 3])
			},
			{
				(a[2, 0] * b[0, 0]) + (a[2, 1] * b[1, 0]) + (a[2, 2] * b[2, 0]) + (a[2, 3] * b[3, 0]),
				(a[2, 0] * b[0, 1]) + (a[2, 1] * b[1, 1]) + (a[2, 2] * b[2, 1]) + (a[2, 3] * b[3, 1]),
				(a[2, 0] * b[0, 2]) + (a[2, 1] * b[1, 2]) + (a[2, 2] * b[2, 2]) + (a[2, 3] * b[3, 2]),
				(a[2, 0] * b[0, 3]) + (a[2, 1] * b[1, 3]) + (a[2, 2] * b[2, 3]) + (a[2, 3] * b[3, 3])
			},
			{
				(a[3, 0] * b[0, 0]) + (a[3, 1] * b[1, 0]) + (a[3, 2] * b[2, 0]) + (a[3, 3] * b[3, 0]),
				(a[3, 0] * b[0, 1]) + (a[3, 1] * b[1, 1]) + (a[3, 2] * b[2, 1]) + (a[3, 3] * b[3, 1]),
				(a[3, 0] * b[0, 2]) + (a[3, 1] * b[1, 2]) + (a[3, 2] * b[2, 2]) + (a[3, 3] * b[3, 2]),
				(a[3, 0] * b[0, 3]) + (a[3, 1] * b[1, 3]) + (a[3, 2] * b[2, 3]) + (a[3, 3] * b[3, 3])
			}
		};

		if (a.TransformType == b.TransformType)
			return new TransformMatrix(matrix, a.TransformType);

		return new TransformMatrix(matrix, TransformType.Complex);
	}

	public static Vertex operator *(TransformMatrix a, Vertex b)
	{
		return new Vertex
		(
			(a.matrix[0, 0] * b.X) + (a.matrix[0, 1] * b.Y) + (a.matrix[0, 2] * b.Z) + a.matrix[0, 3],
			(a.matrix[1, 0] * b.X) + (a.matrix[1, 1] * b.Y) + (a.matrix[1, 2] * b.Z) + a.matrix[1, 3],
			(a.matrix[2, 0] * b.X) + (a.matrix[2, 1] * b.Y) + (a.matrix[2, 2] * b.Z) + a.matrix[2, 3]
		);
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
				if (a.matrix[row, collum] != b.matrix[row, collum])
					return false;

		return true;
	}

	public static bool operator !=(TransformMatrix a, TransformMatrix b) => !(a == b);
}