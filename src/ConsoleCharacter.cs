using System;

namespace Netcurses
{
	public struct  ConsoleCharacter
	{
		public char Character;

		public ConsoleColor Foreground;

		public ConsoleColor Background;

		public bool IsSame (ConsoleCharacter ch)
		{
			if (ch.Character != Character) {
				return false;
			}

			if (ch.Foreground != Foreground) {
				return false;
			}

			if (ch.Background != Background) {

				return false;
			}

			return true;
		}
	}
}

