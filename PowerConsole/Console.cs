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

        private List<ColorTokenItem> questionBuffer = new List<ColorTokenItem>();
        private bool addToBuffer = true;

        /// <summary>
        /// Activates the "question buffer". If text entered in any Read method is not valid, the question is prompted again to the user
        /// </summary>
        /// <remarks>
        /// <para>
        /// The "question buffer acts in a way that any Write* method stores the written values. 
        /// If text entered in any Read method is not valid, the question is prompted again to the user
        /// until a valid input is entered. In that moment the buffer is cleared
        /// </para>
        /// <para>
        /// Do not mix this overload with any other Ask overloads, as the rest of overloads have the same behavior than the Write* methods,
        /// but the Ask version save the message into the buffer
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        ///     string phone = console
        ///         .Ask()
        ///         .Write("Type your phone ")
        ///         .ReadLine&lt;string&gt;("It's not a valid phone number", x =&gt; x.Length == 9);
        /// </code>
        /// </example>
        public IConsole Ask() {
            questionBuffer.Clear();
            addToBuffer = true;
            return this;
        }

        /// <summary>
        /// Writes a <paramref name="message"/> and stores it in the question buffer in case the response is not valid
        /// </summary>
        /// <param name="message">text to be shown</param>
        public IConsole Ask(IEnumerable<ColorToken> message) {
            questionBuffer.Clear();
            foreach (ColorToken item in message) {
                questionBuffer.Add(new ColorTokenItem(item));
            }
            Write(message);
            addToBuffer = false;
            return this;
        }

        /// <summary>
        /// Writes a <paramref name="message"/> and stores it in the question buffer in case the response is not valid
        /// </summary>
        /// <param name="message">text to be shown</param>
        public IConsole Ask(string message) {
            questionBuffer.Clear();
            questionBuffer.Add(new ColorTokenItem(new ColorToken(message)));
            Write(message);
            addToBuffer = false;
            return this;
        }

        /// <summary>
        /// Writes a <paramref name="message"/> and stores it in the question buffer in case the response is not valid
        /// </summary>
        /// <param name="message">text to be shown</param>
        /// <param name="color">color of the text</param>
        public IConsole Ask(string message, Color color) {
            questionBuffer.Clear();
            questionBuffer.Add(new ColorTokenItem(new ColorToken(message, color)));
            Write(message, color);
            addToBuffer = false;
            return this;
        }

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
