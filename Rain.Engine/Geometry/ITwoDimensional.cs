// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry;

/// <summary>
/// Represents a two dimensional object in three dimensional space.
/// </summary>
public interface ITwoDimensional : ISpacial
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
	/// The number of sides that this <c>ITwoDimensional</c> has.
	/// </summary>
	int Sides { get => Points.Length; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s width.
	/// </summary>
	float Width { get; set; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s height.
	/// </summary>
	float Height { get; set; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s counter-clockwise rotation on the X axis.
	/// </summary>
	float RotationX { get; set; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s counter-clockwise rotation on the Y axis.
	/// </summary>
	float RotationY { get; set; }

	/// <summary>
	/// The <c>ITwoDimensional</c>'s counter-clockwise rotation on the Z axis.
	/// </summary>
	float RotationZ { get; set; }

	/// <summary>
	/// Gets the <c>ITwoDimensional</c>'s center point.
	/// </summary>
	///
	/// <returns>
	/// A <c>Vertex</c> positioned at the center of the <c>ITwoDimensional</c>.
	/// </returns>
	public Vertex GetCenterVertex();

	/// <summary>
	/// Copies this <c>ITwoDimensional</c>'s data to another.
	/// </summary>
	/// 
	/// <param name="twoDimensional">
	/// The <c>ITwoDimensional</c> to copy data to.
	/// </param>
	public void CopyTo(out ITwoDimensional twoDimensional);
	
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
	public void Translate(Vertex vertex) => Translate(vertex.X, vertex.Y, vertex.Z);

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
	public void Rotate(float angle, Axes axis);

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
	public void Rotate(float angle, Axes axis, RotationDirection direction);

	/// <summary>
	/// Rotates the <c>ITwoDimensional</c> about the specified <c>Vertex</c>.
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
	/// <param name="vertex">
	/// The <c>Vertex</c> to rotate about.
	/// </param>
	public void Rotate(float angle, Axes axis, Vertex vertex);

	/// <summary>
	/// Rotates the <c>ITwoDimensional</c> about the specified <c>Vertex</c>.
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
	/// 
	/// <param name="vertex">
	/// The <c>Vertex</c> to rotate about.
	/// </param>
	public void Rotate(float angle, Axes axis, RotationDirection direction, Vertex vertex);
}