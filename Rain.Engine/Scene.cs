using System.Buffers;
using System.Numerics;

namespace Rain.Engine;

public class Scene<T> : IDisposable where T : INumber<T>
{
	private readonly IMemoryOwner<T> owner;

	private readonly Memory<T> memory;

	public Scene(IModel<T>[] models)
	{
		owner = MemoryPool<T>.Shared.Rent(models.Length);
		memory = owner.Memory;
		
		var sceneSizeInT = 0;

		for (var i = 0; i < models.Length; i++)
			sceneSizeInT += models[i].Points.Length * Point<T>.SizeInT;

		T[] vertexData = new T[sceneSizeInT];
		var elementsAdded = 0;

		for (var model = 0; model < models.Length; model++)
		{
			for (var i = 0; i < models[model].Array.Length; i++)
				vertexData[elementsAdded + i] = models[model].Array[i];

			elementsAdded += models[model].Array.Length;
		}

		vertexData.CopyTo(memory);
	}

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
			owner.Dispose();

		disposed = true;
	}

	~Scene() => Dispose(false);

	#endregion
}