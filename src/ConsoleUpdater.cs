using System;

namespace Netcurses
{
	public class ConsoleUpdater
	{
		readonly ConsoleWindow screenWindow = new ConsoleWindow (80, 25);

		void SetCursorPosition (int x, int y)
		{
			Console.SetCursorPosition (x, y);
		}

		void SetCharacterAndColor (ConsoleCharacter targetChar, ConsoleCharacter sourceChar)
		{
			Console.BackgroundColor = sourceChar.Background;
			Console.ForegroundColor = sourceChar.Foreground;
			Console.Write (sourceChar.Character);
		}

		public void Update (ConsoleWindow window)
		{
			Console.CursorVisible = false;
			int lastX = -1;
			int lastY = -1;


			var chars = window.ConsoleCharacters;
			for (var y = 0; y < window.Height; ++y) {
				for (var x = 0; x < window.Width; ++x) {
					var sourceIndex = window.GetCharacterIndex (x, y);
					var sourceChar = window.ConsoleCharacters [sourceIndex];
					var targetIndex = screenWindow.GetCharacterIndex (x, y);
					var targetChar = screenWindow.ConsoleCharacters [targetIndex];
					if (!sourceChar.IsSame (targetChar)) {
						if (x != lastX || y != lastY) {
							SetCursorPosition (x, y);
						}
						SetCharacterAndColor (targetChar, sourceChar);
						lastX = x + 1;
						lastY = y;
						screenWindow.ConsoleCharacters [targetIndex] = sourceChar;
					}
				}
			}
			// Console.CursorVisible = true;
		}
	}
}

