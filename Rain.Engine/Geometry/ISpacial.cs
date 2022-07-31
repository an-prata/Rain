// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry;

/// <summary>
/// An interface used to represent any object in space and allows for the calculation of it location and distance from other
/// ISpacial objects.
/// </summary>
public interface ISpacial
{
	/// <summary>
	/// Gets the location of this <c>ISpacial</c> object, should be the center of any multi-point object.
	/// </summary>
	Vertex Location { get; }

	/// <summary>
	/// Gets the distance between this <c>ISpacial</c> and another.
	/// </summary>
	/// 
	/// <param name="other">
	/// The other <c>ISpacial</c>.
	/// </param>
	/// 
	/// <returns>
	/// The distance between this and the other <c>ISpacial</c>.
	/// </returns>
	double GetDistanceBetween(ISpacial other);
}