// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Graphics.OpenGL;
using Rain.Engine.Buffering;
using Rain.Engine.Geometry.TwoDimensional;

namespace Rain.Engine.Geometry;

/// <summary> 
/// A colored point in 3D space that can be rendered by the GPU. 
/// </summary>
public struct Point : IBufferable, ISpacial
{
	public const int BufferSize = Vertex.BufferSize + Color.BufferSize + TextureCoordinate.BufferSize;

	public Vertex Location { get => Vertex; }

	/// <summary> 
	/// The location of the point in 3D space. 
	/// </summary>
	public Vertex Vertex { get; set; }

	/// <summary> 
	/// The color of the point. 
	/// </summary>
	public Color Color { get; set; }

	/// <summary> 
	/// The point's texture position. 
	/// </summary>
	public TextureCoordinate TextureCoordinate { get; set; }

	/// <summary> 
	/// Creates a new <c>Point</c> from a <c>Vertex</c>. 
	/// </summary>
	/// 
	/// <param name="vertex"> 
	/// The <c>Vertex</c> to creates a <c>Point</c> from. 
	/// </param>
	public Point(Vertex vertex)
	{
		Vertex = vertex;
		Color = new(255, 255, 255);
		TextureCoordinate = new(0.0f, 0.0f);
	}

	/// <summary> 
	/// Creates a new <c>Point</c> from a <c>Vertex</c> and <c>Color</c>. 
	/// </summary>
	/// 
	/// <param name="vertex">
	/// The new <c>Point</c>'s <c>Vertex</c>. 
	/// </param>
	/// 
	/// <param name="color"> 
	/// The new <c>Point</c>'s <c>Color</c>. 
	/// </param>
	public Point(Vertex vertex, Color color)
	{
		Vertex = vertex;
		Color = color;
		TextureCoordinate = new(0.0f, 0.0f);
	}

	/// <summary> 
	/// Creates a new <c>Point</c> from a <c>Vertex</c> and <c>TextureCoordinate</c>. 
	/// </summary>
	/// 
	/// <param name="vertex"> 
	/// The new <c>Point</c>'s <c>Vertex</c>. 
	/// </param>
	/// 
	/// <param name="textureCoordinate"> 
	/// The new <c>Point</c>'s <c>TextureCoordinate</c>. 
	/// </param>
	public Point(Vertex vertex, TextureCoordinate textureCoordinate)
	{
		Vertex = vertex;
		Color = new(255, 255, 255);
		TextureCoordinate = textureCoordinate;
	}

	/// <summary> 
	/// Creates a new <c>Point</c> from a <c>Vertex</c>, <c>Color</c>, and <c>TextureCoordinate</c>. 
	/// </summary>
	/// 
	/// <param name="vertex"> 
	/// The new <c>Point</c>'s <c>Vertex</c>. 
	/// </param>
	/// 
	/// <param name="color"> 
	/// The new <c>Point</c>'s <c>Color</c>. 
	/// </param>
	/// 
	/// <param name="textureCoordinate"> 
	/// The new <c>Point</c>'s <c>TextureCoordinate</c>. 
	/// </param>
	public Point(Vertex vertex, Color color, TextureCoordinate textureCoordinate)
	{
		Vertex = vertex;
		Color = color;
		TextureCoordinate = textureCoordinate;
	}

	public double GetDistanceBetween(ISpacial other)
		=> (Location - other.Location).Maginitude;

	public int GetBufferSize(BufferType bufferType)
	{
		if (bufferType == BufferType.VertexBuffer)
			return BufferSize;
		
		return 1;
	}

	public Array GetBufferableArray(BufferType bufferType) 
	{
		if (bufferType != BufferType.VertexBuffer)
			return new uint[] { 0 };

		var vertexData = new float[GetBufferSize(bufferType)];

		for (var i = 0; i < Vertex.Array.Length; i++)
			vertexData[i] = Vertex.Array[i];

		for (var i = 0; i < Color.Array.Length; i++)
			vertexData[i + Vertex.BufferSize] = Color.Array[i];

		for (var i = 0; i < TextureCoordinate.Array.Length; i++)
			vertexData[i + Vertex.BufferSize + Color.BufferSize] = TextureCoordinate.Array[i];

		return vertexData;
	}

	/// <summary> 
	/// Tells OpenGL how to use the data sent through the Vertex Buffer. 
	/// </summary>
	public static void SetAttributes(ShaderProgram shaderProgram)
	{
		// This is only defined here for maintanability purposes, usually I would put it somewhere like the ShaderProgram
		// class but it would be easier to make a hard to fix breaking change to the Point class that way. So this is left
		// here in hopes that if the Point class is ever changed in a way that would change the way Vertex Atttributes
		// are defined it would not be missed.
		GL.EnableVertexAttribArray(shaderProgram.GetAttributeHandleByName("vertexPosition"));
		GL.VertexAttribPointer(shaderProgram.GetAttributeHandleByName("vertexPosition"), Vertex.BufferSize, 
							   VertexAttribPointerType.Float, false, BufferSize * sizeof(float), 0);

		GL.EnableVertexAttribArray(shaderProgram.GetAttributeHandleByName("color"));
		GL.VertexAttribPointer(shaderProgram.GetAttributeHandleByName("color"), Color.BufferSize,
							   VertexAttribPointerType.Float, false, 
							   BufferSize * sizeof(float), 
							   Vertex.BufferSize * sizeof(float));
		
		GL.EnableVertexAttribArray(shaderProgram.GetAttributeHandleByName("texturePosition"));
		GL.VertexAttribPointer(shaderProgram.GetAttributeHandleByName("texturePosition"), TextureCoordinate.BufferSize,
						 	   VertexAttribPointerType.Float, false, 
							   BufferSize * sizeof(float),
							   (Vertex.BufferSize * sizeof(float)) + (Color.BufferSize * sizeof(float)));
	}

	public override int GetHashCode()
		=> GetBufferableArray(BufferType.VertexBuffer).GetHashCode();

	public bool Equals(Point point)
	{
		if (Vertex != point.Vertex)
			return false;
			
		if (Color != point.Color)
			return false;

		if (TextureCoordinate != point.TextureCoordinate)
			return false;

		return true;
	}

	public override bool Equals(object? obj)
	{
		if (obj == null)
			return false;

		if (obj.GetType() != typeof(Point))
			return false;

        return Equals((Point)obj);
	}

	public static bool operator ==(Point a, Point b) => a.Equals(b);

	public static bool operator !=(Point a, Point b) => !a.Equals(b);
}