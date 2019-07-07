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

        public override string ToString() {
            return $"({Foreground}, {Background})";
        }
    }
}
