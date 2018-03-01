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

namespace Netcurses
{
	public static class ConsoleText
	{
		public enum TextAlign
		{
			Left,
			Center,
			Right
		}

		public static string TruncateString (string text, TextAlign align, int maxCharacterCount)
		{
			var textLength = text.Length;

			if (textLength <= maxCharacterCount)
			{
				return text;
			}
			var dotCount = Math.Min (3, maxCharacterCount);
			var displayFromOverflow = maxCharacterCount - dotCount;

			var overflowMarker = new string ('.', dotCount);
			string textToDisplay;

			if (align == TextAlign.Right)
			{
				textToDisplay = overflowMarker + text.Substring (text.Length - 1 - displayFromOverflow);
			}
			else
			{
				textToDisplay = text.Substring (0, displayFromOverflow) + overflowMarker;
			}
			return textToDisplay;
		}

		public static int Draw (ConsoleArea window, string text, int cursorX, int characterCount, TextAlign align)
		{
			var textToDisplay = TruncateString (text, align, characterCount);

			var spaces = characterCount - textToDisplay.Length;
			var spacesBefore = 0;
			var spacesAfter = 0;
			var adjustedCursorX = cursorX;

			switch (align)
			{
			case TextAlign.Left:
				spacesAfter = spaces;
				break;
			case TextAlign.Center:
				spacesBefore = spaces / 2;
				spacesAfter = spaces - spacesBefore;
				adjustedCursorX += spacesBefore;
				break;
			case TextAlign.Right:
				spacesBefore = spaces;
				adjustedCursorX = spacesBefore - cursorX;
				break;
			}
			window.ClearCharacters (spacesBefore);
			window.AddString (textToDisplay);
			window.ClearCharacters (spacesAfter);

			return adjustedCursorX;
		}
	}
}
