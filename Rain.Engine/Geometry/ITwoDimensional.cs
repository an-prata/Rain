namespace Rain.Engine.Geometry;

public interface ITwoDimensional
{
	/// <summary>
	/// An array of indices in groups of three, correlating to triangles.
	/// </summary>
	uint[] Elements { get; }

	/// <summary>
	/// An array of points representing the object.
	/// </summary>
	Point[] Points { get; }

	/// <summary>
	/// The location of the <c>ITwoDimensional</c> in 3D space.
	/// </summary>
	Vertex Location { get; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s width.
	/// </summary>
	float Width { get; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s height.
	/// </summary>
	float Height { get; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s counter-clockwise rotation on the X axis.
	/// </summary>
	float RotationX { get; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s counter-clockwise rotation on the Y axis.
	/// </summary>
	float RotationY { get; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s counter-clockwise rotation on the Z axis.
	/// </summary>
	float RotationZ { get; }

	/// <summary>
	/// Gets the <c>ITwoDimensional</c>'s center point.
	/// </summary>
	///
	/// <returns>
	/// A <c>Vertex</c> positioned at the center of the <c>ITwoDimensional</c>.
	/// </returns>
	Vertex GetCenterVertex();

	/// <summary>
	/// Rotates the <c>ITwoDimensional</c> about its center.
	/// </summary>
	/// 
	/// <remarks>
	/// Updates the rotation property of the same axis.
	/// </remarks>
	///
	/// <param name="angle">
	/// The angle of rotation.
	/// </param>
	/// 
	/// <param name="axis">
	/// The axis to rotate on.
	/// </param>
	void Rotate(float angle, Axes axis);

	/// <summary>
	/// Rotates the <c>ITwoDimensional</c> about its center.
	/// </summary>
	///
	/// <remarks>
	/// Updates the rotation property of the same axis.
	/// </remarks>
	/// 
	/// <param name="angle">
	/// The angle of rotation.
	/// </param>
	///
	/// <param name="axis">
	/// The axis to rotate on.
	/// </param>
	/// 
	/// <param name="direction">
	/// The direction to rotate in.
	/// </param>
	void Rotate(float angle, Axes axis, RotationDirection direction);
}