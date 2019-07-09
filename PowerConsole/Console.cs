using System;
using System.Collections.Generic;
using System.Text;

namespace PowerConsole
{
    /// <summary>
    /// Console main class
    /// </summary>
    public partial class Console
    {
        /// <summary>
        /// Allows to configure the behavior of <see cref="PowerConsole"/>
        /// </summary>
        /// <param name="configurationExpression"><see cref="Action{ConsoleOptions}"/></param>
        public static void Configure(Action<ConsoleOptions> configurationExpression = null) {
            var configOptions = new ConsoleOptions();
            configurationExpression?.Invoke(configOptions);
            Options = configOptions;
        }

        /// <summary>
        /// Other way to access the <see cref="PowerConsole"/> options
        /// </summary>
        public static ConsoleOptions Options { get; set; } = new ConsoleOptions();

        /// <summary>
        /// Default color set
        /// </summary>
        public static DefaultColors Colors { get; set; } = new DefaultColors();

        private static readonly object _innerReadLock = new object();
        private static readonly object _innerWriteLock = new object();
        private static readonly object _lockRead = new object();
        private static readonly object _lockWrite = new object();

    }
}
