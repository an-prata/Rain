using Rain.Engine.Geometry;
using Rain.Engine.Buffering;
using Rain.Engine.Texturing;

namespace Rain.Engine.Rendering;

public interface IRenderable : IBufferable, ITransformable, IModel 
{ 
	public TexturedFaceGroup Faces { get; }
}