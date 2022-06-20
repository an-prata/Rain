using OpenTK.Windowing.Common;

namespace Rain.Engine;

public struct GameOptions
{
	/// <summary> Window width in pixels. </summary>
	public int Width;

	/// <summary> Window height in pixels. </summary>
	public int Height;

	/// <summary> The first scene to become the <c>GameWindow</c>'s <c>ActiveScene</c>. </summary>
	public Scene StartingScene;

	/// <summary> The window title. </summary>
	/// <remarks> Defaults to "Rain" in a <c>GameWindow</c>. </remarks>
	public string? WindowTitle;

	/// <summary> The rate at which <c>GameWindow.RenderFrame</c> is called in Hz. </summary>
	/// <remarks> 
	/// A value less than one indicates no frame limit, values higher that 500 are clamped to 200. 
	/// Defaults to 0 (no frame limit) in a <c>GameWindow</c>.
	/// </remarks>
	public double? RenderFrequency;

	/// <summary> The rate at which <c>GameWindow.UpdateFrame</c> is called in Hz. </summary>
	/// <remarks> 
	/// A value less than one indicates no update limit, values higher that 500 are clamped to 500. 
	/// Defaults to 0 (no update limit) in a <c>GameWindow</c>.
	/// </remarks>
	public double? UpdateFrequency;

	/// <summary> Whether or not the window opens visibly. </summary>
	/// <remarks> Defaults to true in a <c>GameWindow</c>. </remarks>
	public bool? StartVisible;

	/// <summary> Whether or not the window takes focus on start. </summary>
	/// <remarks> Defaults to true in a <c>GameWindow</c>. </remarks>
	public bool? StartFocused;

	/// <summary> Whether or not to center the window on start. </summary>
	/// <remarks> Defaults to false in a <c>GameWindow</c>. </remarks>
	public bool? CenterWindow;

	/// <summary> The style of window border. </summary>
	/// <remarks> Defaults to <c>WindowBorder.Resizable</c> in a <c>GameWindow</c>. </remarks>
	public WindowBorder? WindowBorder;

	/// <summary> The clear color of OpenGL's renders. </summary>
	public Color? ClearColor;
}