using System;

namespace Netcurses
{
	public class ConsoleWindow
	{
		readonly ConsoleCharacter[] characters;
		int xPos;
		int yPos;
		int _width;
		int _height;

		public ConsoleColor Foreground { get; set; }

		public ConsoleColor Background { get; set; }

		public int X { get { return xPos; } }

		public int Y { get { return yPos; } }

		public int Height {
			get { return _height; }
		}

		public int Width {
			get { return _width; }
		}

		public int Length {
			get { return _width * _height; }
		}

		public ConsoleCharacter[] ConsoleCharacters { get { return characters; } }

		public ConsoleWindow (int width, int height)
		{
			this._width = width;
			this._height = height;
			Foreground = ConsoleColor.White;
			Background = ConsoleColor.Cyan;


			characters = new ConsoleCharacter[width * height];
			for (var i = 0; i < Length; ++i) {
				characters [i] = new ConsoleCharacter {
					Foreground = ConsoleColor.White,
					Background = ConsoleColor.Cyan,
				};

			}
		}

		public void Copy (ConsoleWindow window, int xOffset, int yOffset)
		{
			var minHeight = Math.Min (window._height, _height - yOffset);
			var minWidth = Math.Min (window._width, _width - xOffset);

			for (var y = 0; y < minHeight; ++y) {
				for (var x = 0; x < minWidth; ++x) {
					var sourceIndex = window.GetCharacterIndex (x, y);
					if (sourceIndex < 0) {
						continue;
					}
					var targetIndex = GetCharacterIndex (x + xOffset, y + yOffset);
					if (targetIndex < 0) {
						continue;
					}
					characters [targetIndex] = window.characters [sourceIndex];
				}
			}
		}

		public int GetCharacterIndex (int x, int y)
		{
			if (x < 0 || x >= _width || y < 0 || y >= _height) {
				return -1;
			}
			return y * _width + x;
		}

		public void Clear ()
		{
			for (var i = 0; i < characters.Length; ++i) {
				characters [i] = new ConsoleCharacter () { Foreground = Foreground, Background = Background, Character = ' ' };
			}
		}

		public void Fill (int xStart, int yStart, int width, int height)
		{
			var left = xStart - width / 2;
			var top = yStart - height / 2;


			left = Math.Max (0, left);
			top = Math.Max (0, top);
			left = Math.Min (left, _width - 1);
			top = Math.Min (top, _height - 1);

			var fillWidth = Math.Min (width, _width - left);
			var fillHeight = Math.Min (height, _height - top);
			for (var x = 0; x < fillWidth; ++x) {
				for (var y = 0; y < fillHeight; ++y) {
					characters [GetCharacterIndex (x + left, y + top)] = new ConsoleCharacter () {
						Foreground = Foreground,
						Background = Background,
						Character = ' '
					};
				}
			}
		}

		public void Move (int x, int y)
		{
			this.xPos = x;
			this.yPos = y;
		}

		public void SetCharacter (int x, int y, char ch)
		{
			var characterIndex = GetCharacterIndex (x, y);
			if (characterIndex < 0) {
				return;
			}
			characters [characterIndex].Character = ch;
			characters [characterIndex].Foreground = Foreground;
			characters [characterIndex].Background = Background;
		}


		public void AddCharacter (char ch)
		{
			SetCharacter (xPos, yPos, ch);
			xPos++;
		}

		public void AddString (string s)
		{
			for (var i = 0; i < s.Length; ++i) {
				AddCharacter (s [i]);
			}
		}

		public void RepeatCharacter (char c, int length)
		{
			for (var i = 0; i < length; ++i) {
				AddCharacter (c);
			}
		}

		public void RepeatCharacterVertical (char c, int length)
		{
			for (var i = 0; i < length; ++i) {
				AddCharacter (c);
				xPos--;
				yPos++;
			}
		}

		public void ClearLine ()
		{
			for (var x = X; x < Width; ++x) {
				characters [GetCharacterIndex (x, Y)] = new ConsoleCharacter () {
					Foreground = Foreground,
					Background = Background,
					Character = ' '
				};
			}
		}

		public void ScrollUp ()
		{
			for (var y = 0; y < Height - 1; ++y) {
				for (var x = 0; x < Width; ++x) {
					characters [GetCharacterIndex (x, y)] = characters [GetCharacterIndex (x, y + 1)];
				}
			}

			Move (0, Height - 1);
			ClearLine ();
		}
	}
}

