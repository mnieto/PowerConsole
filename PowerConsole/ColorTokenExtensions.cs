using System;

namespace PowerConsole
{

    /// <summary>
    /// Convenience extension methods for re-coloring instances of <see cref="ColorToken"/>.
    /// </summary>
    public static class ColorTokenExtensions
    {
        /// <summary>Returns a <see cref="ColorToken"/> with background color set to <paramref name="backColor"/></summary>
        public static ColorToken On(this ColorToken token, ConsoleColor backColor) {
            return new ColorToken(token.Text, token.Color.Foreground, backColor);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Black</summary>
        public static ColorToken OnBlack(this ColorToken token) {
            return token.On(ConsoleColor.Black);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Blue</summary>
        public static ColorToken OnBlue(this ColorToken token) {
            return token.On(ConsoleColor.Blue);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Cyan</summary>
        public static ColorToken OnCyan(this ColorToken token) {
            return token.On(ConsoleColor.Cyan);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkBlue</summary>
        public static ColorToken OnDarkBlue(this ColorToken token) {
            return token.On(ConsoleColor.DarkBlue);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkCyan</summary>
        public static ColorToken OnDarkCyan(this ColorToken token) {
            return token.On(ConsoleColor.DarkCyan);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkGray</summary>
        public static ColorToken OnDarkGray(this ColorToken token) {
            return token.On(ConsoleColor.DarkGray);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkGreen</summary>
        public static ColorToken OnDarkGreen(this ColorToken token) {
            return token.On(ConsoleColor.DarkGreen);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkMagenta</summary>
        public static ColorToken OnDarkMagenta(this ColorToken token) {
            return token.On(ConsoleColor.DarkMagenta);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkRed</summary>
        public static ColorToken OnDarkRed(this ColorToken token) {
            return token.On(ConsoleColor.DarkRed);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to DarkYellow</summary>
        public static ColorToken OnDarkYellow(this ColorToken token) {
            return token.On(ConsoleColor.DarkYellow);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Gray</summary>
        public static ColorToken OnGray(this ColorToken token) {
            return token.On(ConsoleColor.Gray);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Green</summary>
        public static ColorToken OnGreen(this ColorToken token) {
            return token.On(ConsoleColor.Green);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Magenta</summary>
        public static ColorToken OnMagenta(this ColorToken token) {
            return token.On(ConsoleColor.Magenta);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Red</summary>
        public static ColorToken OnRed(this ColorToken token) {
            return token.On(ConsoleColor.Red);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to White</summary>
        public static ColorToken OnWhite(this ColorToken token) {
            return token.On(ConsoleColor.White);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with background color set to Yellow</summary>
        public static ColorToken OnYellow(this ColorToken token) {
            return token.On(ConsoleColor.Yellow);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to <paramref name="color"/></summary>
        public static ColorToken Color(this ColorToken token, ConsoleColor color) {
            return new ColorToken(token.Text, color, token.Color.Background);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Black</summary>
        public static ColorToken Black(this ColorToken token) {
            return token.Color(ConsoleColor.Black);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Blue</summary>
        public static ColorToken Blue(this ColorToken token) {
            return token.Color(ConsoleColor.Blue);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Cyan</summary>
        public static ColorToken Cyan(this ColorToken token) {
            return token.Color(ConsoleColor.Cyan);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkBlue</summary>
        public static ColorToken DarkBlue(this ColorToken token) {
            return token.Color(ConsoleColor.DarkBlue);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkCyan</summary>
        public static ColorToken DarkCyan(this ColorToken token) {
            return token.Color(ConsoleColor.DarkCyan);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkGray</summary>
        public static ColorToken DarkGray(this ColorToken token) {
            return token.Color(ConsoleColor.DarkGray);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkGreen</summary>
        public static ColorToken DarkGreen(this ColorToken token) {
            return token.Color(ConsoleColor.DarkGreen);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkMagenta</summary>
        public static ColorToken DarkMagenta(this ColorToken token) {
            return token.Color(ConsoleColor.DarkMagenta);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkRed</summary>
        public static ColorToken DarkRed(this ColorToken token) {
            return token.Color(ConsoleColor.DarkRed);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to DarkYellow</summary>
        public static ColorToken DarkYellow(this ColorToken token) {
            return token.Color(ConsoleColor.DarkYellow);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Gray</summary>
        public static ColorToken Gray(this ColorToken token) {
            return token.Color(ConsoleColor.Gray);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Green</summary>
        public static ColorToken Green(this ColorToken token) {
            return token.Color(ConsoleColor.Green);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Magenta</summary>
        public static ColorToken Magenta(this ColorToken token) {
            return token.Color(ConsoleColor.Magenta);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Red</summary>
        public static ColorToken Red(this ColorToken token) {
            return token.Color(ConsoleColor.Red);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to White</summary>
        public static ColorToken White(this ColorToken token) {
            return token.Color(ConsoleColor.White);
        }

        /// <summary>Returns a <see cref="ColorToken"/> with foreground color set to Yellow</summary>
        public static ColorToken Yellow(this ColorToken token) {
            return token.Color(ConsoleColor.Yellow);
        }
    }
}




