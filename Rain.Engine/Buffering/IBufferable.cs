namespace Rain.Engine.Buffering;

public interface IBufferable
{
	/// <summary>
	/// Gets the number of elements that would be outputted by <c>GetBufferableArray</c> using the same <c>BufferType</c>.
	/// </summary>
	/// 
	/// <param name="bufferType">
	/// The <c>BufferType</c> to get the buffer size in elements for.
	/// </param>
	/// 
	/// <returns> 
	/// The number of elements that would be outputted by <c>GetBufferableArray</c> using the same <c>BufferType</c>. 
	/// </returns>
	int GetBufferSize(BufferType bufferType);

	/// <summary> 
	/// Gets an array that can be buffered by a <c>Buffer</c> of type <c>bufferType</c>. 
	/// </summary>
	/// 
	/// <param name="bufferType"> 
	/// The type of <c>Buffer</c> to poduce and array for. 
	/// </param>
	/// 
	/// <returns> 
	/// An array bufferable by any buffer of type <c>bufferType</c>. 
	/// </returns>
	Array GetBufferableArray(BufferType bufferType);
}