using System.Buffers;

using SixLabors.ImageSharp;

namespace Rain.Engine.Common;

static internal class MemoryHelper
{
	internal static IMemoryOwner<T> AllocateContiguousMemory<T>(int size)
		where T : unmanaged
		=> Configuration.Default.MemoryAllocator.Allocate<T>(size);
}