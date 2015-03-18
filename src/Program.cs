using System;
using Netcurses;

namespace Example
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var win = new ConsoleWindow (80, 20);
			var screen = new ConsoleUpdater ();

			win.Foreground = ConsoleColor.White;

			const int diff = 40;

			for (var anim = 0; anim < 400000; ++anim) {
				win.Background = ConsoleColor.Black;
				win.Clear ();
				var x = (int)(Math.Sin (anim / 10000.0f) * diff + diff);
				ConsoleBorder.Draw (win, 40, 10, x / 2, x / 4 + 1);
				if (x > 40) {
					win.Background = ConsoleColor.Cyan;
				} else {
					win.Background = ConsoleColor.Red;

				}
				win.Move (x, 10);
				win.AddString ("Hello world!");
				screen.Update (win);
			}
		}
	}
}
