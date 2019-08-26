using System;

namespace PowerConsole
{

    /// <summary>
    /// Convenience extension methods for coloring instances of <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to <paramref name="foreColor"/></summary>
        public static ColorToken Color(this string text, ConsoleColor foreColor) {
            return new ColorToken(text, new Color(foreColor));
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Black</summary>
        public static ColorToken Black(this string text) {
            return text.Color(ConsoleColor.Black);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Blue</summary>
        public static ColorToken Blue(this string text) {
            return text.Color(ConsoleColor.Blue);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Cyan</summary>
        public static ColorToken Cyan(this string text) {
            return text.Color(ConsoleColor.Cyan);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkBlue</summary>
        public static ColorToken DarkBlue(this string text) {
            return text.Color(ConsoleColor.DarkBlue);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkCyan</summary>
        public static ColorToken DarkCyan(this string text) {
            return text.Color(ConsoleColor.DarkCyan);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkGray</summary>
        public static ColorToken DarkGray(this string text) {
            return text.Color(ConsoleColor.DarkGray);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkGreen</summary>
        public static ColorToken DarkGreen(this string text) {
            return text.Color(ConsoleColor.DarkGreen);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkMagenta</summary>
        public static ColorToken DarkMagenta(this string text) {
            return text.Color(ConsoleColor.DarkMagenta);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkRed</summary>
        public static ColorToken DarkRed(this string text) {
            return text.Color(ConsoleColor.DarkRed);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkYellow</summary>
        public static ColorToken DarkYellow(this string text) {
            return text.Color(ConsoleColor.DarkYellow);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Gray</summary>
        public static ColorToken Gray(this string text) {
            return text.Color(ConsoleColor.Gray);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Green</summary>
        public static ColorToken Green(this string text) {
            return text.Color(ConsoleColor.Green);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Magenta</summary>
        public static ColorToken Magenta(this string text) {
            return text.Color(ConsoleColor.Magenta);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Red</summary>
        public static ColorToken Red(this string text) {
            return text.Color(ConsoleColor.Red);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to White</summary>
        public static ColorToken White(this string text) {
            return text.Color(ConsoleColor.White);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Yellow</summary>
        public static ColorToken Yellow(this string text) {
            return text.Color(ConsoleColor.Yellow);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to <paramref name="backColor"/></summary>
        public static ColorToken On(this string text, ConsoleColor backColor) {
            return new ColorToken(text, new Color(Console.Instance.Colors.ForeColor, backColor));
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Black</summary>
        public static ColorToken OnBlack(this string text) {
            return text.On(ConsoleColor.Black);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Blue</summary>
        public static ColorToken OnBlue(this string text) {
            return text.On(ConsoleColor.Blue);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Cyan</summary>
        public static ColorToken OnCyan(this string text) {
            return text.On(ConsoleColor.Cyan);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkBlue</summary>
        public static ColorToken OnDarkBlue(this string text) {
            return text.On(ConsoleColor.DarkBlue);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkCyan</summary>
        public static ColorToken OnDarkCyan(this string text) {
            return text.On(ConsoleColor.DarkCyan);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkGray</summary>
        public static ColorToken OnDarkGray(this string text) {
            return text.On(ConsoleColor.DarkGray);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkGreen</summary>
        public static ColorToken OnDarkGreen(this string text) {
            return text.On(ConsoleColor.DarkGreen);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkMagenta</summary>
        public static ColorToken OnDarkMagenta(this string text) {
            return text.On(ConsoleColor.DarkMagenta);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkRed</summary>
        public static ColorToken OnDarkRed(this string text) {
            return text.On(ConsoleColor.DarkRed);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkYellow</summary>
        public static ColorToken OnDarkYellow(this string text) {
            return text.On(ConsoleColor.DarkYellow);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Gray</summary>
        public static ColorToken OnGray(this string text) {
            return text.On(ConsoleColor.Gray);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Green</summary>
        public static ColorToken OnGreen(this string text) {
            return text.On(ConsoleColor.Green);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Magenta</summary>
        public static ColorToken OnMagenta(this string text) {
            return text.On(ConsoleColor.Magenta);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Red</summary>
        public static ColorToken OnRed(this string text) {
            return text.On(ConsoleColor.Red);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to White</summary>
        public static ColorToken OnWhite(this string text) {
            return text.On(ConsoleColor.White);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Yellow</summary>
        public static ColorToken OnYellow(this string text) {
            return text.On(ConsoleColor.Yellow);
        }
    }
}
