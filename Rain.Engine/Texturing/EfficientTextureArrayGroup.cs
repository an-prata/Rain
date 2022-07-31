// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using System.Collections;

namespace Rain.Engine.Texturing;

/// <summary>
/// Stores <c>Texture</c> arrays so that, while still retaining their original indices, and insures there are no duplicates, 
/// both in computer and GPU memory.
/// </summary>
public class EfficientTextureArrayGroup : IReadOnlyList<Texture[]>, IEnumerator<Texture[]>,  IDisposable
{
	private readonly EfficientTextureGroup textures;

	private readonly int[] indices;

	public Texture[] this[int index]
	{
		get 
		{
			int arrayLength;

			if (index >= indices.Length)
				throw new IndexOutOfRangeException();
			
			if (index == indices.Length - 1)
				arrayLength = textures.Count - indices[index];
			else
				arrayLength = indices[index + 1] - indices[index];
			
			var textureArray = new Texture[arrayLength];

			for (var texture = 0; texture < textureArray.Length; texture++)
				textureArray[texture] = textures[texture + indices[index]];
			
			return textureArray;
		}
	}

	/// <summary>
	/// The number of objects in this <c>EfficientTextureArrayGroup</c>.
	/// </summary>
	public int Count { get; }

	/// <summary>
	/// Creates a new <c>EfficientTextureArrayGroup</c> instance from a jagged array.
	/// </summary>
	/// 
	/// <param name="textures">
	/// The jagged array to create a new <c>EfficientTextureArrayGroup</c> from.
	/// </param>
	public EfficientTextureArrayGroup(Texture[][] textures)
	{
		var textureList = new List<Texture>();
		var texturesAdded = 0;

		indices = new int[textures.Length];

		for (var array = 0; array < textures.Length; array++)
		{
			for (var texture = 0; texture < textures[array].Length; texture++)
				textureList.Add(textures[array][texture]);
			
			indices[array] = texturesAdded;
			texturesAdded += textures[array].Length;
		}

		this.textures = new(textureList.ToArray());
		Count = textures.Length;
	}

	#region IEnumerable

	IEnumerator<Texture[]>  IEnumerable<Texture[]>.GetEnumerator() => this;
	
	IEnumerator IEnumerable.GetEnumerator() => this;
	
	#endregion

	#region IEnumerator

	private int current = -1;

	object IEnumerator.Current => Current;

	public Texture[] Current
	{
		get
		{
			try
			{
				return this[current];
			}
			catch (IndexOutOfRangeException)
			{
				throw new InvalidOperationException();
			}
		}
	}

	public bool MoveNext()
	{
		if (++current >= Count)
			return false;
		
		return true;
	}

	public void Reset() => current = -1;

	#endregion

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
			textures.Dispose();

		disposed = true;
	}

	~EfficientTextureArrayGroup() => Dispose(false);

	#endregion
}