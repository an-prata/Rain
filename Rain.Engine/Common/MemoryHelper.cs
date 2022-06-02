using System.Buffers;

using SixLabors.ImageSharp;

namespace Rain.Engine.Common;

static internal class MemoryHelper
{
	/// <summary>
	/// Allocates and returns the IMemoryOwner of memory the size of <cref>size</cref> <cref>T<cref>'s.
	/// </summary>
	/// <param name="size">The number of elements to allocate in memory.</param>
	/// <typeparam name="T">The type tto allocate in memory.</typeparam>
	/// <returns>The IMemoryOwner of the allocated memory.</returns>
	static internal IMemoryOwner<T> AllocateContiguousMemory<T>(int size)
		where T : unmanaged
		=> Configuration.Default.MemoryAllocator.Allocate<T>(size);
}