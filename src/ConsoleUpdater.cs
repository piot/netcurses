using System;
using System.Diagnostics;

namespace Netcurse
{
	public class ConsoleUpdater
	{
		readonly ConsoleWindow screenWindow = new ConsoleWindow (80, 25);

		public ConsoleUpdater ()
		{
		}

		public void Update (ConsoleWindow window)
		{
			Console.CursorVisible = false;
			var chars = window.ConsoleCharacters;
			for (var y = 0; y < window.Height; ++y) {
				for (var x = 0; x < window.Width; ++x) {
					var sourceChar = chars [y * window.Width + x];
					var targetChar = screenWindow.ConsoleCharacters [y * screenWindow.Width + x];
					if (!sourceChar.IsSame (targetChar)) {
						Console.SetCursorPosition (x, y);
						Console.BackgroundColor = sourceChar.Background;
						Console.ForegroundColor = sourceChar.Foreground;
						Console.Write (sourceChar.Character);
						targetChar.Set (sourceChar);
					}
				}
			}
			// Console.CursorVisible = true;
		}
	}
}

