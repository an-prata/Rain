using Rain.Engine;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		using var game = new GameWindow("game thats cool idk :))))", 1280, 720);
		game.Run();
	}
}