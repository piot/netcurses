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
	public class ConsoleScrollWindow
	{
		readonly ConsoleArea window;

		public ConsoleArea Window
		{
			get
			{
				return window;
			}
		}

		public ConsoleScrollWindow (Size size)
		{
			window = new ConsoleArea (size);
		}

		void Newline ()
		{
			var y = window.CursorPosition.Y;

			if (y == window.Size.Height - 1)
			{
				window.ScrollUp ();
			}
			else
			{
				y += 1;
			}

			window.Move (new Position(0, y));
		}

		public void AddStringEx (string s)
		{
			for (var i = 0; i < s.Length; ++i)
			{
				var ch = s [i];

				if (ch == '\r')
				{
				}
				else if (ch == '\n')
				{
					Newline ();
				}
				else
				{
					window.AddCharacter (ch);
				}
			}
		}

		public void AddString (string s, bool scroll)
		{
			var clamp = s.Length;
			var clamped = false;

			if (window.CursorPosition.X + s.Length >= window.Size.Width)
			{
				clamp = window.Size.Width - window.CursorPosition.X;
				clamped = true;
			}
			var first = s.Substring (0, clamp);
			var second = s.Substring (clamp);
			AddStringEx (first);

			if (!clamped)
			{
				return;
			}

			if (!scroll)
			{
				return;
			}
			Newline ();

			if (second.Length > 0)
			{
				AddString (second, scroll);
			}
		}
	}
}
