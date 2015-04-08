using System;

namespace Netcurses
{
	static class BoxDrawing
	{
		public static int HLINE = 4194417;
		public static int VLINE = 4194424;

		public static int LRCORNER = 4194410;
		public static int ULCORNER = 4194412;
		public static int URCORNER = 4194411;
		public static int LLCORNER = 4194413;
	}

	public interface IDriver
	{
		void Move (Position position);

		void Add (ConsoleColor foreground, ConsoleColor background, int ch);

		void Refresh ();

		ConsoleKeyInfo ReadKey();
	}
}

