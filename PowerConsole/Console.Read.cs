using System;
using System.ComponentModel.DataAnnotations;
using SysConsole = System.Console;
using PowerConsole.ValidationBehaviour;

namespace PowerConsole
{
    public partial class Console
    {

        public static void Configure(Action<ConsoleOptions> configurationExpression = null) {
            var configOptions = new ConsoleOptions();
            configurationExpression?.Invoke(configOptions);
            Options = configOptions;
        }
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
                                //TODO: Use AggregateException to run all the validations?
                                throw new ValidationException(validation.ErrorMessage, validation, result);
                            }
                        }
                    }
                    return result;
                } catch (Exception ex) {
                    if (Options.ThrowErrorOnInvalidInput)
                        throw;
                    var reposition = false;
                    foreach(IValidationBehavior behavior in Options.ValidationBehaviours) {
                        reposition |= behavior.ShowMessage(ex.Message);         //if any of the behaviors need to reposition the cursor
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

    }
}