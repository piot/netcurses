using System;
using System.Runtime.InteropServices;

namespace Netcurses
{
	public class CursesDriver : IDriver
	{
		const string CURSES_DLL = "libncurses.dylib";

		#region Output

		[DllImport (CURSES_DLL)]
		extern public static int move (int line, int col);

		[DllImport (CURSES_DLL)]
		extern internal static int addch (int ch);

		[DllImport (CURSES_DLL)]
		extern public static int addstr (string s);

		#endregion

		[DllImport (CURSES_DLL)]
		extern static internal IntPtr initscr ();

		[DllImport (CURSES_DLL)]
		extern public static int refresh ();

		[DllImport (CURSES_DLL)]
		extern internal static int start_color ();

		[DllImport (CURSES_DLL)]
		extern internal static int init_pair (ushort pair, ushort f, ushort b);

		[DllImport (CURSES_DLL)]
		extern public static int attrset (int attrs);

		[DllImport (CURSES_DLL)]
		extern internal static int use_default_colors ();

		[DllImport (CURSES_DLL)]
		extern public static int attron (int attrs);

		[DllImport (CURSES_DLL)]
		extern internal static bool has_colors ();

		[DllImport (CURSES_DLL)]
		extern public static bool can_change_color ();

		public CursesDriver ()
		{
			initscr ();
			if (!has_colors ()) {
				throw new Exception ("No colors man!");
			}
			if (start_color () != 0) {
				throw new Exception ("No start color man!");
			}

			if (!can_change_color ()) {
				throw new Exception ("Can not change color");
			}
			use_default_colors ();


			for (ushort fg = 0; fg < 8; ++fg) {
				for (ushort bg = 0; bg < 8; ++bg) {
					ushort index = (ushort)((fg * 8 + bg) + 1);
					init_pair (index, fg, bg);
				}
			}
		}

		public void Move (Position position)
		{
			move (position.Y, position.X);
		}

		public void Add (ConsoleColor foreground, ConsoleColor background, int ch)
		{
			ushort colorIndex = (ushort)(((int)foreground * 8 + (int)background) + 1);
			int attribute = (colorIndex * 256);

			attrset (2 + attribute);
			addch (ch);
		}

		public void Refresh ()
		{
			refresh ();
		}

		public ConsoleKeyInfo ReadKey ()
		{
			throw new NotImplementedException ();
		}
	}
}

