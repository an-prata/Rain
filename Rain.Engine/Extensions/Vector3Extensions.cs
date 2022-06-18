using OpenTK.Mathematics;
using Rain.Engine.Geometry;

namespace Rain.Engine.Extensions;

public static class Vector3Extensions
{
	/// <summary> Creates a new Vertex instance from an already existing OpenTK Vector3 instance. </summary>
	/// <returns> A newly created and equivilant Vertex instance. </returns>
	public static Vertex ToVertex(this Vector3 vector)
		=> new(vector.X, vector.Y, vector.Z);
}