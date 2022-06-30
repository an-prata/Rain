using Rain.Engine.Geometry;
using Rain.Engine.Buffering;

namespace Rain.Engine.Texturing;

public struct TexturedFace : IBufferable
{
	public ITwoDimensional Face { get; set; }

	public Texture Texture { get; set; }

	public int GetBufferSize(BufferType bufferType)
	{
		if (bufferType == BufferType.VertexBuffer)
			return Face.Points.Length * Point.BufferSize;
		return Face.Elements.Length;
	}

	public Array GetBufferableArray(BufferType bufferType)
	{
		if (bufferType == BufferType.VertexBuffer)
		{
			var bufferableArray = new float[Face.Points.Length * Point.BufferSize];

			for (var point = 0; point < Face.Points.Length; point++)
			{
				var pointArray = (float[])Face.Points[point].GetBufferableArray(bufferType);

				for (var i = 0; i < pointArray.Length; i++)
					bufferableArray[point * Point.BufferSize] = pointArray[i];
			}

			return bufferableArray;
		}
		else
			return Face.Elements;
	}
}