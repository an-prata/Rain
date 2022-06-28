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
	/// Translate the <c>ITwoDimensional</c> through three dimensional space.
	/// </summary>
	/// 
	/// <param name="x">
	/// The amount to translate on the X axis.
	/// </param>
	/// 
	/// <param name="y">
	/// The amount to translate on the Y axis.
	/// </param>
	/// 
	/// <param name="z">
	/// The amount to translate on the Z axis.
	/// </param>
	public void Translate(float x, float y, float z);

	/// <summary>
	/// Translate the <c>ITwoDimensional</c> through three dimensional space.
	/// </summary>
	/// 
	/// <param name="vertex">
	/// A <c>Vertex</c> who's X, Y, and Z values will be used to translate the <c>ITwoDimensional</c>. 
	/// </param>
	public void Translate(Vertex vertex);

	/// <summary>
	/// Scale the <c>ITwoDimensional</c> by factors of <c>x</c> and <c>y</c>.
	/// </summary>
	/// 
	/// <param name="x">
	/// Scale factor along the <c>ITwoDimensional</c>'s width.
	/// </param>
	/// 
	/// <param name="y">
	/// Scale factor along the <c>ITwoDimensional</c>'s height.
	/// </param>
	public void Scale(float x, float y);

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