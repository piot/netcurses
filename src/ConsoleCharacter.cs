using System;
using System.Diagnostics;

namespace Netcurse
{
	public class ConsoleCharacter
	{
		public char Character { get; set; }

		public ConsoleColor Foreground { get; set; }

		public ConsoleColor Background { get; set; }

		public void Set (ConsoleCharacter ch)
		{
			Character = ch.Character;
			Foreground = ch.Foreground;
			Background = ch.Background;
		}

		public void Clear (ConsoleColor foreground, ConsoleColor background)
		{
			Character = ' ';
			Foreground = foreground;
			Background = background;
		}

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

