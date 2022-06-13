using OpenTK.Mathematics;

using static Rain.Engine.ColorHelpers;

namespace Rain.Engine;

public static class ColorExtensions
{
	public static Color4 ToColor4(this Color<float> color) => ColorToColor4(color);
	
	public static Color4 ToColor4(this Color<double> color) => ColorToColor4(color);
	
	public static Color4 ToColor4(this Color<decimal> color) => ColorToColor4(color);
	
	public static Color4 ToColor4(this Color<byte> color) => ColorToColor4(color);
	
	public static Color4 ToColor4(this Color<ushort> color) => ColorToColor4(color);
		
	public static Color4 ToColor4(this Color<uint> color) => ColorToColor4(color);
		
	public static Color4 ToColor4(this Color<ulong> color) => ColorToColor4(color);
		
	public static Color4 ToColor4(this Color<sbyte> color) => ColorToColor4(color);

	public static Color4 ToColor4(this Color<short> color) => ColorToColor4(color);

	public static Color4 ToColor4(this Color<int> color) => ColorToColor4(color);
		
	public static Color4 ToColor4(this Color<long> color) => ColorToColor4(color);
}