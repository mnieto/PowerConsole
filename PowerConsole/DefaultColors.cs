using System;

namespace PowerConsole
{
    /// <summary>
    /// Defines the color schema
    /// </summary>
    public class DefaultColors
    {
        /// <summary>
        /// Color for Error messages
        /// </summary>
        public ConsoleColor ErrorColor { get; set; } = ConsoleColor.DarkRed;

        /// <summary>
        /// Color for Warning messages
        /// </summary>
        public ConsoleColor WarnColor { get; set; } = ConsoleColor.Yellow;

        /// <summary>
        /// Color for Info messages
        /// </summary>
        public ConsoleColor InfoColor { get; set; } = ConsoleColor.Blue;

        /// <summary>
        /// Standard back color
        /// </summary>
        public ConsoleColor BackColor {get; set; } = System.Console.BackgroundColor;

        /// <summary>
        /// Standard foreground color
        /// </summary>
        public ConsoleColor ForeColor {get; set; } = System.Console.ForegroundColor;

        /// <summary>
        /// Highlight titles, accelerator keys,...  
        /// </summary>
        public ConsoleColor HighlightColor {get; set; } = ConsoleColor.White;
    }
}
