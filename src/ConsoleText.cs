using System;

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

			if (textLength <= maxCharacterCount) {
				return text;
			}
			var dotCount = Math.Min (3, maxCharacterCount);
			var displayFromOverflow = maxCharacterCount - dotCount;

			var overflowMarker = new string ('.', dotCount);
			string textToDisplay;
			if (align == TextAlign.Right) {
				textToDisplay = overflowMarker + text.Substring (text.Length - 1 - displayFromOverflow);
			} else {
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
			switch (align) {
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

