using System;
using SysConsole = System.Console;
using System.ComponentModel.DataAnnotations;

namespace PowerConsole
{
    public partial class Console
    {
        public static ConsoleOptions Options { get; set; } = new ConsoleOptions();
        public static DefaultColors Colors { get; set; } = new DefaultColors();

        public static T ReadLineAt<T>(string message, int x, int y) {
            return ReadLineAt<T>(message, x, y, null);
        }

        /// <summary>
        /// Position the cursor in a coordinate and reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <param name="message">Optional prompt message</param>
        /// <param name="x">horizontal position. If <paramref name="message" /> is specified, the x position is the starting coordinate of the message. Position is zero based</param>
        /// <param name="x">vertical position. If <paramref name="message"/> is specified, the x position is the starting coordinate of the message. Position is zero based</param>
        /// <param name="validation">a validation object</param>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <exception cref="InvalidOperationException"> if the input or output streams are redirected<</exception>
        public static T ReadLineAt<T>(string message, int x, int y, params ValidationAttribute[] validations) {
            CheckRedirected();
            SysConsole.SetCursorPosition(x, y);
            return ReadLine<T>(message, validations);
        }


        /// <summary>
        /// Position the cursor in a coordinate and reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <param name="message">Optional prompt message</param>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <exception cref="InvalidOperationException"> if the input or output streams are redirected<</exception>
        public static T ReadLine<T>(string message) {
            return ReadLine<T>(message, null);
        }

        /// <summary>
        /// Position the cursor in a coordinate and reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <param name="message">Optional prompt message</param>
        /// <param name="validation">a validation object</param>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <exception cref="InvalidOperationException"> if the input or output streams are redirected<</exception
        public static T ReadLine<T>(string message, params ValidationAttribute[] validations) {
        bool retry = false;
            do {
                string value = string.Empty;
                int x = SysConsole.CursorLeft;
                int y = SysConsole.CursorTop;
                SysConsole.Write(message);
                try {
                    value = SysConsole.ReadLine();
                    T result = (T)Convert.ChangeType(value, typeof(T));
                    if (validations != null) {
                        foreach (var validation in validations) {
                            if (!validation.IsValid(result)) {
                                throw new ValidationException(validation.ErrorMessage, validation, result);
                            }
                        }
                    }
                    return result;
                } catch (Exception ex) {
                    if (Options.ThrowErrorOnInvalidInput)
                        throw;
                    if (Options.BeepOnError)
                        SysConsole.Beep();
                    bool reposition = true;
                    if (Options.ShowErrorMessages) {
                        ShowErrorMessage(ex.Message);
                    } else if (Options.StandardErrorMessages) {
                        SysConsole.Error.WriteLine(ex.Message);
                        reposition = false;
                    }

                    if (reposition) {
                        SysConsole.SetCursorPosition(x, y);
                        SysConsole.Write(new string(' ', (message?.Length ?? 0) + value.Length));
                        SysConsole.CursorLeft = x;
                    }
                    retry = true;
                }

            } while (retry);
            return default(T);

        }
        protected static void CheckRedirected() {
            if (SysConsole.IsInputRedirected || SysConsole.IsOutputRedirected) {
                throw new InvalidOperationException("Can't set cursor position on redirected stream");
            }
        }

        protected static void ShowErrorMessage(string message) {
            message = message.Substring(0, Math.Min(message.Length, SysConsole.LargestWindowWidth));

            //Save values phase
            int x = SysConsole.CursorLeft;
            int y = SysConsole.CursorTop;
            ConsoleColor foreColor = SysConsole.ForegroundColor;
            ConsoleColor backColor = SysConsole.BackgroundColor;

            //Write phase
            //This may overwrite legitime text. It's possible to read previously existing text from console to save it and restore after showing the message
            //https://stackoverflow.com/questions/12355378/read-from-location-on-console-c-sharp
            //https://docs.microsoft.com/en-us/windows/console/reading-and-writing-blocks-of-characters-and-attributes
            SysConsole.SetCursorPosition(0, SysConsole.WindowTop + SysConsole.WindowHeight - 1);
            SysConsole.ForegroundColor = Colors.ErrorColor;
            SysConsole.Write(message);

            //Wait and restore phase
            SysConsole.ForegroundColor = foreColor;
            SysConsole.BackgroundColor = backColor;
            if (Options.StatusMessagesDuration == 0) {
                SysConsole.ReadKey();
            } else {
                //It would be nice if we could wait in separate thread. In that case, be awere that the cursos position could be different from the stored in the save phase
                //https://stackoverflow.com/questions/46862475/changing-thread-context-in-c-sharp-console-application
                System.Threading.Thread.Sleep(Options.StatusMessagesDuration);
            }
            SysConsole.Write("\r" + new string(' ', message.Length));
            SysConsole.SetCursorPosition(x, y);
        }
    }
}