using System;

namespace Netcurses
{
	public class ConsoleScrollWindow
	{
		readonly ConsoleArea window;

		public ConsoleArea Window { get { return window; } }

		public ConsoleScrollWindow (Size size)
		{
			window = new ConsoleArea (size);
		}

		void Newline ()
		{
			var y = window.CursorPosition.Y;
			if (y == window.Size.Height - 1) {
				window.ScrollUp ();
			} else {
				y += 1;
			}

			window.Move (new Position(0, y));
		}

		public void AddStringEx (string s)
		{
			for (var i = 0; i < s.Length; ++i) {
				var ch = s [i];
				if (ch == '\r') {
				} else if (ch == '\n') {
					Newline ();
				} else {
					window.AddCharacter (ch);
				}
			}
		}

		public void AddString (string s, bool scroll)
		{
			var clamp = s.Length;
			var clamped = false; 
			if (window.CursorPosition.X + s.Length >= window.Size.Width) {
				clamp = window.Size.Width - window.CursorPosition.X;
				clamped = true;
			}
			var first = s.Substring (0, clamp);
			var second = s.Substring (clamp);
			AddStringEx (first);
			if (!clamped) {
				return;
			}

			if (!scroll) {
				return;
			}
			Newline ();
			if (second.Length > 0) {
				AddString (second, scroll);
			}
		}
	}
}

