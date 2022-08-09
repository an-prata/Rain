// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry;

public enum TransformType
{
	/// <summary>
	/// A matrix that, when multiplied with anything else, leaves it exactly the same.
	/// </summary>
	Identity,

	/// <summary>
	/// A matrix, that when multiplied with points in space, will move them away from or towards the origin, for a group of 
	/// points this will move them away from or towards eachother, scaling the shape to be a different size. 
	/// </summary>
	Scale,

	/// <summary>
	/// A matrix, that when multiplied with a point in space, will move it away from its original position.
	/// </summary>
	Translation,

	/// <summary>
	/// A matrix that, when multiplied with a point in space, will rotate it about the origin along a specified axis. This can 
	/// be combined with a translation to create a rotation around a certain point.
	/// </summary>
	Rotation,

	/// <summary>
	/// A combination of different matrices creatyed by multiplying them together.
	/// </summary>
	Complex,

	/// <summary>
	/// A matrix for giving the illusion of depth, making further objects appear smaller and larger ones appear larger.
	/// </summary>
	Perspective,

	/// <summary>
	/// A matrix of unknown purpose or creation.
	/// </summary>
	Unknown
}