using OpenTK.Mathematics;

namespace Rain.Engine.Extensions;

public static class ColorExtensions
{
	/// <summary> Creates a new OpenTK Color4 instance from an already existing Color instance. </summary>
	/// <returns> An equivilant OpenTK Color4 instance.</returns>
	public static Color4 ToColor4(this Color color)
		=> new(color.R, color.G, color.B, color.A);
}