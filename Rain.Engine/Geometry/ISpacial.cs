namespace Rain.Engine.Geometry;

public interface ISpacial
{
	/// <summary>
	/// Gets the location of this <c>ISpacial</c> object, should be the center of any multi-point object.
	/// </summary>
	Vertex Location { get; }

	/// <summary>
	/// Gets the distance between this <c>ISpacial</c> and another.
	/// </summary>
	/// 
	/// <param name="other">
	/// The other <c>ISpacial</c>.
	/// </param>
	/// 
	/// <returns>
	/// The distance between this and the other <c>ISpacial</c>.
	/// </returns>
	double GetDistanceBetween(ISpacial other);
}