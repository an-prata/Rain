// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Geometry;
using Rain.Engine.Buffering;
using Rain.Engine.Texturing;

namespace Rain.Engine.Rendering;

public interface IRenderable : IBufferable, ITransformable, IModel 
{ 
	public TexturedFaceGroup Faces { get; }
}