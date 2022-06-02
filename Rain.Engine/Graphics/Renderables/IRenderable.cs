using OpenTK.Mathematics;

namespace Rain.Engine.Graphics.Renderables;

interface IRenderable
{
	/// <summary>
	/// Length of the IRenderable on the X axis.
	/// </summary>
	float LengthX { get; }

	/// <summary>
	/// Length of the IRenderable on the Y axis.
	/// </summary>
	float LengthY { get; }

	/// <summary>
	/// Length of the IRenderable on the Z axis.
	/// </summary>
	float LengthZ { get; }

	/// <summary>
	/// Position of the IRenderable, will also be the vertex with the smallest value of X, Y, and Z.
	/// </summary>
	Vector3 Position { get; }

	/// <summary>
	/// The vertices used to assemble the IRenderable.
	/// </summary>
	/// <value>All vertices are in groups of three, for X, Y, and Z positions.</value>
	float[] Vertices { get; }
}