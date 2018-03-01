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
	public class ConsoleArea
	{
		readonly ConsoleCharacter[] characters;
		Position cursorPosition = new Position (0, 0);

		public Size Size
		{
			private set; get;
		}

		public ConsoleColor Foreground
		{
			get; set;
		}

		public ConsoleColor Background
		{
			get; set;
		}

		public Position CursorPosition
		{
			get
			{
				return cursorPosition;
			}
		}

		public int WidthLeft
		{
			get
			{
				return Size.Width - CursorPosition.X;
			}
		}

		public int Length
		{
			get
			{
				return Size.Area;
			}
		}

		public ConsoleCharacter[] ConsoleCharacters
		{
			get
			{
				return characters;
			}
		}

		public ConsoleArea (Size size)
		{
			Size = size;
			Foreground = ConsoleColor.White;
			Background = ConsoleColor.Cyan;

			characters = new ConsoleCharacter[Size.Area];
			for (var i = 0; i < Length; ++i)
			{
				characters [i] = new ConsoleCharacter
				{
					Foreground = ConsoleColor.White,
					Background = ConsoleColor.Cyan,
				};
			}
		}

		public void Copy (ConsoleArea window, Position offset)
		{
			var minHeight = Math.Min (window.Size.Height, Size.Height - offset.Y);
			var minWidth = Math.Min (window.Size.Width, Size.Width - offset.X);

			for (var y = 0; y < minHeight; ++y)
			{
				for (var x = 0; x < minWidth; ++x)
				{
					var position = new Position (x, y);
					var sourceIndex = window.GetCharacterIndex (position);

					if (sourceIndex < 0)
					{
						continue;
					}
					var targetIndex = GetCharacterIndex (position + offset);

					if (targetIndex < 0)
					{
						continue;
					}
					characters [targetIndex] = window.characters [sourceIndex];
				}
			}
		}

		public int GetCharacterIndex (Position position)
		{
			if (Size.Outside(position))
			{
				return -1;
			}
			return position.Y * Size.Width + position.X;
		}

		public void Clear ()
		{
			for (var i = 0; i < characters.Length; ++i)
			{
				characters [i] = new ConsoleCharacter () {
					Foreground = Foreground, Background = Background, Character = ' '
				};
			}
		}

		public void Fill (Rectangle rect)
		{
			var pos = rect.Center;
			var size = rect.Size;
			var left = pos.X - size.Width / 2;
			var top = pos.Y - size.Height / 2;

			left = Math.Max (0, left);
			top = Math.Max (0, top);
			left = Math.Min (left, Size.Width - 1);
			top = Math.Min (top, Size.Height - 1);
			var offset = new Position (left, top);

			var fillWidth = Math.Min (size.Width, Size.Width - left);
			var fillHeight = Math.Min (size.Height, Size.Height - top);
			for (var x = 0; x < fillWidth; ++x)
			{
				for (var y = 0; y < fillHeight; ++y)
				{
					var position = new Position (x, y);
					characters [GetCharacterIndex (position + offset)] = new ConsoleCharacter () {
						Foreground = Foreground,
						Background = Background,
						Character = ' '
					};
				}
			}
		}

		public void Move (Position position)
		{
			cursorPosition = position;
		}

		public void SetCharacter (Position position, int ch)
		{
			var characterIndex = GetCharacterIndex (position);

			if (characterIndex < 0)
			{
				return;
			}
			characters [characterIndex].Character = ch;
			characters [characterIndex].Foreground = Foreground;
			characters [characterIndex].Background = Background;
		}

		public void AddCharacter (int ch)
		{
			SetCharacter (cursorPosition, ch);
			cursorPosition = cursorPosition.MoveRight ();
		}

		public void AddString (string s)
		{
			for (var i = 0; i < s.Length; ++i)
			{
				AddCharacter (s [i]);
			}
		}

		public void RepeatCharacter (int c, int length)
		{
			for (var i = 0; i < length; ++i)
			{
				AddCharacter (c);
			}
		}

		public void RepeatCharacterVertical (int c, int length)
		{
			for (var i = 0; i < length; ++i)
			{
				AddCharacter (c);
				cursorPosition = cursorPosition.MoveLeft ().MoveDown ();
			}
		}

		public void ClearLine ()
		{
			for (var x = CursorPosition.X; x < Size.Width; ++x)
			{
				characters [GetCharacterIndex (new Position(x, CursorPosition.Y))] = new ConsoleCharacter () {
					Foreground = Foreground,
					Background = Background,
					Character = ' '
				};
			}
		}

		public void ClearCharacters (int spaces)
		{
			for (var i = 0; i < spaces; ++i)
			{
				AddCharacter (' ');
			}
		}

		public void ScrollUp ()
		{
			for (var y = 0; y < Size.Height - 1; ++y)
			{
				for (var x = 0; x < Size.Width; ++x)
				{
					var position = new Position (x, y);
					characters [GetCharacterIndex (position)] = characters [GetCharacterIndex (position.MoveDown())];
				}
			}

			Move (new Position(0, Size.Height - 1));
			ClearLine ();
		}
	}
}
