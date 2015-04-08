using System;

namespace Netcurses
{
	public class ConsoleUpdater
	{
		readonly ConsoleArea screenWindow;
		readonly IDriver _driver;

		public ConsoleUpdater (IDriver driver, Size size)
		{
			_driver = driver;
			screenWindow = new ConsoleArea (size);
		}

		void SetCursorPosition (int x, int y)
		{
			_driver.Move (new Position (x, y));
		}

		void SetCharacterAndColor (ConsoleCharacter sourceChar)
		{
			_driver.Add (sourceChar.Foreground, sourceChar.Background, sourceChar.Character);
		}

		public void Update (ConsoleArea window)
		{
			Console.CursorVisible = false;
			int lastX = -1;
			int lastY = -1;
			var updateHeight = Math.Min (window.Size.Height, screenWindow.Size.Height);
			var updateWidth = Math.Min (window.Size.Width, screenWindow.Size.Width);

			var chars = window.ConsoleCharacters;
			for (var y = 0; y < updateHeight; ++y) {
				for (var x = 0; x < updateWidth; ++x) {
					var position = new Position (x, y);
					var sourceIndex = window.GetCharacterIndex (position);
					var sourceChar = window.ConsoleCharacters [sourceIndex];
					var targetIndex = screenWindow.GetCharacterIndex (position);
					var targetChar = screenWindow.ConsoleCharacters [targetIndex];
					if (!sourceChar.IsSame (targetChar)) {
						if (x != lastX || y != lastY) {
							SetCursorPosition (x, y);
						}
						SetCharacterAndColor (sourceChar);
						lastX = x + 1;
						lastY = y;
						screenWindow.ConsoleCharacters [targetIndex] = sourceChar;
					}
				}
			}
			_driver.Move (window.CursorPosition);
			_driver.Refresh ();
			// Console.CursorVisible = true;
		}
	}
}

