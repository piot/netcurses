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
﻿ using System;
using System.Collections.Generic;

namespace Netcurses
{
	public class ConsoleDriver : IDriver
	{
		Dictionary<int, char> boxDrawing = new Dictionary<int, char>() {
			{BoxDrawing.HLINE, '─'},
			{BoxDrawing.VLINE, '│'},
			{BoxDrawing.LRCORNER, '┙'},
			{BoxDrawing.ULCORNER, '┌'},
			{BoxDrawing.URCORNER, '┐'},
			{BoxDrawing.LLCORNER, '└'}
		};

		#region IDriver implementation

		public void Move (Position position)
		{
			Console.SetCursorPosition (position.X, position.Y);
		}

		public void Add (ConsoleColor foreground, ConsoleColor background, int charCode)
		{
			Console.BackgroundColor = background;
			Console.ForegroundColor = foreground;
			char ch = (char)charCode;
			char boxDrawingChar;

			if (boxDrawing.TryGetValue (charCode, out boxDrawingChar))
			{
				ch = boxDrawingChar;
			}

			Console.Write ((char)ch);
		}

		public void Close()
		{
			Console.CursorVisible = true;
			Console.ResetColor();
			Console.Clear();
		}

		public void Refresh ()
		{
		}

		public ConsoleKeyInfo ReadKey ()
		{
			if (Console.KeyAvailable)
			{
				return Console.ReadKey (true);
			}
			else
			{
				return new ConsoleKeyInfo ();
			}
		}

		#endregion
	}
}
