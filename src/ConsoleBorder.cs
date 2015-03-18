using System;

namespace Netcurse
{
	public static class ConsoleBorder
	{
		public static void Draw (ConsoleWindow window, int x, int y, int width, int height)
		{
			if (width < 2 || height < 2) {
				return;
			}
			var left = x - width / 2;
			var right = left + width - 1;
			var top = y - height / 2;
			var bottom = top + height - 1;

			window.Move (left, top);
			window.AddCharacter ('┌');
			window.RepeatCharacter ('─', width - 2);
			window.AddCharacter ('┐');

			window.Move (left, top + 1);
			window.RepeatCharacterVertical ('│', height - 2);
			window.Move (right, top + 1);
			window.RepeatCharacterVertical ('│', height - 2);

			window.Move (left, bottom);
			window.AddCharacter ('└');
			window.RepeatCharacter ('─', width - 2);
			window.AddCharacter ('┘');

		}
	}
}

