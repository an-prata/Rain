using System.Numerics;

namespace Rain.Engine;

public class Square : IModel<float>
{
	public Vertex<float> Location => new(-0.5f, -0.5f, 1.0f);

	public Point<float>[] Points
	{
		get => new Point<float>[4]
		{
			new Point<float>(new Vertex<float>(-0.5f, -0.5f, 1.0f), new Color<float>(1.0f, 0.0f, 0.0f, 1.0f)), // bl
			new Point<float>(new Vertex<float>( 0.5f, -0.5f, 1.0f), new Color<float>(0.0f, 1.0f, 0.0f, 1.0f)), // br
			new Point<float>(new Vertex<float>(-0.5f,  0.5f, 1.0f), new Color<float>(0.0f, 0.0f, 1.0f, 1.0f)), // tl
			new Point<float>(new Vertex<float>( 0.5f,  0.5f, 1.0f), new Color<float>(1.0f, 0.0f, 1.0f, 1.0f))  // tr
		};
	}

	public uint[] Elements { get => new uint[] {0, 1, 2, 2, 1, 3}; }

	public float[] Array 
	{ 
		get 
		{
			return new float[] 
			{
				-0.5f, -0.5f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f,
				0.5f, -0.5f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f,
				-0.5f, 0.5f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f,
				0.5f, 0.5f, 1.0f, 1.0f, 0.0f, 1.0f, 1.0f
			};
		}
	}
}