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
	public static class ConsoleBorder
	{
		public static void Draw (ConsoleArea window, int x, int y, int width, int height)
		{
			if (width < 2 || height < 2)
			{
				return;
			}
			var left = x - width / 2;
			var right = left + width - 1;
			var top = y - height / 2;
			var bottom = top + height - 1;

			window.Move (new Position(left, top));
			window.AddCharacter (BoxDrawing.ULCORNER);
			window.RepeatCharacter (BoxDrawing.HLINE, width - 2);
			window.AddCharacter (BoxDrawing.URCORNER);

			window.Move (new Position(left, top + 1));
			window.RepeatCharacterVertical (BoxDrawing.VLINE, height - 2);
			window.Move (new Position(right, top + 1));
			window.RepeatCharacterVertical (BoxDrawing.VLINE, height - 2);

			window.Move (new Position(left, bottom));
			window.AddCharacter (BoxDrawing.LLCORNER);
			window.RepeatCharacter (BoxDrawing.HLINE, width - 2);
			window.AddCharacter (BoxDrawing.LRCORNER);
		}
	}
}
