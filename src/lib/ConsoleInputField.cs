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
	public enum InputResult
	{
		Nothing,
		Escape,
		Enter
	}
	public class ConsoleInputField
	{
		int cursorX;
		string value = string.Empty;

		public string Value
		{
			get
			{
				return value;
			}
		}

		public int CursorX
		{
			get
			{
				return cursorX;
			}
		}

		public ConsoleInputField ()
		{
		}

		void AddChar (char ch)
		{
			value = value.Substring (0, cursorX) + ch + value.Substring (cursorX);
			cursorX++;
		}

		void RemoveChar ()
		{
			if (value.Length <= cursorX)
			{
				return;
			}
			value = value.Substring (0, cursorX) + value.Substring (cursorX + 1);
		}

		void Back ()
		{
			if (cursorX > 0)
			{
				cursorX--;
			}
		}

		public InputResult DoChar (ConsoleKeyInfo key)
		{
			var ch = key.KeyChar;

			if (ch >= ' ' && ch <= 'z')
			{
				AddChar (ch);
				return InputResult.Nothing;
			}
			switch (key.Key)
			{
			case ConsoleKey.Backspace:
				Back ();
				RemoveChar ();
				break;
			case ConsoleKey.Escape:
				return InputResult.Escape;
			case ConsoleKey.Enter:
				return InputResult.Enter;
			}

			return InputResult.Nothing;
		}
	}
}
