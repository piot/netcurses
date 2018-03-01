/*

MIT License

Copyright (c) 2015 Peter Bjorklund

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
﻿using System;

namespace Netcurses
{
	public class Position
	{
		public int X
		{
			private set; get;
		}
		public int Y
		{
			private set; get;
		}

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
			return new Position (X, Y + 1);
		}
	}
}
