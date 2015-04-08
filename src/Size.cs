using System;

namespace Netcurses
{
	public class Size
	{
		public int Width { private set; get; }

		public int Height { private set; get; }

		public Size (int width, int height)
		{
			Width = width;
			Height = height;
		}

		public int Area { get { return Width * Height; } }

		public bool Outside (Position position)
		{
			return position.X < 0 || position.X >= Width || position.Y < 0 || position.Y >= Height;
		}
	}
}

