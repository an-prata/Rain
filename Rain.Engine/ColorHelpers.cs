<<<<<<< HEAD
using OpenTK.Mathematics;

=======
>>>>>>> ff5192dbc0ca0f91c892cdfa8c0e64706d0bb738
namespace Rain.Engine;

static class ColorHelpers
{
<<<<<<< HEAD
	public static Color4 ColorToColor4(Color<float> color) 
		=> new(color.R, color.G, color.B, color.A);

	public static Color4 ColorToColor4(Color<double> color) 
		=> new((float)color.R, (float)color.G, (float)color.B, (float)color.A);

	public static Color4 ColorToColor4(Color<decimal> color) 
		=> new((float)color.R, (float)color.G, (float)color.B, (float)color.A);

	public static Color4 ColorToColor4(Color<byte> color)
		=> new(color.R / byte.MaxValue, color.G / byte.MaxValue, color.B / byte.MaxValue, color.A / byte.MaxValue);

	public static Color4 ColorToColor4(Color<ushort> color)
		=> new(color.R / ushort.MaxValue, color.G / ushort.MaxValue, color.B / ushort.MaxValue, color.A / ushort.MaxValue);

	public static Color4 ColorToColor4(Color<uint> color)
		=> new(color.R / uint.MaxValue, color.G / uint.MaxValue, color.B / uint.MaxValue, color.A / uint.MaxValue);

	public static Color4 ColorToColor4(Color<ulong> color)
		=> new(color.R / ulong.MaxValue, color.G / ulong.MaxValue, color.B / ulong.MaxValue, color.A / ulong.MaxValue);

	public static Color4 ColorToColor4(Color<sbyte> color)
	{
		float r = (byte)(Math.Abs(sbyte.MinValue) + color.R) / byte.MaxValue;
		float g = (byte)(Math.Abs(sbyte.MinValue) + color.G) / byte.MaxValue;
		float b = (byte)(Math.Abs(sbyte.MinValue) + color.B) / byte.MaxValue;
		float a = (byte)(Math.Abs(sbyte.MinValue) + color.A) / byte.MaxValue;

		return new(r, g, b, a);
	}

	public static Color4 ColorToColor4(Color<short> color)
	{
		float r = (ushort)(Math.Abs(short.MinValue) + color.R) / ushort.MaxValue;
		float g = (ushort)(Math.Abs(short.MinValue) + color.G) / ushort.MaxValue;
		float b = (ushort)(Math.Abs(short.MinValue) + color.B) / ushort.MaxValue;
		float a = (ushort)(Math.Abs(short.MinValue) + color.A) / ushort.MaxValue;

		return new(r, g, b, a);
	}

	public static Color4 ColorToColor4(Color<int> color)
	{
		float r = (uint)(Math.Abs(int.MinValue) + color.R) / uint.MaxValue;
		float g = (uint)(Math.Abs(int.MinValue) + color.G) / uint.MaxValue;
		float b = (uint)(Math.Abs(int.MinValue) + color.B) / uint.MaxValue;
		float a = (uint)(Math.Abs(int.MinValue) + color.A) / uint.MaxValue;

		return new(r, g, b, a);
	}

	public static Color4 ColorToColor4(Color<long> color)
	{
		float r = (ulong)(Math.Abs(long.MinValue) + color.R) / ulong.MaxValue;
		float g = (ulong)(Math.Abs(long.MinValue) + color.G) / ulong.MaxValue;
		float b = (ulong)(Math.Abs(long.MinValue) + color.B) / ulong.MaxValue;
		float a = (ulong)(Math.Abs(long.MinValue) + color.A) / ulong.MaxValue;

		return new(r, g, b, a);
	}
=======

>>>>>>> ff5192dbc0ca0f91c892cdfa8c0e64706d0bb738
}