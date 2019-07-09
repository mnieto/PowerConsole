using System;
using System.Collections.Generic;
using SysConsole = System.Console;

namespace PowerConsole
{
    public partial class Console
    {

		/// <summary>
        /// Writes the specified value
        /// </summary>
        /// <param name="value">The value to write</param>
        public static void Write(string value) {
            SysConsole.Write(value);
        }

        /// <summary>
        /// Writes the specified value
        /// </summary>
        /// <param name="value">The value to write</param>
        public static void Write(object value) {
            SysConsole.Write(value);
        }


        /// <summary>
        /// Writes the specified value with the specified color
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="color">The color to use</param>
        public static void Write(string value, Color color) {
            lock (_innerWriteLock) {
                Color prevColor = new Color(SysConsole.ForegroundColor, SysConsole.BackgroundColor);
                SysConsole.ForegroundColor = color.Foreground;
                SysConsole.BackgroundColor = color.Background;
                SysConsole.Write(value);
                SysConsole.ForegroundColor = prevColor.Foreground;
                SysConsole.BackgroundColor = prevColor.Background; 
            }
        }

        /// <summary>
        /// Writes the specified value with the specified color
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="color">The color to use</param>
        public static void Write(object value, Color color) {
            Write(value.ToString(), color);
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="parser">A parser that implements <see cref="ITokenizeString"/></param>
        public static void Write(string value, ITokenizeString parser) {
            Write(parser.Parse(value));
        }

        /// <summary>
        /// Writes a tokenized string
        /// </summary>
        /// <param name="token">chunks of the string</param>
        public static void Write(ColorToken token) {
            Write(token.Text, token.Color);
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="tokens">chunks of the string</param>
        public static void Write(IEnumerable<ColorToken> tokens) {
            foreach (var token in tokens) {
                Write(token.Text, token.Color);
            }
        }

        /// <summary>
        /// Writes the specified value at a specific position
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        public static void WriteAt(string value, int x, int y) {
            lock (_lockWrite) {
                SysConsole.SetCursorPosition(x, y);
                SysConsole.Write(value); 
            }
        }

        /// <summary>
        /// Writes the specified value at a specific position
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        public static void WriteAt(object value, int x, int y) {
            lock (_lockWrite) {
                SysConsole.SetCursorPosition(x, y);
                SysConsole.Write(value); 
            }
        }

        /// <summary>
        /// Writes the specified value at a specific position
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        /// <param name="color">The color to use</param>
        public static void WriteAt(string value, int x, int y, Color color) {
            lock (_lockWrite) {
                SysConsole.SetCursorPosition(x, y);
                Write(value, color); 
            }
        }

        /// <summary>
        /// Writes the specified value at a specific position
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        /// <param name="color">The color to use</param>
        public static void WriteAt(object value, int x, int y, Color color) {
            lock (_lockWrite) {
                SysConsole.SetCursorPosition(x, y);
                Write(value, color); 
            }
        }


        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        /// <param name="value">The value to write</param>
        /// <param name="parser">A parser that implements <see cref="ITokenizeString"/></param>
        public static void WriteAt(string value, int x, int y, ITokenizeString parser) {
            lock (_lockWrite) {
                SysConsole.SetCursorPosition(x, y);
                WriteAt(x, y, parser.Parse(value)); 
            }
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        /// <param name="tokens">chunks of the string</param>
        public static void WriteAt(int x, int y, IEnumerable<ColorToken> tokens) {
            lock (_lockWrite) {
                SysConsole.SetCursorPosition(x, y);
                foreach (var token in tokens) {
                    Write(token.Text, token.Color);
                } 
            }
        }

        /// <summary>
        /// Writes a new line terminator
        /// </summary>
        public static void WriteLine() {
            SysConsole.WriteLine();
        }

        /// <summary>
        /// Writes the specified value followed by a line terminator
        /// </summary>
        /// <param name="value">The value to write</param>
        public static void WriteLine(string value) {
            SysConsole.WriteLine(value);
        }

        /// <summary>
        /// Writes the specified value followed by a line terminator
        /// </summary>
        /// <param name="value">The value to write</param>
        public static void WriteLine(object value) {
            SysConsole.WriteLine(value);
        }

        /// <summary>
        /// Writes the specified value followed by a line terminator
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="color">The color to use</param>
        public static void WriteLine(string value, Color color) {
            WriteLine(new ColorToken(value, color));
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="parser">A parser that implements <see cref="ITokenizeString"/></param>
        public static void WriteLine(string value, ITokenizeString parser) {
            WriteLine(parser.Parse(value));
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="token">chunks of the string</param>
        public static void WriteLine(ColorToken token) {
            lock (_innerWriteLock) {
                Color prevColor = new Color(SysConsole.ForegroundColor, SysConsole.BackgroundColor);
                SysConsole.ForegroundColor = token.Color.Foreground;
                SysConsole.BackgroundColor = token.Color.Background;
                SysConsole.WriteLine(token.Text);
                SysConsole.ForegroundColor = prevColor.Foreground;
                SysConsole.BackgroundColor = prevColor.Background; 
            }
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="tokens">chunks of the string</param>
        public static void WriteLine(IEnumerable<ColorToken> tokens) {
            lock (_lockWrite) {
                Write(tokens);
                SysConsole.WriteLine(); 
            }
        }

    }
}
