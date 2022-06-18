using OpenTK.Mathematics;

using static System.Math;

namespace Rain.Engine;

public static class VertexHelpers
{
	/// <summary> Creates a new Vertex instance from an OpenTK Vector3. </summary>
	/// <param name="vector"> The Vector3 to create a Vertex from. </param>
	/// <returns> A Vertex created from the Vector3. </returns>
	public static Vertex VertexFromVector3(Vector3 vector)
		=> new(vector.X, vector.Y, vector.Z);

	/// <summary> Finds the distance between two Vertex<float> instances. </summary>
	/// <returns> The distance between the two vertices. </returns>
	public static double GetDistance(Vertex a, Vertex b)
	{
		var difference = a - b;
		return Sqrt(Pow(difference.X, 2) + Pow(difference.Y, 2) + Pow(difference.Z, 2));
	}
}
