using Rain.Engine.Geometry;

namespace Rain.Engine.Texturing;

/// <summary>
/// Stores <c>Texture</c> instances so that, while still retaining their original indices, there are no duplicates, both in 
/// computer and GPU memory.
/// </summary>
public struct EfficientTextureGroup
{
	/// <summary>
	/// All textures stored by this <c>EfficientTextureGroup</c>, indices will be the same as the array passed in during
	/// construction, however the <c>Texture</c> at that index is actually empty, and the getter method simply returns the
	/// first duplicate found in the array. 
	/// </summary>
	public Texture this[int index]
	{
		get => textures[indices[index]];
		set
		{
			// indices[index] == index will only true if the original texture is housed at texture[index], which in this case 
			// is the same as texture[indices[index]]. This means that texture[index].IsEmpty must be false, and that it is 
			// potentially pointed to by another index in indices.
			//
			// If this is not true, and our texture is empty, we can simply populate the empty Texture object and change
			// indices[index] to point to it, meaning indices[index] will equal index.
			if (indices[index] != index)
			{
				indices[index] = index;
				textures[index] = value;
				return;
			}

			// In the event this index could potentially be referenced to by another index in indices, we need to make all
			// potential pointing indices point to the next least index, and populate that index in the textures array with
			// the original texture. 
			//
			// Loop through all indices greater than index to see if any point to the same texture as before.
			for (var duplicateCheck = index; duplicateCheck < indices.Length; duplicateCheck++)
			{
				if (indices[duplicateCheck] == index)
				{
					// Upon finding a duplicate, move the pointed to texture to the greater index, as the original will
					// be overriden with a new texture. Then make indices[duplicate] point to that new texture.
					textures[duplicateCheck] = textures[index];
					indices[duplicateCheck] = duplicateCheck;

					// Loop through all remaining greater indices and if further duplicate indices are found, replace them
					// with the new housing index so that the all point to the same texture, finaly freeing our original 
					// index to be overriden.
					for (var furtherDuplicate = duplicateCheck; furtherDuplicate < indices.Length; furtherDuplicate++)
						if (indices[furtherDuplicate] == index)
							indices[furtherDuplicate] = duplicateCheck;
				}
			}
				
			// Now that we know for certain that our desired index is free, we can make indices point to it, and set its 
			// corrosponding texture.
			indices[index] = index;
			textures[index] = value;
			return;
		}
	}

	private Texture[] textures;

	private int[] indices;

	/// <summary>
	/// Creates a new <c>EfficientTextureGroup</c> from an array of <c>Texture</c>s.
	/// </summary>
	/// 
	/// <param name="textures">
	/// An array of <c>Texture</c>s to be stored without duplicates.
	/// </param>
	public EfficientTextureGroup(Texture[] textures)
	{
		this.textures = textures;
		indices = new int[this.textures.Length];
		indices[0] = 0;

		// Checks for duplicates in textures and uses indices to point to the first instance of the duplicate textures and
		// then replaces the second instance of that texture with an empty texture.
		for (var texture = 1; texture < this.textures.Length; texture++)
		{
			for (var duplicateCheck = texture - 1; duplicateCheck >= 0; duplicateCheck--)
				if (!this.textures[texture].IsEmpty && this.textures[texture] == this.textures[duplicateCheck])
				{
					indices[texture] = duplicateCheck;
					this.textures[texture].Dispose();
					this.textures[texture] = Texture.Empty();
				}
				else
					indices[texture] = texture;
		}
	}
}