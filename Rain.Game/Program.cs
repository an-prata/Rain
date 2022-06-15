using Rain.Engine;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		var models = new IModel<float>[] { new Square() };
		var scene = new Scene<float>(models);
		using var game = new GameWindow<float>(scene, "square is cool", 1280, 720);
		game.Run();
	}
}