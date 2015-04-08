using System;

namespace Netcurses
{
	public static class ConsoleBorder
	{
		public static void Draw (ConsoleArea window, int x, int y, int width, int height)
		{
			if (width < 2 || height < 2) {
				return;
			}
			var left = x - width / 2;
			var right = left + width - 1;
			var top = y - height / 2;
			var bottom = top + height - 1;

			window.Move (new Position(left, top));
			window.AddCharacter (BoxDrawing.ULCORNER);
			window.RepeatCharacter (BoxDrawing.HLINE, width - 2);
			window.AddCharacter (BoxDrawing.URCORNER);

			window.Move (new Position(left, top + 1));
			window.RepeatCharacterVertical (BoxDrawing.VLINE, height - 2);
			window.Move (new Position(right, top + 1));
			window.RepeatCharacterVertical (BoxDrawing.VLINE, height - 2);

			window.Move (new Position(left, bottom));
			window.AddCharacter (BoxDrawing.LLCORNER);
			window.RepeatCharacter (BoxDrawing.HLINE, width - 2);
			window.AddCharacter (BoxDrawing.LRCORNER);

		}
	}
}

