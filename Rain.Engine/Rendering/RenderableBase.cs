// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Buffering;
using Rain.Engine.Geometry;
using Rain.Engine.Texturing;

namespace Rain.Engine.Rendering;

/// <summary>
/// The base class to all three dimensional rednerable objects.
/// </summary>
public abstract class RenderableBase : IRenderable, IEquatable<RenderableBase>
{
	private float lengthX = 1.0f;

	private float lengthY = 1.0f;

	private float lengthZ = 1.0f;

	private Angle rotationX;

	private Angle rotationY;

	private Angle rotationZ;

	/// <summary>
	/// The <c>TexturedFace</c> objects this object is composed of.
	/// </summary>
	public abstract TexturedFaceGroup Faces { get; }

	public uint[] Elements => (uint[])GetBufferableArray(BufferType.ElementBuffer);

	public Point[] Points
	{
		get
		{
			var points = new List<Point>();

			for (var face = 0; face < Faces.Length; face++)
				for (var point = 0; point < Faces[face].Points.Length; point++)
					points.Add(Faces[face].Points[point]);
			
			return points.ToArray();
		}

		set
		{
			var pointsWritten = 0;

			for (var face = 0; face < Faces.Length; face++)
			{
				for (var point = 0; point < Faces[face].Points.Length; point++)
					Faces[face].Points[point] = value[pointsWritten + point];

				pointsWritten += Faces[face].Points.Length;
			}
		}
	}

	public Vertex Location
	{
		get => GetCenterVertex();
		set => Translate(value - Location);
	}

	public float LengthX
	{
		get => lengthX;
		set => Scale(value / lengthX, 1, 1);
	}

	public float LengthY 
	{ 
		get => lengthY;
		set => Scale(1, value / lengthY, 1); 
	}

	public float LengthZ 
	{ 
		get => lengthZ;
		set => Scale(1, 1, value / lengthZ); 
	}

	public Angle RotationX 
	{ 
		get => rotationX; 
		set => Rotate(value / rotationX, Axes.X); 
	}

	public Angle RotationY
	{ 
		get => rotationY; 
		set => Rotate(value / rotationY, Axes.Y); 
	}

	public Angle RotationZ 
	{ 
		get => rotationZ; 
		set => Rotate(value / rotationZ, Axes.Z); 
	}

	public RenderableBase(float lengthX, float lengthY, float lengthZ)
	{
		this.lengthX = lengthX;
		this.lengthY = lengthY;
		this.lengthZ = lengthZ;

		rotationX = new() { Radians = 0.0f };
		rotationY = new() { Radians = 0.0f };
		rotationZ = new() { Radians = 0.0f };
	}

	public RenderableBase(float lengthX, float lengthY, float lengthZ, Angle rotationX, Angle rotationY, Angle rotationZ)
	{
		this.lengthX = lengthX;
		this.lengthY = lengthY;
		this.lengthZ = lengthZ;

		this.rotationX = rotationX;
		this.rotationY = rotationY;
		this.rotationZ = rotationZ;
	}

	public Vertex GetCenterVertex()
	{
		var averageVertex = Points[0].Vertex;

		for (var point = 1; point < Points.Length; point++)
			averageVertex += Points[point].Vertex;
		
		averageVertex /= Points.Length;
		return averageVertex;
	}

	public double GetDistanceBetween(ISpacial other)
		=> (Location - other.Location).Maginitude;

	public Array GetBufferableArray(BufferType bufferType)
	{
		if (bufferType == BufferType.VertexBuffer)
		{
			var pointData = new List<float>();

			for (var point = 0; point < Points.Length; point++)
			{
				var pointArray = (float[])Points[point].GetBufferableArray(bufferType);

				for (var i = 0; i < Point.BufferSize; i++)
					pointData.Add(pointArray[i]);
			}

			return pointData.ToArray();
		}

		var points = 0;
		var elementData = new List<uint>();

		for (var face = 0; face < Faces.Length; face++)
		{
			for (var element = 0; element < Faces[face].Elements.Length; element++)
				elementData.Add((uint)points + Faces[face].Elements[element]);
			
			points += Faces[face].Points.Length;
		}

		return elementData.ToArray();
	}

	public int GetBufferSize(BufferType bufferType)
	{
		var bufferSize = 0;

		if (bufferType == BufferType.VertexBuffer)
			for (var face = 0; face < Faces.Length; face++)
				bufferSize += Faces[face].Points.Length * Point.BufferSize;
		else
			for (var face = 0; face < Faces.Length; face++)
				bufferSize += Faces[face].Elements.Length;

		return bufferSize;
	}

	public void Translate(float x, float y, float z)
		=> Points = (this * TransformMatrix.CreateTranslationMatrix(x, y, z)).Points;

	public void Translate(Vertex vertex)
		=> Translate(vertex.X, vertex.Y, vertex.Z);

	public void Scale(float x = 1, float y = 1, float z = 1)
	{
		var transform = TransformMatrix.CreateTranslationMatrix(Location);

		transform *= TransformMatrix.CreateRotationMatrix(-rotationX, Axes.X);
		transform *= TransformMatrix.CreateRotationMatrix(-rotationY, Axes.Y);
		transform *= TransformMatrix.CreateRotationMatrix(-rotationZ, Axes.Z);

		transform *= TransformMatrix.CreateScaleMatrix(x, y, z);

		transform *= TransformMatrix.CreateRotationMatrix(rotationX, Axes.X);
		transform *= TransformMatrix.CreateRotationMatrix(rotationY, Axes.Y);
		transform *= TransformMatrix.CreateRotationMatrix(rotationZ, Axes.Z);
		
		transform *= TransformMatrix.CreateTranslationMatrix(-Location);

		Points = (this * transform).Points;

		lengthX *= x;
		lengthY *= y;
		lengthZ *= z;
	}

	public void Rotate(Angle angle, Axes axis)
		=> Rotate(angle, axis, GetCenterVertex());

	public void Rotate(Angle angle, Axes axis, RotationDirection direction)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis, GetCenterVertex());
		else
			Rotate(-angle, axis, GetCenterVertex());
	}

	public void Rotate(Angle angle, Axes axis, Vertex vertex)
	{
		var transform = TransformMatrix.CreateTranslationMatrix(vertex);
		transform *= TransformMatrix.CreateRotationMatrix(angle, axis);
		transform *= TransformMatrix.CreateTranslationMatrix(-vertex);

		Points = (this * transform).Points;

		switch(axis)
		{
			case Axes.X:
				rotationX += angle;
				break;

			case Axes.Y:
				rotationY += angle;
				break;

			case Axes.Z:
				rotationZ += angle;
				break;
		}
	}

	public void Rotate(Angle angle, Axes axis, RotationDirection direction, Vertex vertex)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis, vertex);
		else
			Rotate(-angle, axis, vertex);
	}

	public override int GetHashCode()
		=> GetBufferableArray(BufferType.VertexBuffer).GetHashCode();

	public bool Equals(RenderableBase? other)
	{
		if (other is null)
			return false;
		
		var thisBufferableArray = (float[])GetBufferableArray(BufferType.VertexBuffer);
		var otherBufferableArray = (float[])other.GetBufferableArray(BufferType.VertexBuffer);

		if (thisBufferableArray.Length != otherBufferableArray.Length)
			return false;

		for (var i = 0; i < thisBufferableArray.Length; i++)
			if (thisBufferableArray[i] != otherBufferableArray[i])
				return false;
	
		return true;
	}

	public override bool Equals(object? obj)
	{
		if (obj == null)
			return false;

		if (obj.GetType() != typeof(Vertex))
			return false;

        return Equals(obj);
	}

	public static RenderableBase operator *(RenderableBase a, TransformMatrix b) => b * a;

	public static RenderableBase operator *(TransformMatrix a, RenderableBase b)
	{
		for (var i = 0; i < b.Points.Length; i++)
			b.Points[i].Vertex *= a;

		return b;
	}

	public static bool operator ==(RenderableBase a, RenderableBase b) => a.Equals(b);

	public static bool operator !=(RenderableBase a, RenderableBase b) => !a.Equals(b);
}