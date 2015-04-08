using System;
using System.Collections.Generic;

namespace Netcurses
{
	public class ConsoleDriver : IDriver
	{
		Dictionary<int, char> boxDrawing = new Dictionary<int, char>() {
			{BoxDrawing.HLINE, '─'},
			{BoxDrawing.VLINE, '│'},
			{BoxDrawing.LRCORNER, '┙'},
			{BoxDrawing.ULCORNER, '┌'},
			{BoxDrawing.URCORNER, '┐'},
			{BoxDrawing.LLCORNER, '└'}
		};

		#region IDriver implementation

		public void Move (Position position)
		{
			Console.SetCursorPosition (position.X, position.Y);
		}

		public void Add (ConsoleColor foreground, ConsoleColor background, int charCode)
		{
			Console.BackgroundColor = background;
			Console.ForegroundColor = foreground;
			char ch = (char)charCode;
			char boxDrawingChar;
			if (boxDrawing.TryGetValue (charCode, out boxDrawingChar)) {
				ch = boxDrawingChar;
			}

			Console.Write ((char)ch);
		}

		public void Refresh ()
		{
		}


		public ConsoleKeyInfo ReadKey ()
		{
			if (Console.KeyAvailable) {
				return Console.ReadKey (true);
			} else {
				return new ConsoleKeyInfo ();
			}
		}
		#endregion
	}
}

