using System;
using System.Collections.Generic;
using SysConsole = System.Console;

namespace PowerConsole
{
    public partial class Console : IConsole
    {

        /// <summary>
        /// Writes the specified value
        /// </summary>
        /// <param name="value">The value to write</param>
        public IConsole Write(string value) {
            SysConsole.Write(value);
            if (addToBuffer) {
                questionBuffer.Add(new ColorTokenItem(value, false));
            }
            return this;
        }

        /// <summary>
        /// Writes the specified value
        /// </summary>
        /// <param name="value">The value to write</param>
        public IConsole Write(object value) {
            SysConsole.Write(value);
            if (addToBuffer) {
                questionBuffer.Add(new ColorTokenItem(value.ToString(), false));
            }
            return this;
        }


        /// <summary>
        /// Writes the specified value with the specified color
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="color">The color to use</param>
        public IConsole Write(string value, Color color) {
            lock (_innerWriteLock) {
                Color prevColor = new Color(SysConsole.ForegroundColor, SysConsole.BackgroundColor);
                SysConsole.ForegroundColor = color.Foreground;
                SysConsole.BackgroundColor = color.Background;
                SysConsole.Write(value);
                SysConsole.ForegroundColor = prevColor.Foreground;
                SysConsole.BackgroundColor = prevColor.Background;
            }
            if (addToBuffer) {
                questionBuffer.Add(new ColorTokenItem(value, color));
            }
            return this;
        }

        /// <summary>
        /// Writes the specified value with the specified color
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="color">The color to use</param>
        public IConsole Write(object value, Color color) {
            return Write(value.ToString(), color);
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="parser">A parser that implements <see cref="ITokenizeString"/></param>
        public IConsole Write(string value, ITokenizeString parser) {
            return Write(parser.Parse(value));
        }

        /// <summary>
        /// Writes a tokenized string
        /// </summary>
        /// <param name="token">chunks of the string</param>
        public IConsole Write(ColorToken token) {
            return Write(token.Text, token.Color);
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="tokens">chunks of the string</param>
        public IConsole Write(IEnumerable<ColorToken> tokens) {
            foreach (var token in tokens) {
                Write(token.Text, token.Color);
            }
            return this;
        }

        /// <summary>
        /// Writes a new line terminator
        /// </summary>
        public IConsole WriteLine() {
            SysConsole.WriteLine();
            if (addToBuffer) {
                questionBuffer.Add(new ColorTokenItem("", true));
            }
            return this;
        }

        /// <summary>
        /// Writes the specified value followed by a line terminator
        /// </summary>
        /// <param name="value">The value to write</param>
        public IConsole WriteLine(string value) {
            SysConsole.WriteLine(value);
            if (addToBuffer) {
                questionBuffer.Add(new ColorTokenItem(value, true));
            }
            return this;
        }

        /// <summary>
        /// Writes the specified value followed by a line terminator
        /// </summary>
        /// <param name="value">The value to write</param>
        public IConsole WriteLine(object value) {
            SysConsole.WriteLine(value);
            if (addToBuffer) {
                questionBuffer.Add(new ColorTokenItem(value.ToString(), true));
            }
            return this;
        }

        /// <summary>
        /// Writes the specified value followed by a line terminator
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="color">The color to use</param>
        public IConsole WriteLine(string value, Color color) {
            WriteLine(new ColorToken(value, color));
            return this;
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="parser">A parser that implements <see cref="ITokenizeString"/></param>
        public IConsole WriteLine(string value, ITokenizeString parser) {
            WriteLine(parser.Parse(value));
            return this;
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="token">chunks of the string</param>
        public IConsole WriteLine(ColorToken token) {
            lock (_innerWriteLock) {
                Color prevColor = new Color(SysConsole.ForegroundColor, SysConsole.BackgroundColor);
                SysConsole.ForegroundColor = token.Color.Foreground;
                SysConsole.BackgroundColor = token.Color.Background;
                SysConsole.WriteLine(token.Text);
                SysConsole.ForegroundColor = prevColor.Foreground;
                SysConsole.BackgroundColor = prevColor.Background;
            }
            if (addToBuffer) {
                questionBuffer.Add(new ColorTokenItem(token, true));
            }
            return this;
        }

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="tokens">chunks of the string</param>
        public IConsole WriteLine(IEnumerable<ColorToken> tokens) {
            lock (_lockWrite) {
                Write(tokens);
                WriteLine();
            }
            return this;
        }

    }
}
