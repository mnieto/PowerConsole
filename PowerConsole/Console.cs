using System;
using System.Collections.Generic;
using System.Text;
using SysConsole = System.Console;

namespace PowerConsole
{
    /// <summary>
    /// Console main class
    /// </summary>
    public partial class Console : IConsole
    {


        /// <summary>
        /// Internal console instance
        /// </summary>
        protected static IConsole console = new Console();

        /// <summary>
        /// Initializes and return a <see cref="Console"/> with default configuration
        /// </summary>
        /// <returns></returns>
        public static IConsole Create() {
            return Instance;
        }

        /// <summary>
        /// Configures the console with <see cref="ConsoleOptions"/>
        /// </summary>
        /// <param name="configurationExpression">Configuration action</param>
        /// <returns>Configured console</returns>
        public static IConsole Create(Action<ConsoleOptions> configurationExpression = null) {
            var configOptions = new ConsoleOptions();
            configurationExpression?.Invoke(configOptions);
            console.Options = configOptions;
            return console;
        }

        /// <summary>
        /// Returns the console instance
        /// </summary>
        public static IConsole Instance => console;

        /// <summary>
        /// Other way to access the <see cref="PowerConsole"/> options
        /// </summary>
        public ConsoleOptions Options { get; set; } = new ConsoleOptions();

        /// <summary>
        /// Default color set
        /// </summary>
        public DefaultColors Colors { get; set; } = new DefaultColors();


        /// <summary>
        /// Set cursor position
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <exception cref="InvalidOperationException">When the console streams are redirected</exception>
        public IConsole At(int x, int y) {
            CheckRedirected();
            SysConsole.SetCursorPosition(x, y);
            return this;
        }

        /// <summary>
        /// Raises an <see cref="InvalidOperationException"/> exception if the Console streams, StdIn or StdOut are redirected
        /// </summary>
        protected void CheckRedirected() {
            if (SysConsole.IsInputRedirected || SysConsole.IsOutputRedirected) {
                throw new InvalidOperationException("Can't set cursor position on redirected stream");
            }
        }



        private static readonly object _innerReadLock = new object();
        private static readonly object _innerWriteLock = new object();
        private static readonly object _lockRead = new object();
        private static readonly object _lockWrite = new object();

    }
}
