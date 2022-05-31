using System.Buffers;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Rain.Engine.Graphics.Textures;

public static class ImageMemoryHelper
{
	internal static IMemoryOwner<TPixel> AllocateContiguousMemory<TPixel>(int size)
		where TPixel : unmanaged, IPixel<TPixel>
		=> Configuration.Default.MemoryAllocator.Allocate<TPixel>(size);
	
	internal static Memory<TPixel> CopyImageToMemory<TPixel>(this Image<TPixel> image, IMemoryOwner<TPixel> owner)
		where TPixel : unmanaged, IPixel<TPixel>
	{
		if (image.DangerousTryGetSinglePixelMemory(out var memory))
		{
			memory.CopyTo(owner.Memory);
			return owner.Memory;
		}
		
		var imageSize = image.Height * image.Width;
		var memorySize = owner.Memory.Length;

		throw new Exception($"Could not copy image (TPixels: {imageSize}) to memory (Length: {memorySize})");
	}

	internal static Span<byte> Bytes<TPixel>(this Image<TPixel> image)
		where TPixel : unmanaged, IPixel<TPixel>
	{
		var bytes = new Span<byte>(new byte[4 * image.Width * image.Height]);
		image.CopyPixelDataTo(bytes);
		return bytes;
	}
}