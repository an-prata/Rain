using Rain.Engine.Geometry;

namespace Rain.Engine.Texturing;

public abstract class TexturableBase : ModelBase, ITexturable, IModel
{
	public override uint[] Elements 
	{
		get
		{
			var elements = new uint[Faces.Length];

			for (var face = 0; face < Faces.GetLength(0); face++)
				for (var element = 0; element < Faces.GetLength(1); element++)
					elements[face * Faces.GetLength(1) + element] = Faces[face, element];
			
			return elements;
		}
	}

	public abstract uint[,] Faces { get; }

	public abstract Texture[] Textures { get; }

	public TexturableBase(Point[] points) : base(points) { }
}