// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Mathematics;
using Rain.Engine.Geometry;

namespace Rain.Engine.Rendering;

public class PerspectiveProjection
{
	private Matrix4 openGLMatrix;

	public ref Matrix4 OpenGLMatrix { get => ref openGLMatrix; }

	public TransformMatrix PerspectiveMatrix { get; }

	public Angle Fov { get; }

	public PerspectiveProjection(Angle fov, float aspectRatio, float nearClip, float farClip)
	{
		var fovX = aspectRatio * (float)fov.Radians;
		var fovY = (float)fov.Radians;
		
		PerspectiveMatrix = TransformMatrix.CreatePerspectiveProjectionMatrix(fovX, fovY, nearClip, farClip);
		openGLMatrix = PerspectiveMatrix.ToOpenGLMatrix4();
	}
}