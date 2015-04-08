using System;

namespace Netcurses
{
	public class Position
	{
		public int X { private set; get; }
		public int Y { private set; get; }

		public Position (int x, int y)
		{
			X = x;
			Y = y;
		}
		public static Position operator +(Position c1, Position c2)
		{
			return new Position(c1.X + c2.X, c1.Y + c2.Y);
		}

		public Position MoveRight ()
		{
			return new Position (X + 1, Y);
		}

		public Position MoveLeft ()
		{
			return new Position (X - 1, Y);
		}

		public Position MoveDown ()
		{
			return  new Position (X, Y + 1);
		}
	}
}

