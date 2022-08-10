// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Mathematics;
using Rain.Engine.Geometry;

namespace Rain.Engine.Rendering;

public class PerspectiveProjection
{
	private const float DefaultAspectRatio = 1.0f;

	private const float DefaultNearClip = 0.05f;

	private const float DefaultFarClip = 100.0f;

	/// <summary>
	/// A reference to a copy of this <c>PerspectiveProjection</c> as an OpenGL compatable <c>Matrix4</c>.
	/// </summary>
	/// 
	/// <remarks>
	/// Note that while this is a reference, it is a reference to a pre-computed struct, and modifying it will not modify its
	/// correspoding <c>TransformMatrix</c>.
	/// </remarks>
	public virtual ref Matrix4 OpenGLMatrixPerspectiveTransform { get => ref openGLMatrix; }

	private Matrix4 openGLMatrix;

	/// <summary>
	/// The underlying <c>TransformMatrix</c> of this <c>PerspectiveProjection</c>
	/// </summary>
	public TransformMatrix PerspectiveMatrix { get; private set; }

	/// <summary>
	/// The feild of view that this <c>PerspectiveProjection</c> will apply.
	/// </summary>
	public Angle Fov 
	{ 
		get => fov; 
		set
		{
			fov = value;
			PerspectiveMatrix = TransformMatrix.CreatePerspectiveProjection((float)FovX.Radians, (float)FovY.Radians, nearClip, farClip);
		} 
	}

	private Angle fov;

	/// <summary>
	/// The feild of view that this <c>PerspectiveProjection</c> will apply along the X axis.
	/// </summary>
	/// 
	/// <remarks>
	/// This is just the <c>Fov</c> property multiplied by <c>AspectRatio</c>.
	/// </remarks>
	public Angle FovX { get => Fov * AspectRatio; }

	/// <summary>
	/// The feild of view that this <c>PerspectiveProjection</c> will apply along the X axis.
	/// </summary>
	/// 
	/// <remarks>
	/// This is also just the <c>Fov</c> property.
	/// </remarks>
	public Angle FovY { get => Fov; }

	/// <summary>
	/// The ratio of the display area's length over its height.
	/// </summary>
	public float AspectRatio 
	{ 
		get => aspectRatio; 
		set
		{
			aspectRatio = value;
			PerspectiveMatrix = TransformMatrix.CreatePerspectiveProjection((float)FovX.Radians, (float)FovY.Radians, nearClip, farClip);
		} 
	}

	private float aspectRatio;

	/// <summary>
	/// The distance at which nothing before will be rendered.
	/// </summary>
	public float NearClip 
	{ 
		get => nearClip; 
		set
		{
			nearClip = value;
			PerspectiveMatrix = TransformMatrix.CreatePerspectiveProjection((float)FovX.Radians, (float)FovY.Radians, nearClip, farClip);
		} 
	}

	private float nearClip;

	/// <summary>
	/// The distance at which nothing beyond will be rendered.
	/// </summary>
	public float FarClip 
	{ 
		get => farClip; 
		set
		{
			farClip = value;
			PerspectiveMatrix = TransformMatrix.CreatePerspectiveProjection((float)FovX.Radians, (float)FovY.Radians, nearClip, farClip);
		} 
	}

	private float farClip;

	public PerspectiveProjection(Angle fov, float aspectRatio = DefaultAspectRatio, float nearClip = DefaultNearClip, float farClip = DefaultFarClip)
	{
		this.fov = fov;
		this.aspectRatio = aspectRatio;
		this.nearClip = nearClip;
		this.farClip = farClip;
		
		PerspectiveMatrix = TransformMatrix.CreatePerspectiveProjection((float)FovX.Radians, (float)FovY.Radians, nearClip, farClip);
		openGLMatrix = PerspectiveMatrix.ToOpenGLMatrix4();
	}
}