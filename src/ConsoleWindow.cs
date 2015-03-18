using System;

namespace Netcurses
{
	public class ConsoleWindow
	{
		readonly ConsoleCharacter[] characters;
		int x;
		int y;
		int width;
		int height;

		public ConsoleColor Foreground { get; set; }

		public ConsoleColor Background { get; set; }

		public int Height {
			get { return height; }
		}

		public int Width {
			get { return width; }
		}

		public int Length {
			get { return width * height; }
		}

		public ConsoleCharacter[] ConsoleCharacters { get { return characters; } }

		public ConsoleWindow (int width, int height)
		{
			this.width = width;
			this.height = height;

			characters = new ConsoleCharacter[width * height];
			for (var i = 0; i < Length; ++i) {
				characters [i] = new ConsoleCharacter ();

			}
		}

		ConsoleCharacter GetCharacter (int x, int y)
		{
			if (x < 0 || x >= width || y < 0 || y >= height) {
				return null;
			}
			var consoleCharacter = characters [y * width + x];
			return consoleCharacter;
		}

		public void Clear ()
		{
			foreach (var ch in characters) {
				ch.Clear (Foreground, Background);
			}
		}

		public void Move (int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public void SetCharacter (int x, int y, char ch)
		{
			var character = GetCharacter (x, y);
			if (character == null) {
				return;
			}
			character.Character = ch;
			character.Foreground = Foreground;
			character.Background = Background;
		}


		public void AddCharacter (char ch)
		{
			SetCharacter (x, y, ch);
			x++;
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
				x--;
				y++;
			}
		}
	}
}

