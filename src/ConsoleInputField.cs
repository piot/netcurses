using System;

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

		public string Value { get { return value; } }

		public int CursorX { get { return cursorX; } }

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
			if (value.Length <= cursorX) {
				return;
			}
			value = value.Substring (0, cursorX) + value.Substring (cursorX + 1);
		}

		void Back ()
		{
			if (cursorX > 0) {
				cursorX--;
			}
		}

		public InputResult DoChar (ConsoleKeyInfo key)
		{
			var ch = key.KeyChar;
			if (ch >= ' ' && ch <= 'z') {
				AddChar (ch);
				return InputResult.Nothing;
			}
			switch (key.Key) {
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

