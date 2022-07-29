// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using System.Collections;
using Rain.Engine.Geometry;
using Rain.Engine.Rendering;

namespace Rain.Engine.Texturing;

/// <summary>
/// An efficient group of <c>TexturedFace</c> objects.
/// </summary>
public class TexturedFaceGroup : IReadOnlyList<Face>, IEnumerator<Face>, IDisposable
{
	public TexturedFace this[int index]
	{
		// Construct with reference so the returned value can have TransformMatrices applied to it.
		get => new(Faces[index], Textures[index], constructWithReference: true);
	}

	Face IReadOnlyList<Face>.this[int index]
	{
		get => this[index];
	}

	/// <summary>
	/// The faces to be textured.
	/// </summary>
	public TwoDimensionalBase[] Faces { get; }

	/// <summary>
	/// An <c>EfficientTextureArrayGroup</c> with an array for each face.
	/// </summary>
	public EfficientTextureArrayGroup Textures { get; private set; }

	/// <summary>
	/// The number of objects in this <c>TexturedFaceGroup</c>.
	/// </summary>
	public int Count { get => Faces.Length; }

	/// <summary>
	/// Creates a new <c>TexturedFaceGroup</c> from an array of <c>TexturedFace</c> objects.
	/// </summary>
	/// 
	/// <param name="texturedFaces">
	/// The <c>TexturedFace</c>s to be made into a group.
	/// </param>
	public TexturedFaceGroup(TexturedFace[] texturedFaces)
	{
		var faces = new TwoDimensionalBase[texturedFaces.Length];
		var textures = new Texture[texturedFaces.Length][];

		for (var face = 0; face < texturedFaces.Length; face++)
		{
			faces[face] = texturedFaces[face].GetTwoDimensional();
			textures[face] = texturedFaces[face].Textures;
		}

		Faces = faces;
		Textures = new(textures.ToArray());
	}

	#region IEnumerable

	IEnumerator<Face> IEnumerable<Face>.GetEnumerator() => this;
	
	IEnumerator IEnumerable.GetEnumerator() => this;
	
	#endregion

	#region IEnumerator

	private int current = -1;

	object IEnumerator.Current => Current;

	public Face Current
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
			Textures.Dispose();

		disposed = true;
	}

	~TexturedFaceGroup() => Dispose(false);

	#endregion
}