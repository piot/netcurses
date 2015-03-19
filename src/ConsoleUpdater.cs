using System;

namespace Netcurses
{
	public class ConsoleUpdater
	{
		readonly ConsoleWindow screenWindow = new ConsoleWindow (80, 25);

		public void Update (ConsoleWindow window)
		{
			Console.CursorVisible = false;
			int lastX = -1;
			int lastY = -1;
			var chars = window.ConsoleCharacters;
			for (var y = 0; y < window.Height; ++y) {
				for (var x = 0; x < window.Width; ++x) {
					var sourceChar = chars [y * window.Width + x];
					var targetChar = screenWindow.ConsoleCharacters [y * screenWindow.Width + x];
					if (!sourceChar.IsSame (targetChar)) {
						if (x != lastX || y != lastY) {
							Console.SetCursorPosition (x, y);
						}
						Console.BackgroundColor = sourceChar.Background;
						Console.ForegroundColor = sourceChar.Foreground;
						Console.Write (sourceChar.Character);
						lastX = x + 1;
						lastY = y;
						targetChar.Set (sourceChar);
					}
				}
			}
			// Console.CursorVisible = true;
		}
	}
}

