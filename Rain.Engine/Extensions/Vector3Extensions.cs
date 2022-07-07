// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Mathematics;
using Rain.Engine.Geometry;

namespace Rain.Engine.Extensions;

public static class Vector3Extensions
{
	/// <summary> Creates a new Vertex instance from an already existing OpenTK Vector3 instance. </summary>
	/// <returns> A newly created and equivilant Vertex instance. </returns>
	public static Vertex ToVertex(this Vector3 vector)
		=> new(vector.X, vector.Y, vector.Z);
}