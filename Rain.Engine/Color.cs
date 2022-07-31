// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Mathematics;

namespace Rain.Engine;

/// <summary>
/// Stores an RGBA color, for floating point types all values should be between 0 and 1, for integer types values are just
/// between whatever the minimum and maximum values of that given type are.
/// </summary>
public struct Color
{
	/// <summary> 
	/// The length of any array outputed by <c>Color.Array</c>. 
	/// </summary>
	public const int BufferSize = 4;

	private float r;

	private float g;

	private float b;

	private float a;

	/// <summary> 
	/// The color's red component. 
	/// </summary>
	/// 
	/// <value> 
	/// A value between 0 and 1 representing the brightness of the red pixel. 
	/// </value>
	public float R { get => r; set => r = Math.Clamp(value, 0.0f, 1.0f); }

	/// <summary> 
	/// The color's green component. 
	/// </summary>
	/// 
	/// <value> 
	/// A value between 0 and 1 representing the brightness of the green pixel. 
	/// </value>
	public float G { get => g; set => g = Math.Clamp(value, 0.0f, 1.0f); }
	
	/// <summary> 
	/// The color's blue component. 
	/// </summary>
	/// 
	/// <value> 
	/// A value between 0 and 1 representing the brightness of the blue pixel. 
	/// </value>
	public float B { get => b; set => b = Math.Clamp(value, 0.0f, 1.0f); }
	
	/// <summary> 
	/// The color's alpha component. 
	/// </summary>
	/// 
	/// <value> 
	/// A value between 0 and 1 representing the color's transparency. 
	/// </value>
	public float A { get => a; set => a = Math.Clamp(value, 0.0f, 1.0f); }

	/// <summary> 
	/// An array representing the color. 
	/// </summary>
	/// 
	/// <value> 
	/// A <c>float[]</c> comprised of the <c>R</c>, <c>G</c>, <c>B</c>, and <c>A</c> values in that order. 
	/// </value>
	public float[] Array { get => new float[] {R, G, B, A}; }

	/// <summary> 
	/// Makes a new <c>Color</c> instance with red, green and blue components. 
	/// </summary>
	/// 
	/// <param name="r"> 
	/// The color's red component. 
	/// </param>
	/// 
	/// <param name="g"> 
	/// The color's green component. 
	/// </param>
	/// 
	/// <param name="b"> 
	/// The color's blue component. 
	/// </param>
	public Color(float r, float g, float b)
	{
		this.r = Math.Clamp(r, 0.0f, 1.0f);
		this.g = Math.Clamp(g, 0.0f, 1.0f);
		this.b = Math.Clamp(b, 0.0f, 1.0f);
		a = 1.0f;
	}

	/// <summary> 
	/// Makes a new <c>Color</c> instance with red, green, blue and alpha components. 
	/// </summary>
	/// 
	/// <param name="r"> 
	/// The color's red component. 
	/// </param>
	/// 
	/// <param name="g"> 
	/// The color's green component. 
	/// </param>
	/// 
	/// <param name="b"> 
	/// The color's blue component. 
	/// </param>
	/// 
	/// <param name="a"> 
	/// The color's alpha component. 
	/// </param>
	public Color(float r, float g, float b, float a)
	{
		this.r = Math.Clamp(r, 0.0f, 1.0f);
		this.g = Math.Clamp(g, 0.0f, 1.0f);
		this.b = Math.Clamp(b, 0.0f, 1.0f);
		this.a = Math.Clamp(a, 0.0f, 1.0f);
	}

	/// <summary> 
	/// Makes a new <c>Color</c> instance with red, green, and blue components. 
	/// </summary>
	/// 
	/// <param name="r"> 
	/// The color's red component. 
	/// </param>
	/// 
	/// <param name="g"> 
	/// The color's green component. 
	/// </param>
	/// 
	/// <param name="b"> 
	/// The color's blue component. 
	/// </param>
	public Color(byte r, byte g, byte b)
	{
		this.r = (float)r / byte.MaxValue;
		this.g = (float)g / byte.MaxValue;
		this.b = (float)b / byte.MaxValue;
		this.a = (float)255 / byte.MaxValue;
	}

	/// <summary> 
	/// Makes a new <c>Color</c> instance with red, green, blue and alpha components. 
	/// </summary>
	/// 
	/// <param name="r"> 
	/// The color's red component. 
	/// </param>
	/// 
	/// <param name="g"> 
	/// The color's green component. 
	/// </param>
	/// 
	/// <param name="b"> 
	/// The color's blue component. 
	/// </param>
	/// 
	/// <param name="a"> 
	/// The color's alpha component. 
	/// </param>
	public Color(byte r, byte g, byte b, byte a)
	{
		this.r = (float)r / byte.MaxValue;
		this.g = (float)g / byte.MaxValue;
		this.b = (float)b / byte.MaxValue;
		this.a = (float)a / byte.MaxValue;
	}

	/// <summary>
	/// Creates a new <c>Color</c> from an array of floats.
	/// </summary>
	/// 
	/// <param name="colorArray">
	/// An array with elements representing components in order Red, Green, Blue, then Alpha.
	/// </param>
	public Color(float[] colorArray)
	{
		if (colorArray.Length != BufferSize)
			throw new Exception($"{nameof(colorArray)} must be of length {BufferSize}");
		
		r = Math.Clamp(colorArray[0], 0.0f, 1.0f);
		g = Math.Clamp(colorArray[1], 0.0f, 1.0f);
		b = Math.Clamp(colorArray[2], 0.0f, 1.0f);
		a = Math.Clamp(colorArray[3], 0.0f, 1.0f);
	}

	/// <summary> 
	/// Creates a new OpenTK Color4 instance from an already existing Color instance. 
	/// </summary>
	/// 
	/// <returns> 
	/// An equivilant OpenTK Color4 instance.
	/// </returns>
	public Color4 ToColor4() => new(R, G, B, A);

	public override int GetHashCode()
		=> (R, G, B, A).GetHashCode();

	public bool Equals(Color color)
		=> (R, G, B, A) == (color.R, color.G, color.B, color.A);

	public override bool Equals(object? obj)
	{
		if (obj == null)
			return false;

		if (obj.GetType() != typeof(Color))
			return false;

        return Equals((Color)obj);
	}

	public static Color operator +(Color a, Color b) => new(a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A);

	public static Color operator -(Color a, Color b) => new(a.R - b.R, a.G - b.G, a.B - b.B, a.A - b.A);

	public static Color operator *(Color a, Color b) => new(a.R * b.R, a.G * b.G, a.B * b.B, a.A * b.A);

	public static Color operator /(Color a, Color b) => new(a.R / b.R, a.G / b.G, a.B / b.B, a.A / b.A);

	public static bool operator ==(Color a, Color b) => a.Equals(b);

	public static bool operator !=(Color a, Color b) => !a.Equals(b);
}