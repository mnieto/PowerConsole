using System;
using System.ComponentModel.DataAnnotations;
using SysConsole = System.Console;
using PowerConsole.ValidationBehaviour;
using System.Text;

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
            return ReadLineAt<T>(message, x, y, v => null);
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
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="message">Optional prompt message</param>
        /// <param name="x">horizontal position. If <paramref name="message" /> is specified, the x position is the starting coordinate of the message. Position is zero based</param>
        /// <param name="x">vertical position. If <paramref name="message"/> is specified, the x position is the starting coordinate of the message. Position is zero based</param>
        /// <param name="validationExpression">Function that receives the input value and returns a <see cref="ValidationResult"/> object</param>
        /// <returns>A validated value</returns>
        public static T ReadLineAt<T>(string message, int x, int y, Func<T, ValidationResult> validationExpression) {
            CheckRedirected();
            SysConsole.SetCursorPosition(x, y);
            return ReadLine<T>(message, validationExpression);
        }

        /// <summary>
        /// Position the cursor in a coordinate and reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="message">Optional prompt message</param>
        /// <param name="x">horizontal position. If <paramref name="message" /> is specified, the x position is the starting coordinate of the message. Position is zero based</param>
        /// <param name="x">vertical position. If <paramref name="message"/> is specified, the x position is the starting coordinate of the message. Position is zero based</param>
        /// <param name="errorMessage">Error message to be shown in case of the input value is not valid</param>
        /// <param name="validationExpression">Function that receives the input value and returns <c>true</c> if it is valid or <c>false</c> if the input value is not valid</param>
        /// <returns>A validated value</returns>
        public static T ReadLineAt<T>(string message, int x, int y, string errorMessage, Func<T, bool> validationExpression) {
            CheckRedirected();
            SysConsole.SetCursorPosition(x, y);
            return ReadLine(message, ConvertValidations(validationExpression, errorMessage));
        }

        /// <summary>
        /// Position the cursor in a coordinate and reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="message">Optional prompt message</param>
        /// <param name="x">horizontal position. If <paramref name="message" /> is specified, the x position is the starting coordinate of the message. Position is zero based</param>
        /// <param name="x">vertical position. If <paramref name="message"/> is specified, the x position is the starting coordinate of the message. Position is zero based</param>
        /// <param name="validationExpression">Function that receives the input value and returns <c>true</c> if it is valid or <c>false</c> if the input value is not valid</param>
        /// <returns>A validated value</returns>
        public static T ReadLineAt<T>(string message, int x, int y, Func<T, bool> validationExpression) {
            CheckRedirected();
            SysConsole.SetCursorPosition(x, y);
            return ReadLine(message, ConvertValidations(validationExpression, null));
        }
        
        /// <summary>
                 /// Position the cursor in a coordinate and reads a value of type T from console. Repeat the input until the typed value is valid.
                 /// </summary>
                 /// <param name="message">Optional prompt message</param>
                 /// <typeparam name="T">Type of the returned value</typeparam>
                 /// <exception cref="InvalidOperationException"> if the input or output streams are redirected<</exception>
        public static T ReadLine<T>(string message) {
            return ReadLine<T>(message, x => null);
        }

        /// <summary>
        /// Position the cursor in a coordinate and reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <param name="message">Optional prompt message</param>
        /// <param name="validation">a validation object</param>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <exception cref="InvalidOperationException"> if the input or output streams are redirected<</exception
        public static T ReadLine<T>(string message, params ValidationAttribute[] validations) {
            return ReadLine<T>(message, ConvertValidations<T>(validations));
        }

        /// <summary>
        /// Position the cursor in a coordinate and reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="message">Optional prompt message</param>
        /// <param name="errorMessage">Error message to be shown in case of the input value is not valid</param>
        /// <param name="validationExpression">Function that receives the input value and returns <c>true</c> if it is valid or <c>false</c> if the input value is not valid</param>
        /// <returns>A validated value</returns>
        public static T ReadLine<T>(string message, string errorMessage, Func<T, bool> validationExpression) {
            return ReadLine(message, ConvertValidations(validationExpression, errorMessage));
        }

        /// <summary>
        /// Position the cursor in a coordinate and reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="message">Optional prompt message</param>
        /// <param name="errorMessage">Error message to be shown in case of the input value is not valid</param>
        /// <param name="validationExpression">Function that receives the input value and returns <c>true</c> if it is valid or <c>false</c> if the input value is not valid</param>
        /// <returns>A validated value</returns>
        public static T ReadLine<T>(string message, Func<T, bool> validationExpression) {
            return ReadLine(message, ConvertValidations(validationExpression, null));
        }

        /// <summary>
        /// Position the cursor in a coordinate and reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="message">Optional prompt message</param>
        /// <param name="validationExpression">Function that receives the input value and returns a <see cref="ValidationResult"/> object</param>
        /// <returns>A validated value</returns>
        public static T ReadLine<T>(string message, Func<T, ValidationResult> validationExpression) {
        bool retry = false;
            do {
                string value = string.Empty;
                int x = SysConsole.CursorLeft;
                int y = SysConsole.CursorTop;
                SysConsole.Write(message);
                try {
                    value = SysConsole.ReadLine();
                    T result = (T)Convert.ChangeType(value, typeof(T));
                    var validationResult = validationExpression?.Invoke(result);
                    if (validationResult != null) {
                        throw new ValidationException(validationResult.ErrorMessage);
                    }
                    return result;
                } catch (Exception ex) {
                    if (Options.ThrowErrorOnInvalidInput)
                        throw;
                    var reposition = false;
                    foreach (IValidationBehavior behavior in Options.ValidationBehaviours) {
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

        /// <summary>
        /// Raises an <see cref="InvalidOperationException"/> exception if the Console streams, StdIn or StdOut are redirected
        /// </summary>
        protected static void CheckRedirected() {
            if (SysConsole.IsInputRedirected || SysConsole.IsOutputRedirected) {
                throw new InvalidOperationException("Can't set cursor position on redirected stream");
            }
        }


        /// <summary>
        /// Converts a user friendly format of specify a validation and error message to the internal <see cref="ValidationResult"/>
        /// </summary>
        /// <typeparam name="T">Type of the input value</typeparam>
        /// <param name="validationExpression">Function that receives the input value and returns <c>true</c> if the input value is valid; otherwise, it returns <c>false</c></param>
        /// <param name="errorMessage">Error message to be shown in case of the input value is not valid</param>
        /// <returns>Function to be invoked after the user type the input value and convert <paramref name="validationExpression"/> to the internal <see cref="ValidationResult"/></returns>
        protected static Func<T, ValidationResult> ConvertValidations<T>(Func<T, bool> validationExpression, string errorMessage) {
            Func<T, ValidationResult> Validator = x => {
                if (validationExpression(x))
                    return ValidationResult.Success;
                return new ValidationResult(errorMessage ?? string.Empty);  //ValidationResult requires a not null error message
            };
            return Validator;
        }

        /// <summary>
        /// Converts a user friendly format of specify a validation and error message to the internal <see cref="ValidationResult"/>
        /// </summary>
        /// <typeparam name="T">Type of the input value</typeparam>
        /// <param name="validations">ValidationAttribute descendants that validate the invput value</param>
        /// <returns>Function to be invoked after the user type the input value and convert <paramref name="validations"/> to the internal <see cref="ValidationResult"/></returns>
        protected static Func<T, ValidationResult> ConvertValidations<T>(ValidationAttribute[] validations) {
            if (validations == null)
                return x => ValidationResult.Success;

            Func<T, ValidationResult> Validator = x => {
                StringBuilder sb = new StringBuilder();
                foreach(ValidationAttribute v in validations) {
                    if (!v.IsValid(x)) {
                        sb.AppendLine(v.ErrorMessage);
                    }
                }
                if (sb.Length == 0)
                    return ValidationResult.Success;
                return new ValidationResult(sb.ToString().Substring(0, sb.Length - Environment.NewLine.Length));
            };

            return Validator;
        }



    }
}