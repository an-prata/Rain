// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Geometry;
using Rain.Engine.Buffering;
using Rain.Engine.Texturing;

namespace Rain.Engine.Rendering;

/// <summary>
/// A two dimensional face.
/// </summary>
public class Face : TwoDimensionalBase, ITwoDimensional, IBufferable, IDisposable
{
	private readonly TwoDimensionalBase face;

	public override uint[] Elements => face.Elements;

	public override Point[] Points 
	{
		get => face.Points;
		set => face.Points = value;
	}

	public override (Point, Point)[] Sides { get => face.Sides; }

	/// <summary>
	/// The <c>Textures</c> that will be rendered onto <c>Face</c>.
	/// </summary>
	/// 
	/// <remarks>
	/// <c>Texture</c>s are applied in ascending order using their indices. Also, note that if ever this property is of the
	/// base class <c>Face</c> as opposed to a derived class, this property will always be null.
	/// </remarks>
	public virtual Texture[]? Textures { get; }

	public Face(TwoDimensionalBase face) : base(face.Width, face.Height, face.RotationX, face.RotationY, face.RotationZ)
	{
		this.face = face;
	}

	public Face(Face face) : base(face.Width, face.Height, face.RotationX, face.RotationY, face.RotationZ)
	{
		face.face.CopyTo(out this.face);
	}

	public TwoDimensionalBase GetTwoDimensional() => face;

	/// <summary>
	/// Trys to upload and then bind a maximum of 16 <c>Texture</c> objects in <c>Textures</c>.
	/// </summary>
	/// 
	/// <param name="shader">
	/// The shader to upload and bind <c>Texture</c>s to.
	/// </param>
	/// 
	/// <returns>
	/// Returns true upon success, false upon failure (this likely just means that there werent <c>Texture</c> objects in
	/// <c>Textures</c>).
	/// </returns>
	public bool TryUploadAndBindTextures(ShaderProgram shader)
	{
		if (Textures is null)
			return false;
		
		for (var texture = 0; texture < Textures.Length; texture++)
		{
			if (texture > 0)
				shader.GetUniformByName($"opacity{texture}").SetToFloat(Textures[texture].Opacity);
			
			Textures[texture].Upload(Textures[texture].Unit, shader.GetUniformByName($"texture{texture}"));
			Textures[texture].Bind();
		}
		
		return true;
	}

	/// <summary>
	/// Tries to unbind all <c>Texture</c> objects in <c>Textures</c>.
	/// </summary>
	/// 
	/// <returns>
	/// Returns true upon success, false upon failure (this likely just means that there werent <c>Texture</c> objects in
	/// <c>Textures</c>).
	/// </returns>
	public bool TryUnbindTextures()
	{
		if (Textures is null)
			return false;
		
		for (var texture = 0; texture < Textures.Length; texture++)
			Textures[texture].Unbind();
		
		return true;
	}

	public int GetBufferSize(BufferType bufferType)
	{
		if (bufferType == BufferType.VertexBuffer)
			return Points.Length * Point.BufferSize;

		return Elements.Length;
	}

	public Array GetBufferableArray(BufferType bufferType)
	{
		if (bufferType == BufferType.VertexBuffer)
		{
			var bufferableArray = new float[Points.Length * Point.BufferSize];

			for (var point = 0; point < Points.Length; point++)
			{
				var pointArray = (float[])Points[point].GetBufferableArray(bufferType);

				for (var i = 0; i < pointArray.Length; i++)
					bufferableArray[(point * Point.BufferSize) + i] = pointArray[i];
			}

			return bufferableArray;
		}
		else
		{
			return Elements;
		}
	}

	/// <summary>
	/// Copies data from this <c>Face</c> to another.
	/// </summary>
	/// 
	/// <param name="face">
	/// The <c>Face</c> to copy data to.
	/// </param>
	public override void CopyTo(out TwoDimensionalBase face)
		=> face = new Face(this);

	/// <summary>
	/// Copies data from this <c>Face</c> to another.
	/// </summary>
	/// 
	/// <param name="face">
	/// The <c>Face</c> to copy data to.
	/// </param>
	public void CopyTo(out Face face)
		=> face = new Face(this);

	#region IDisposable

	private bool disposed = false;

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposed) 
			return;

		if (disposing)
		{
			for (var texture = 0; Textures is not null && texture < Textures.Length; texture++)
				Textures[texture].Dispose();
		}

		disposed = true;
	}

	~Face() => Dispose(false);

	#endregion
}