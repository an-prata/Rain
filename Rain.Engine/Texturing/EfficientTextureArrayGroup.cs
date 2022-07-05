namespace Rain.Engine.Texturing;

public class EfficientTextureArrayGroup
{
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
	private EfficientTextureGroup textures;

	private int[] indices;

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