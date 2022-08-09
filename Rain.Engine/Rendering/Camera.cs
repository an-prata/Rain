// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Mathematics;
using Rain.Engine.Geometry;

namespace Rain.Engine.Rendering;

/// <summary>
/// A class for creating a transform, computed on the GPU, that is applied to every point in the <c>Scene</c> it is used to
/// render with this. This creates the illision of a mutable view, allowing for movement, change to the viewing angle,
/// expansion or reduction of the feild of view, and changed to the near and far clip planes.
/// </summary>
public class Camera : ISpacial
{
	private Matrix4 openGLMatrix = new();

	public Vertex Location { get; set; }

	/// <summary>
	/// This <c>Camera</c>'s view angle on the X axis.
	/// </summary>
	public Angle RotationX { get; set; }

	/// <summary>
	/// This <c>Camera</c>'s view angle on the Y axis. 
	/// </summary>
	public Angle RotationY { get; set; } = Angle.FromDegrees(270.0);

	public Vertex Facing { get; set; }

	public Vertex Right { get; set; }

	public Vertex Up { get; set; }

	/// <summary>
	/// The total, compound transform defining the way this <c>Camera</c> will affect the render.
	/// </summary>
	public TransformMatrix NegativeCameraTransform 
	{ 
		get
		{
			var x = (float)Math.Cos(RotationX.Radians) * (float)Math.Cos(RotationY.Radians);
			var y = (float)Math.Sin(RotationX.Radians);
			var z = (float)Math.Cos(RotationX.Radians) * (float)Math.Sin(RotationY.Radians);

			Facing = Vertex.Normalize(new(x, y, z));
			Right = Vertex.Normalize(Vertex.CrossProduct(Facing, new(0.0f, 1.0f, 0.0f)));
			Up = Vertex.Normalize(Vertex.CrossProduct(Right, Facing));

			return TransformMatrix.CreateRelativeSpace(Location, Location + Facing, Up);
		}
	}

	public ref Matrix4 OpenGLMatrixCameraTranform 
	{ 
		get
		{
			openGLMatrix = NegativeCameraTransform.ToOpenGLMatrix4();
			return ref openGLMatrix;
		} 
	}

	public Camera(Vertex location)
	{
		Location = location;
	}

	public double GetDistanceBetween(ISpacial other)
		=> (Location - other.Location).Maginitude;
}