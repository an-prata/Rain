using OpenTK.Mathematics;

namespace Rain.Engine.Graphics.Renderables;

interface IRenderable
{
	/// <summary>
	/// Length of the IRenderable on the X axis.
	/// </summary>
	public float LengthX { get; }

	/// <summary>
	/// Length of the IRenderable on the Y axis.
	/// </summary>
	public float LengthY { get; }

	/// <summary>
	/// Length of the IRenderable on the Z axis.
	/// </summary>
	public float LengthZ { get; }

	/// <summary>
	/// Position of the IRenderable, will also be the vertex with the smallest value of X, Y, and Z.
	/// </summary>
	public Vector3 Position { get; }

	/// <summary>
	/// The vertices used to assemble the IRenderable.
	/// </summary>
	/// <value>All vertices are in groups of three, for X, Y, and Z positions.</value>
	public float[] Vertices { get; }
}