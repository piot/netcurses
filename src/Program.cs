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
			const int WIDTH = 80;
			const int HEIGHT = 30;
			var defaultSize = new Size (WIDTH, HEIGHT);
			var win = new ConsoleArea (defaultSize);
			var driver = new ConsoleDriver ();
			// var driver = new CursesDriver ();
			var screen = new ConsoleUpdater (driver, defaultSize);
			var scrolly = new ConsoleScrollWindow (new Size (WIDTH - 2, 10));
			scrolly.Window.Background = ConsoleColor.DarkRed;

			win.Foreground = ConsoleColor.White;
			var input = new ConsoleInputField ();
			const int diff = 40;
			bool hide = false;

			for (var anim = 0; anim < 1000; ++anim) {
				win.Background = ConsoleColor.Blue;
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
				var p = new Position (xStart, yStart);
				var s = new Size (borderWidth - 2, borderHeight - 2);
				var rect = new Rectangle (p, s);
				win.Fill (rect);
				win.Background = ConsoleColor.Black;
				win.Foreground = ConsoleColor.Yellow;

				if (x > 40) {
					win.Background = ConsoleColor.Cyan;
				} else {
					win.Background = ConsoleColor.Red;

				}
				scrolly.AddString ("This is a test", true);
				win.Move (new Position (x, 10));
				win.AddString ("Hello world!");
				win.Background = ConsoleColor.DarkMagenta;
				win.Copy (scrolly.Window, new Position (40 - x / 6, 10 - x / 3));
				// ConsoleText.Draw (win, "THis is a field", 25, ConsoleText.TextAlign.Center);
				var info = driver.ReadKey ();
				var result = input.DoChar (info);
				if (result == InputResult.Enter) {
					hide = true;
				} else if (result == InputResult.Escape) {
					break;
				}
				win.Move (new Position (0, 15));
				if (!hide) {
					var adjustedCursorX = ConsoleText.Draw (win, input.Value, input.CursorX, 25, ConsoleText.TextAlign.Left);
					win.Move (new Position (adjustedCursorX, 15));
				}
				screen.Update (win);


				Task.Delay (10).Wait ();
			}
		}
	}
}
