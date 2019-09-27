using System;
using System.ComponentModel.DataAnnotations;
using SysConsole = System.Console;
using PowerConsole.ValidationBehaviour;
using System.Text;
using System.Collections.Generic;

namespace PowerConsole
{
    /// <summary>
    /// Console main class
    /// </summary>
    public partial class Console : IConsole
    {

        /// <summary>
        /// Reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="readColor">Color of the user written text</param>
        public T ReadLine<T>(Color? readColor = null) {
            return ReadLine<T>(x => null, readColor);
        }

        /// <summary>
        /// Reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <param name="validations">a validation object</param>
        /// <typeparam name="T">Type of the returned value</typeparam>
        public T ReadLine<T>(params ValidationAttribute[] validations) {
            lock (_lockRead) {
                return ReadLine<T>(ConvertValidations<T>(validations)); 
            }
        }

        /// <summary>
        /// Reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="errorMessage">Error message to be shown in case of the input value is not valid</param>
        /// <param name="validationExpression">Function that receives the input value and returns <c>true</c> if it is valid or <c>false</c> if the input value is not valid</param>
        /// <param name="readColor">Color of the user written text</param>
        /// <returns>A validated value</returns>
        public T ReadLine<T>(string errorMessage, Func<T, bool> validationExpression, Color? readColor = null) {
            lock (_lockRead) {
                return ReadLine(ConvertValidations(validationExpression, errorMessage), readColor); 
            }
        }

        /// <summary>
        /// Reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="validationExpression">Function that receives the input value and returns <c>true</c> if it is valid or <c>false</c> if the input value is not valid</param>
        /// <param name="readColor">Color of the user written text</param>
        /// <returns>A validated value</returns>
        public T ReadLine<T>(Func<T, bool> validationExpression, Color? readColor = null) {
            lock (_lockRead) {
                return ReadLine(ConvertValidations(validationExpression, null), readColor); 
            }
        }

        /// <summary>
        /// Reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="validationExpression">Function that receives the input value and returns a <see cref="ValidationResult"/> object</param>
        /// <param name="readColor">Color of the user written text</param>
        /// <returns>A validated value</returns>
        public T ReadLine<T>(Func<T, ValidationResult> validationExpression, Color? readColor = null) {
            lock (_innerReadLock) {
                bool retry = false;
                do {
                    string value = string.Empty;
                    int x = 1, y = 1;

                    //For testing purposes, we redirect the input, so we can't get or set cursor position
                    var isRedirected = SysConsole.IsInputRedirected || SysConsole.In.GetType() != typeof(System.IO.TextReader);
                    if (!isRedirected) {    
                        x = SysConsole.CursorLeft;
                        y = SysConsole.CursorTop;
                    }
                    try {
                        value = InternalRead(readColor ?? new Color(Colors.ForeColor, Colors.BackColor));
                        T result = (T)Convert.ChangeType(value, typeof(T));
                        var validationResult = validationExpression?.Invoke(result);
                        if (validationResult != null) {
                            throw new ValidationException(validationResult.ErrorMessage);
                        }
                        addToBuffer = false;
                        questionBuffer.Clear();
                        return result;
                    } catch (Exception ex) {
                        if (Options.ThrowErrorOnInvalidInput)
                            throw;
                        var reposition = false;
                        foreach (IValidationBehavior behavior in Options.ValidationBehaviours) {
                            reposition |= behavior.ShowMessage(ex.Message);         //if any of the behaviors need to reposition the cursor
                        }
                        if (reposition && !isRedirected) {
                            SysConsole.SetCursorPosition(x, y);
                            SysConsole.Write(new string(' ', value.Length));
                            SysConsole.CursorLeft = x;
                        }
                        RepeatQuestion();
                        retry = true;
                    }

                } while (retry);
                addToBuffer = false;
                questionBuffer.Clear();
                return default(T); 
            }
        }

        /// <summary>
        /// Read a password string
        /// </summary>
        /// <param name="showMask">If <c>true</c> shows an * char for each real char. If <c>fakse</c> doesn't show any char</param>
        /// <remarks>
        /// <para>Enter will end the password typing. The enter char is discarded, but a new line is written in Console</para>
        /// <para>Delete key delete the last typed char</para>
        /// <para>ESC key clear all typed chars and start again</para>
        /// </remarks>
        /// <returns>typed password. If the password is empty, returns <c>null</c></returns>
        public string ReadPassword(bool showMask = false) {
            StringBuilder sb = new StringBuilder();
            bool isEnter = false;
            int len;
            while (!isEnter) {
                ConsoleKeyInfo key = SysConsole.ReadKey(true);
                switch (key.Key) {
                    case ConsoleKey.Backspace:
                        len = sb.Length;
                        if (len > 0) {
                            if (showMask) Write("\b \b");
                            sb = new StringBuilder(sb.ToString().Substring(0, len - 1));
                        }
                        continue;
                    case ConsoleKey.Enter:
                        sb.Append(key.KeyChar);
                        WriteLine();
                        isEnter = true;
                        break;
                    case ConsoleKey.Escape:
                        if (showMask) {
                            len = sb.Length;
                            string back = new string('\b', len);
                            Write(string.Concat(back, new string(' ', len), back));
                        }
                        sb.Clear();
                        break;
                    default:
                        sb.Append(key.KeyChar);
                        if (showMask) Write('*');
                        break;
                };
            }
            if (sb.Length > 0)
                return sb.ToString();
            return null;
        }


        /// <summary>
        /// Reads the typed text, in the specified <see cref="Color"/>
        /// </summary>
        /// <param name="color">What color use for the typed text</param>
        /// <returns></returns>
        protected static string InternalRead(Color color) {
            Color prevColor = new Color(SysConsole.ForegroundColor, SysConsole.BackgroundColor);
            SysConsole.ForegroundColor = color.Foreground;
            SysConsole.BackgroundColor = color.Background;
            string value = SysConsole.ReadLine();
            SysConsole.ForegroundColor = prevColor.Foreground;
            SysConsole.BackgroundColor = prevColor.Background;
            return value;
        }

        /// <summary>
        /// Converts a user friendly format of specify a validation and error message to the internal <see cref="ValidationResult"/>
        /// </summary>
        /// <typeparam name="T">Type of the input value</typeparam>
        /// <param name="validationExpression">Function that receives the input value and returns <c>true</c> if the input value is valid; otherwise, it returns <c>false</c></param>
        /// <param name="errorMessage">Error message to be shown in case of the input value is not valid</param>
        /// <returns>Function to be invoked after the user type the input value and convert <paramref name="validationExpression"/> to the internal <see cref="ValidationResult"/></returns>
        protected Func<T, ValidationResult> ConvertValidations<T>(Func<T, bool> validationExpression, string errorMessage) {
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
        protected Func<T, ValidationResult> ConvertValidations<T>(ValidationAttribute[] validations) {
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

        private void RepeatQuestion() {
            bool previousState = addToBuffer;
            addToBuffer = false;
            try {
                foreach (ColorTokenItem item in questionBuffer) {
                    if (item.NewLine)
                        WriteLine(item);
                    else
                        Write(item);
                }
            } finally {
                addToBuffer = previousState;
            }
        }


    }
}