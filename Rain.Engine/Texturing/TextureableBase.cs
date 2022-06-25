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

	public abstract int[] TextureIndices { get; }

	public TexturableBase(Point[] points) : base(points) { }

	#region IDisposable

	private bool disposed = false;

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposed) 
			return;

		if (disposing)
		{
			for (var texture = 0; texture < Textures.Length; texture++)
				Textures[texture].Dispose();
		}

		disposed = true;
	}

	~TexturableBase() => Dispose(false);

	#endregion
}