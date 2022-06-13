using System.Numerics;

namespace Rain.Engine;

/// <summary>
/// Stores an RGBA color, for floating point types all values should be between 0 and 1, for integer types values are just
/// between whatever the minimum and maximum values of that given type are.
/// </summary>
public struct Color<T> where T : INumber<T> 
{
	/// <summary> The color's red component. </summary>
	/// <value> 
	/// A value between 0 and 1 if <c>T</c> is a floating point type and if <c>T</c> is an integer type then a value between 
	/// its minimum and maximum values.
	/// </value>
	public T R { get; set; }

	/// <summary> The color's green component. </summary>
	/// <value> 
	/// A value between 0 and 1 if <c>T</c> is a floating point type and if <c>T</c> is an integer type then a value between 
	/// its minimum and maximum values.
	/// </value>
	public T G { get; set; }
	
	/// <summary> The color's blue component. </summary>
	/// <value> 
	/// A value between 0 and 1 if <c>T</c> is a floating point type and if <c>T</c> is an integer type then a value between 
	/// its minimum and maximum values.
	/// </value>
	public T B { get; set; }
	
	/// <summary> The color's alpha component. </summary>
	/// <value> 
	/// A value between 0 and 1 if <c>T</c> is a floating point type and if <c>T</c> is an integer type then a value between 
	/// its minimum and maximum values.
	/// </value>
	public T A { get; set; }

	/// <summary> An array representing the color. </summary>
	/// <value> A <c>T[]</c> comprised of the <c>R</c>, <c>G</c>, <c>B</c>, and <c>A</c> values in that order. </value>
	public T[] Array { get => new T[] {R, G, B, A}; }

	/// <summary> The unmanaged type used to store color values. </summary>
	public Type Type { get => typeof(T); } 

	/// <summary>
	/// Makes a new <c>Color</c> instance with red, green, blue and alpha components.
	/// </summary>
	/// <param name="r"> The color's red component. </param>
	/// <param name="g"> The color's green component. </param>
	/// <param name="b"> The color's blue component. </param>
	/// <param name="a"> The color's alpha component. </param>
	public Color(T r, T g, T b, T a)
	{
		R = r;
		G = g;
		B = b;
		A = a;
	}
}