﻿using System;
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
        public static void Write(string value, ConsoleColor color) {
        ConsoleColor prevColor = SysConsole.ForegroundColor;
            SysConsole.ForegroundColor = color;
            SysConsole.Write(value);
            SysConsole.ForegroundColor = prevColor;
        }

        /// <summary>
        /// Writes the specified value with the specified color
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="color">The color to use</param>
        public static void Write(object value, ConsoleColor color) {
            ConsoleColor prevColor = SysConsole.ForegroundColor;
            SysConsole.ForegroundColor = color;
            SysConsole.Write(value);
            SysConsole.ForegroundColor = prevColor;
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
            Write(token.Text, token.Color.Foreground);
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="tokens">chunks of the string</param>
        public static void Write(IEnumerable<ColorToken> tokens) {
            foreach (var token in tokens) {
                Write(token.Text, token.Color.Foreground);
            }
        }

        /// <summary>
        /// Writes the specified value at a specific position
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        public static void WriteAt(string value, int x, int y) {
            SysConsole.SetCursorPosition(x, y);
            SysConsole.Write(value);
        }

        /// <summary>
        /// Writes the specified value at a specific position
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        public static void WriteAt(object value, int x, int y) {
            SysConsole.SetCursorPosition(x, y);
            SysConsole.Write(value);
        }

        /// <summary>
        /// Writes the specified value at a specific position
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        /// <param name="color">The color to use</param>
        public static void WriteAt(string value, int x, int y, ConsoleColor color) {
            SysConsole.SetCursorPosition(x, y);
            Write(value, color);
        }

        /// <summary>
        /// Writes the specified value at a specific position
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        /// <param name="color">The color to use</param>
        public static void WriteAt(object value, int x, int y, ConsoleColor color) {
            SysConsole.SetCursorPosition(x, y);
            Write(value, color);
        }


        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        /// <param name="value">The value to write</param>
        /// <param name="parser">A parser that implements <see cref="ITokenizeString"/></param>
        public static void WriteAt(string value, int x, int y, ITokenizeString parser) {
            SysConsole.SetCursorPosition(x, y);
            WriteAt(x, y, parser.Parse(value));
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        /// <param name="tokens">chunks of the string</param>
        public static void WriteAt(int x, int y, IEnumerable<ColorToken> tokens) {
            SysConsole.SetCursorPosition(x, y);
            foreach (var token in tokens) {
                Write(token.Text, token.Color.Foreground);
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
        public static void WriteLine(string value, ConsoleColor color) {
            ConsoleColor prevColor = SysConsole.ForegroundColor;
            SysConsole.ForegroundColor = color;
            SysConsole.WriteLine(value);
            SysConsole.ForegroundColor = prevColor;
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
        /// <param name="tokens">chunks of the string</param>
        public static void WriteLine(IEnumerable<ColorToken> tokens) {
            Write(tokens);
            SysConsole.WriteLine();
        }

    }
}
