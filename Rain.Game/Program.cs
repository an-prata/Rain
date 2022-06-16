using Rain.Engine;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		var models = new IModel<float>[] { new Rectangle<float>(new(-0.8f, -0.8f, 1.0f), 1.6f, 1.6f, new(0.6f, 0.1f, 0.8f, 1.0f)) };
		var scene = new Scene<float>(models);
		using var game = new GameWindow<float>(scene, "square is cool", 1280, 720);
		game.Run();
	}
}