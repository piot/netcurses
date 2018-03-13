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
﻿
using System;
using System.Collections.Generic;

namespace Netcurses
{
	public class AnsiDriver : IDriver
	{
		Dictionary<int, char> boxDrawing = new Dictionary<int, char>() {
			{BoxDrawing.HLINE, '─'},
			{BoxDrawing.VLINE, '│'},
			{BoxDrawing.LRCORNER, '┙'},
			{BoxDrawing.ULCORNER, '┌'},
			{BoxDrawing.URCORNER, '┐'},
			{BoxDrawing.LLCORNER, '└'}
		};
		ConsoleColor lastForeground;
		bool foregroundDefined;
		ConsoleColor lastBackground;
		bool backgroundDefined;

		public AnsiDriver()
		{
			Output("\x1b[12h");
		}

		void IDriver.Add(ConsoleColor foreground, ConsoleColor background, int charCode)
		{
			var sequence = "";

			if (lastForeground != foreground || !foregroundDefined)
			{
				var fg = 30 + DarkConsoleColorToAnsi(foreground);
				var intensity = ConsoleColorToIntensity(foreground);
				sequence += $"{fg}";

				if (intensity == 1)
				{
					sequence += ";1";
				}
				lastForeground = foreground;
				foregroundDefined = true;
			}

			if (lastBackground != background || !backgroundDefined)
			{
				var bg = 40 + DarkConsoleColorToAnsi(background);

				if (sequence.Length > 0)
				{
					sequence += ";";
				}
				sequence += $"{bg}";
				lastBackground = background;
				backgroundDefined = true;
			}

			if (sequence.Length > 0)
			{
				Output("\x1b[" + sequence + "m");
			}

			char ch = (char)charCode;
			char boxDrawingChar;

			if (boxDrawing.TryGetValue(charCode, out boxDrawingChar))
			{
				ch = boxDrawingChar;
			}

			Output("" + ch);
		}

		void IDriver.Close()
		{
		}

		int DarkConsoleColorToAnsi(ConsoleColor c)
		{
			switch (c)
			{
			case ConsoleColor.Black:
			case ConsoleColor.Gray:
				return 0;
			case ConsoleColor.DarkRed:
			case ConsoleColor.Red:
				return 1;
			case ConsoleColor.DarkGreen:
			case ConsoleColor.Green:
				return 2;
			case ConsoleColor.DarkYellow:
			case ConsoleColor.Yellow:
				return 3;
			case ConsoleColor.DarkBlue:
			case ConsoleColor.Blue:
				return 4;
			case ConsoleColor.DarkMagenta:
			case ConsoleColor.Magenta:
				return 5;
			case ConsoleColor.DarkCyan:
			case ConsoleColor.Cyan:
				return 6;
			case ConsoleColor.DarkGray:
			case ConsoleColor.White:
				return 7;
			}
			return 0;
		}

		int ConsoleColorToIntensity(ConsoleColor c)
		{
			switch (c)
			{
			case ConsoleColor.Gray:
			case ConsoleColor.Cyan:
			case ConsoleColor.Green:
			case ConsoleColor.Magenta:
			case ConsoleColor.Red:
			case ConsoleColor.White:
			case ConsoleColor.Yellow:
				return 1;
			}

			return 0;
		}

		string EscapePosition(int column, int line)
		{
			return $"\x1b[{line+1};{column+1}H";
		}

		void Output(string text)
		{
			Console.Out.Write(text);
		}

		void IDriver.Move(Position position)
		{
			Output(EscapePosition(position.X, position.Y));
		}

		public ConsoleKeyInfo ReadKey()
		{
			if (Console.KeyAvailable)
			{
				return Console.ReadKey(true);
			}
			return new ConsoleKeyInfo();
		}

		void IDriver.Refresh()
		{
		}
	}
}
