using OpenTK.Mathematics;

using static Rain.Engine.VertexHelpers;

namespace Rain.Engine;

public static class VertexExtensions
{
	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex vertex)
		=> new(vertex.X, vertex.Y, vertex.Z);

	/// <summary> Finds the distance between two Vertex<float> instances. </summary>
	/// <returns> The distance between the two vertices. </returns>
	public static double DistanceBetween(this Vertex a, Vertex b)
		=>  GetDistance(a, b);
}