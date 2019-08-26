using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PowerConsole
{
    /// <summary>
    /// Console main interface
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// Default color set
        /// </summary>
        DefaultColors Colors { get; set; }

        /// <summary>
        /// Other way to access the <see cref="PowerConsole"/> options
        /// </summary>
        ConsoleOptions Options { get; set; }

        /// <summary>
        /// Set cursor position
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <exception cref="InvalidOperationException">When the console streams are redirected</exception>
        IConsole At(int x, int y);

        /// <summary>
        /// Reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="readColor">Color of the user written text</param>
        T ReadLine<T>(Color? readColor = null);

        /// <summary>
        /// Reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="validationExpression">Function that receives the input value and returns <c>true</c> if it is valid or <c>false</c> if the input value is not valid</param>
        /// <param name="readColor">Color of the user written text</param>
        /// <returns>A validated value</returns>
        T ReadLine<T>(Func<T, bool> validationExpression, Color? readColor = null);

        /// <summary>
        /// Reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="validationExpression">Function that receives the input value and returns a <see cref="ValidationResult"/> object</param>
        /// <param name="readColor">Color of the user written text</param>
        /// <returns>A validated value</returns>
        T ReadLine<T>(Func<T, ValidationResult> validationExpression, Color? readColor = null);

        /// <summary>
        /// Reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <param name="validations">a validation object</param>
        /// <typeparam name="T">Type of the returned value</typeparam>
        T ReadLine<T>(params ValidationAttribute[] validations);

        /// <summary>
        /// Reads a value of type T from console. Repeat the input until the typed value is valid.
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="errorMessage">Error message to be shown in case of the input value is not valid</param>
        /// <param name="validationExpression">Function that receives the input value and returns <c>true</c> if it is valid or <c>false</c> if the input value is not valid</param>
        /// <param name="readColor">Color of the user written text</param>
        /// <returns>A validated value</returns>
        T ReadLine<T>(string errorMessage, Func<T, bool> validationExpression, Color? readColor = null);

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
        string ReadPassword(bool showMask = false);

        /// <summary>
        /// Writes a tokenized string
        /// </summary>
        /// <param name="token">chunks of the string</param>
        IConsole Write(ColorToken token);

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="tokens">chunks of the string</param>
        IConsole Write(IEnumerable<ColorToken> tokens);

        /// <summary>
        /// Writes the specified value
        /// </summary>
        /// <param name="value">The value to write</param>
        IConsole Write(object value);

        /// <summary>
        /// Writes the specified value with the specified color
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="color">The color to use</param>
        IConsole Write(object value, Color color);

        /// <summary>
        /// Writes the specified value
        /// </summary>
        /// <param name="value">The value to write</param>
        IConsole Write(string value);

        /// <summary>
        /// Writes the specified value with the specified color
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="color">The color to use</param>
        IConsole Write(string value, Color color);

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="parser">A parser that implements <see cref="ITokenizeString"/></param>
        IConsole Write(string value, ITokenizeString parser);

        /// <summary>
        /// Writes a new line terminator
        /// </summary>
        IConsole WriteLine();

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="token">chunks of the string</param>
        IConsole WriteLine(ColorToken token);

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="tokens">chunks of the string</param>
        IConsole WriteLine(IEnumerable<ColorToken> tokens);

        /// <summary>
        /// Writes the specified value followed by a line terminator
        /// </summary>
        /// <param name="value">The value to write</param>
        IConsole WriteLine(object value);

        /// <summary>
        /// Writes the specified value followed by a line terminator
        /// </summary>
        /// <param name="value">The value to write</param>
        IConsole WriteLine(string value);

        /// <summary>
        /// Writes the specified value followed by a line terminator
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="color">The color to use</param>
        IConsole WriteLine(string value, Color color);

        /// <summary>
        /// Writes a tokenized string, each chunk with its color specification
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="parser">A parser that implements <see cref="ITokenizeString"/></param>
        IConsole WriteLine(string value, ITokenizeString parser);
    }
}