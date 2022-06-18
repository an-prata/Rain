using Rain.Engine;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		var triangle = new Triangle(new(-0.5f, -0.5f, 1.0f), 0.2f, 0.2f, new(0.6f, 0.1f, 0.8f, 1.0f));
		//triangle.RotateClockwise(45f);
		var models = new IModel[] { triangle };
		var scene = new Scene(models);
		using var game = new GameWindow(scene, "square is cool", 1280, 720);
		game.Run();
	}
}