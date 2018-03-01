/*

MIT License

Copyright (c) 2015 Peter Bjorklund

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
﻿using System;
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

		[DllImport(CURSES_DLL)]
		extern public static int getch();

		[DllImport(CURSES_DLL)]
		extern public static void timeout(int v);

		public CursesDriver ()
		{
			initscr ();
			timeout(0);

			if (!has_colors ())
			{
				throw new Exception ("No colors man!");
			}

			if (start_color () != 0)
			{
				throw new Exception ("No start color man!");
			}

			if (!can_change_color ())
			{
				throw new Exception ("Can not change color");
			}
			use_default_colors ();

			
		}

		public void Move (Position position)
		{
			move (position.Y, position.X);
		}

		public void Add (ConsoleColor foreground, ConsoleColor background, int ch)
		{
			addch (ch);
		}

		public void Refresh ()
		{
			refresh ();
		}

		ConsoleKey KeyToKey(int ch)
		{
			switch (ch)
			{
			case 0x102:
				return ConsoleKey.DownArrow;
			}

			return ConsoleKey.NoName;
		}

		public ConsoleKeyInfo ReadKey ()
		{
			int ch = getch();
			var info = new ConsoleKeyInfo(' ', KeyToKey(ch), false, false, false); //throw new NotImplementedException ();

			return info;
		}
	}
}
