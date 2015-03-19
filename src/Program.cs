using System;
using Netcurses;
using System.Threading.Tasks;
using System.Threading;

namespace Example
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var win = new ConsoleWindow (80, 20);
			var screen = new ConsoleUpdater ();
			var scrolly = new ConsoleScrollWindow (20, 10);
			scrolly.Window.Background = ConsoleColor.DarkRed;

			win.Foreground = ConsoleColor.White;

			const int diff = 40;

			for (var anim = 0; anim < 100; ++anim) {
				win.Background = ConsoleColor.Black;
				win.Clear ();
				var x = (int)(Math.Sin (anim / 10.0f) * diff + diff);

				win.Background = ConsoleColor.DarkBlue;
				win.Foreground = ConsoleColor.Yellow;
				var xStart = x / 3;
				var yStart = x / 8;
				var borderWidth = x / 3;
				var borderHeight = x / 5 + 1;
				ConsoleBorder.Draw (win, xStart, yStart, borderWidth, borderHeight);
				win.Background = ConsoleColor.DarkGreen;
				win.Fill (xStart, yStart, borderWidth - 2, borderHeight - 2);
				win.Background = ConsoleColor.Black;
				win.Foreground = ConsoleColor.Yellow;

				if (x > 40) {
					win.Background = ConsoleColor.Cyan;
				} else {
					win.Background = ConsoleColor.Red;

				}
				scrolly.AddString ("This is a test", true);

				win.Move (x, 10);
				win.AddString ("Hello world!");
				win.Copy (scrolly.Window, x / 4, x / 10);
				screen.Update (win);
				Thread.Sleep (50);
			}
		}
	}
}
