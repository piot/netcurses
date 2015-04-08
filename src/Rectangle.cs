using System;

namespace Netcurses
{
	public class Rectangle
	{
		public Position Center { private set; get; }

		public Size Size { private set; get; }

		public Rectangle (Position center, Size size)
		{
			Center = center;
			Size = size;
		}
	}
}

