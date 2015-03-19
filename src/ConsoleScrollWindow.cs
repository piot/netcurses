using System;

namespace Netcurses
{
	public class ConsoleScrollWindow
	{
		readonly ConsoleWindow window;

		public ConsoleWindow Window { get { return window; } }

		public ConsoleScrollWindow (int width, int height)
		{
			window = new ConsoleWindow (width, height);
		}

		void Newline()
		{
			var y = window.Y;
			if (y == window.Height - 1) {
				window.ScrollUp ();
			} else {
				y += 1;
			}

			window.Move (0, y);
		}

		public void AddStringEx(string s)
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
			if (window.X + s.Length >= window.Width) {
				clamp = window.Width - window.X;
				clamped = true;
			}
			var first = s.Substring (0, clamp);
			var second = s.Substring (clamp);
			AddStringEx(first);
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

