namespace Rain.Engine.Texturing;

/// <summary>
/// Stores <c>Texture</c> arrays so that, while still retaining their original indices, there are no duplicates, both in 
/// computer and GPU memory.
/// </summary>
public class EfficientTextureArrayGroup
{
	private EfficientTextureGroup textures;

	private int[] indices;

	public Texture[] this[int index]
	{
		get 
		{
			int arrayLength;

			if (index >= indices.Length)
				throw new IndexOutOfRangeException();
			
			if (index == indices.Length - 1)
				arrayLength = textures.Length - indices[index];
			else
				arrayLength = indices[index + 1] - indices[index];
			
			var textureArray = new Texture[arrayLength];

			for (var texture = 0; texture < textureArray.Length; texture++)
				textureArray[texture] = textures[texture + indices[index]];
			
			return textureArray;
		}
	}

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
	}
}