// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry;

/// <summary>
/// An interface for transform methods.
/// </summary>
public interface ITransformable
{
	/// <summary>
	/// Translate the <c>ITransformable</c> through three dimensional space.
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
	/// Translate the <c>ITransformable</c> through three dimensional space.
	/// </summary>
	/// 
	/// <param name="vertex">
	/// A <c>Vertex</c> who's X, Y, and Z values will be used to translate the <c>ITransformable</c>. 
	/// </param>
	public void Translate(Vertex vertex) => Translate(vertex.X, vertex.Y, vertex.Z);

	/// <summary>
	/// Scale the <c>ITransformable</c> by factors of <c>x</c> and <c>y</c>.
	/// </summary>
	/// 
	/// <param name="x">
	/// Scale factor along the <c>ITransformable</c>'s x axis.
	/// </param>
	/// 
	/// <param name="y">
	/// Scale factor along the <c>ITransformable</c>'s y axis.
	/// </param>
	/// 
	/// <param name="z">
	/// Scale factor along the <c>ITransformable</c>'s z axis.
	/// </param>
	public void Scale(float x, float y, float z);

	/// <summary>
	/// Rotates the <c>ITransformable</c> about its center.
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
	/// Rotates the <c>ITransformable</c> about its center.
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
	/// Rotates the <c>ITransformable</c> about the specified <c>Vertex</c>.
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
	/// The <c>Vertex</c> to rotate around.
	/// </param>
	public void Rotate(float angle, Axes axis, Vertex vertex);

	/// <summary>
	/// Rotates the <c>ITransformable</c> about the specified <c>Vertex</c>.
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
	/// The <c>Vertex</c> to rotate around.
	/// </param>
	public void Rotate(float angle, Axes axis, RotationDirection direction, Vertex vertex);
}