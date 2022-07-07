// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry;

public interface IModel : ISpacial
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
	/// The <c>IModel</c>'s counter-clockwise rotation on the X axis.
	/// </summary>
	float RotationX { get; set; }

	/// <summary>
	/// The <c>IModel</c>'s counter-clockwise rotation on the Y axis.
	/// </summary>
	float RotationY { get; set; }

	/// <summary>
	/// The <c>IModel</c>'s counter-clockwise rotation on the Z axis.
	/// </summary>
	float RotationZ { get; set; }

	/// <summary> 
	/// Gets the <c>IModel</c>'s center point. 
	/// </summary>
	/// 
	/// <returns> 
	/// A <c>Vertex</c> positioned at the center of the <c>IModel</c>. 
	/// </returns>
	Vertex GetCenterVertex();
}