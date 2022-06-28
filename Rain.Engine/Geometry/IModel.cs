namespace Rain.Engine.Geometry;

public interface IModel
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
	/// The location of the IModel in 3D space. 
	/// </summary>
	Vertex Location { get; }

	/// <summary> 
	/// The length of the <c>IModel</c> along the X axis. 
	/// </summary>
	float LengthX { get; }

	/// <summary> 
	/// The length of the <c>IModel</c> along the Y axis. 
	/// </summary>
	float LengthY { get; }

	/// <summary> 
	/// The length of the <c>IModel</c> along the Z axis. 
	/// </summary>
	float LengthZ { get; }

	/// <summary> 
	/// Gets the <c>IModel</c>'s center point. 
	/// </summary>
	/// 
	/// <returns> 
	/// A <c>Vertex</c> positioned at the center of the <c>IModel</c>. 
	/// </returns>
	Vertex GetCenterVertex();

	/// <summary> 
	/// Rotates the <c>IModel</c> about its center. 
	/// </summary>
	/// 
	/// <param name="angle"> 
	/// The angle of rotation. 
	/// </param>
	void Rotate(float angle, Axes axis);

	/// <summary> 
	/// Rotates the <c>IModel</c> about its center. 
	/// </summary>
	/// 
	/// <param name="angle"> 
	/// The angle of rotation. 
	/// </param>
	/// 
	/// <param name="direction"> 
	/// The direction to rotate in. 
	/// </param>
	void Rotate(float angle, Axes axis, RotationDirection direction);
}