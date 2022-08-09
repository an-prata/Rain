// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Texturing;

public enum TextureUnit
{
	/// <summary>
	/// Represents an empty or unassigned texture unit.
	/// </summary>
	None,

	/// <summary>
	/// Represents the first of sixteen texture unites in OpenGL.
	/// </summary>
	Unit0 = OpenTK.Graphics.OpenGL.TextureUnit.Texture0,

	/// <summary>
	/// Represents the second of sixteen texture unites in OpenGL.
	/// </summary>
	Unit1 = OpenTK.Graphics.OpenGL.TextureUnit.Texture1,

	/// <summary>
	/// Represents the third of sixteen texture unites in OpenGL.
	/// </summary>
	Unit2 = OpenTK.Graphics.OpenGL.TextureUnit.Texture2,

	/// <summary>
	/// Represents the fourth of sixteen texture unites in OpenGL.
	/// </summary>
	Unit3 = OpenTK.Graphics.OpenGL.TextureUnit.Texture3,

	/// <summary>
	/// Represents the fifth of sixteen texture unites in OpenGL.
	/// </summary>
	Unit4 = OpenTK.Graphics.OpenGL.TextureUnit.Texture4,

	/// <summary>
	/// Represents the sixth of sixteen texture unites in OpenGL.
	/// </summary>
	Unit5 = OpenTK.Graphics.OpenGL.TextureUnit.Texture5,

	/// <summary>
	/// Represents the seventh of sixteen texture unites in OpenGL.
	/// </summary>
	Unit6 = OpenTK.Graphics.OpenGL.TextureUnit.Texture6,

	/// <summary>
	/// Represents the eigth of sixteen texture unites in OpenGL.
	/// </summary>
	Unit7 = OpenTK.Graphics.OpenGL.TextureUnit.Texture7,

	/// <summary>
	/// Represents the ninth of sixteen texture unites in OpenGL.
	/// </summary>
	Unit8 = OpenTK.Graphics.OpenGL.TextureUnit.Texture8,

	/// <summary>
	/// Represents the tenth of sixteen texture unites in OpenGL.
	/// </summary>
	Unit9 = OpenTK.Graphics.OpenGL.TextureUnit.Texture9,

	/// <summary>
	/// Represents the eleventh of sixteen texture unites in OpenGL.
	/// </summary>
	Unit10 = OpenTK.Graphics.OpenGL.TextureUnit.Texture10,

	/// <summary>
	/// Represents the twelth of sixteen texture unites in OpenGL.
	/// </summary>
	Unit11 = OpenTK.Graphics.OpenGL.TextureUnit.Texture11,

	/// <summary>
	/// Represents the thirteenth of sixteen texture unites in OpenGL.
	/// </summary>
	Unit12 = OpenTK.Graphics.OpenGL.TextureUnit.Texture12,

	/// <summary>
	/// Represents the first of fourteenth texture unites in OpenGL.
	/// </summary>
	Unit13 = OpenTK.Graphics.OpenGL.TextureUnit.Texture13,

	/// <summary>
	/// Represents the first of fifteenth texture unites in OpenGL.
	/// </summary>
	Unit14 = OpenTK.Graphics.OpenGL.TextureUnit.Texture14,

	/// <summary>
	/// Represents the first of sixteen texture unites in OpenGL.
	/// </summary>
	Unit15 = OpenTK.Graphics.OpenGL.TextureUnit.Texture15
}