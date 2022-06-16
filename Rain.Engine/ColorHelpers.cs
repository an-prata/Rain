using OpenTK.Mathematics;

namespace Rain.Engine;

public static class ColorHelpers
{
	public static Color4 ColorToColor4(Color<float> color) 
		=> new(color.R, color.G, color.B, color.A);

	public static Color4 ColorToColor4(Color<double> color) 
		=> new((float)color.R, (float)color.G, (float)color.B, (float)color.A);

	public static Color4 ColorToColor4(Color<decimal> color) 
		=> new((float)color.R, (float)color.G, (float)color.B, (float)color.A);

	public static Color4 ColorToColor4(Color<byte> color)
	{
		float r = (float)color.R / byte.MaxValue;
		float g = (float)color.G / byte.MaxValue;
		float b = (float)color.B / byte.MaxValue;
		float a = (float)color.A / byte.MaxValue;

		return new(r, g, b, a);
	}

	public static Color4 ColorToColor4(Color<ushort> color)
	{
		float r = (float)color.R / ushort.MaxValue;
		float g = (float)color.G / ushort.MaxValue;
		float b = (float)color.B / ushort.MaxValue;
		float a = (float)color.A / ushort.MaxValue;

		return new(r, g, b, a);
	}

	public static Color4 ColorToColor4(Color<uint> color)
	{
		float r = (float)color.R / uint.MaxValue;
		float g = (float)color.G / uint.MaxValue;
		float b = (float)color.B / uint.MaxValue;
		float a = (float)color.A / uint.MaxValue;

		return new(r, g, b, a);
	}

	public static Color4 ColorToColor4(Color<ulong> color)
	{
		float r = (float)color.R / ulong.MaxValue;
		float g = (float)color.G / ulong.MaxValue;
		float b = (float)color.B / ulong.MaxValue;
		float a = (float)color.A / ulong.MaxValue;

		return new(r, g, b, a);
	}

	public static Color4 ColorToColor4(Color<sbyte> color)
	{
		float r = (float)color.R / sbyte.MaxValue;
		float g = (float)color.G / sbyte.MaxValue;
		float b = (float)color.B / sbyte.MaxValue;
		float a = (float)color.A / sbyte.MaxValue;

		return new(r, g, b, a);
	}

	public static Color4 ColorToColor4(Color<short> color)
	{
		float r = (float)color.R / short.MaxValue;
		float g = (float)color.G / short.MaxValue;
		float b = (float)color.B / short.MaxValue;
		float a = (float)color.A / short.MaxValue;

		return new(r, g, b, a);
	}

	public static Color4 ColorToColor4(Color<int> color)
	{
		float r = (float)color.R / int.MaxValue;
		float g = (float)color.G / int.MaxValue;
		float b = (float)color.B / int.MaxValue;
		float a = (float)color.A / int.MaxValue;

		return new(r, g, b, a);
	}

	public static Color4 ColorToColor4(Color<long> color)
	{
		float r = (float)color.R / ulong.MaxValue;
		float g = (float)color.G / ulong.MaxValue;
		float b = (float)color.B / ulong.MaxValue;
		float a = (float)color.A / ulong.MaxValue;

		return new(r, g, b, a);
	}
}