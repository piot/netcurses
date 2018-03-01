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
	public class ConsoleUpdater
	{
		readonly ConsoleArea screenWindow;
		readonly IDriver _driver;

		public ConsoleUpdater (IDriver driver, Size size)
		{
			_driver = driver;
			screenWindow = new ConsoleArea (size);
		}

		void SetCursorPosition (int x, int y)
		{
			_driver.Move (new Position (x, y));
		}

		void SetCharacterAndColor (ConsoleCharacter sourceChar)
		{
			_driver.Add (sourceChar.Foreground, sourceChar.Background, sourceChar.Character);
		}

		public void Update (ConsoleArea window)
		{
			Console.CursorVisible = false;
			int lastX = -1;
			int lastY = -1;
			var updateHeight = Math.Min (window.Size.Height, screenWindow.Size.Height);
			var updateWidth = Math.Min (window.Size.Width, screenWindow.Size.Width);

			var chars = window.ConsoleCharacters;
			for (var y = 0; y < updateHeight; ++y)
			{
				for (var x = 0; x < updateWidth; ++x)
				{
					var position = new Position (x, y);
					var sourceIndex = window.GetCharacterIndex (position);
					var sourceChar = window.ConsoleCharacters [sourceIndex];
					var targetIndex = screenWindow.GetCharacterIndex (position);
					var targetChar = screenWindow.ConsoleCharacters [targetIndex];

					if (!sourceChar.IsSame (targetChar))
					{
						if (x != lastX || y != lastY)
						{
							SetCursorPosition (x, y);
						}
						SetCharacterAndColor (sourceChar);
						lastX = x + 1;
						lastY = y;
						screenWindow.ConsoleCharacters [targetIndex] = sourceChar;
					}
				}
			}
			_driver.Move (window.CursorPosition);
			_driver.Refresh ();
			// Console.CursorVisible = true;
		}
	}
}
