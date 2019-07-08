using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PowerConsole
{
    /// <summary>
    /// Maintains the fore and back colors pair
    /// </summary>
    [DebuggerDisplay("({Foreground},{Background})")]
    public struct Color
    {

        private ConsoleColor? _foreground;
        private ConsoleColor? _background;

        /// <summary>
        /// Foreground color. If not assigned, it will return the current assigned <see cref="DefaultColors.ForeColor"/> of <see cref="Console.Colors"/>
        /// </summary>
        public ConsoleColor Foreground {
            get => _foreground ?? Console.Colors.ForeColor;
            set => _foreground = value;
        }

        /// <summary>
        /// Background color. If not assigned, it will return the current assigned <see cref="DefaultColors.BackColor"/> of <see cref="Console.Colors"/>
        /// </summary>
        public ConsoleColor Background {
            get => _background ?? Console.Colors.BackColor;
            set => _background = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="foreground">Foreground color. If not specified, default foreColor is used</param>
        /// <param name="background">Background color. If not specified, default backColor is used</param>
        public Color(ConsoleColor? foreground = null, ConsoleColor? background = null) {
            _foreground = foreground;
            _background = background;
        }

        /// <summary>
        /// Returns the colors representation
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"({Foreground}, {Background})";
        }

        ///<summary>Returns a <see cref="Color"/> with Black foreground color and default background color</summary>
        public static Color Black => new Color(ConsoleColor.Black);

        ///<summary>Returns a <see cref="Color"/> with DarkBlue foreground color and default background color</summary>
        public static Color DarkBlue => new Color(ConsoleColor.DarkBlue);

        ///<summary>Returns a <see cref="Color"/> with DarkGreen foreground color and default background color</summary>
        public static Color DarkGreen => new Color(ConsoleColor.DarkGreen);

        ///<summary>Returns a <see cref="Color"/> with DarkCyan foreground color and default background color</summary>
        public static Color DarkCyan => new Color(ConsoleColor.DarkCyan);

        ///<summary>Returns a <see cref="Color"/> with DarkRed foreground color and default background color</summary>
        public static Color DarkRed => new Color(ConsoleColor.DarkRed);

        ///<summary>Returns a <see cref="Color"/> with DarkMagenta foreground color and default background color</summary>
        public static Color DarkMagenta => new Color(ConsoleColor.DarkMagenta);

        ///<summary>Returns a <see cref="Color"/> with DarkYellow foreground color and default background color</summary>
        public static Color DarkYellow => new Color(ConsoleColor.DarkYellow);

        ///<summary>Returns a <see cref="Color"/> with Gray foreground color and default background color</summary>
        public static Color Gray => new Color(ConsoleColor.Gray);

        ///<summary>Returns a <see cref="Color"/> with DarkGray foreground color and default background color</summary>
        public static Color DarkGray => new Color(ConsoleColor.DarkGray);

        ///<summary>Returns a <see cref="Color"/> with Blue foreground color and default background color</summary>
        public static Color Blue => new Color(ConsoleColor.Blue);

        ///<summary>Returns a <see cref="Color"/> with Green foreground color and default background color</summary>
        public static Color Green => new Color(ConsoleColor.Green);

        ///<summary>Returns a <see cref="Color"/> with Cyan foreground color and default background color</summary>
        public static Color Cyan => new Color(ConsoleColor.Cyan);

        ///<summary>Returns a <see cref="Color"/> with Red foreground color and default background color</summary>
        public static Color Red => new Color(ConsoleColor.Red);

        ///<summary>Returns a <see cref="Color"/> with Magenta foreground color and default background color</summary>
        public static Color Magenta => new Color(ConsoleColor.Magenta);

        ///<summary>Returns a <see cref="Color"/> with Yellow foreground color and default background color</summary>
        public static Color Yellow => new Color(ConsoleColor.Yellow);

        ///<summary>Returns a <see cref="Color"/> with White foreground color and default background color</summary>
        public static Color White => new Color(ConsoleColor.White);

    }
}
