using System;
using System.Collections.Generic;
using System.Text;

namespace PowerConsole
{
    /// <summary>
    /// String chunk with color specification
    /// </summary>
    public class ColorToken
    {
        /// <summary>
        /// The color of the text
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// The chunk of text
        /// </summary>
        public string Text { get; set; }


        /// <summary>
        /// Constructor that initializes the text and color
        /// </summary>
        /// <param name="text">chunk of text</param>
        /// <param name="color">color of the text</param>
        /// <remarks>
        /// If <paramref name="color"/> is not specified, the default <see cref="DefaultColors.ForeColor"/> and <see cref="DefaultColors.BackColor"/> of <see cref="Console.Colors"/> are used
        /// </remarks>
        public ColorToken(string text, Color? color = null) {
            Text = text;
            if (color.HasValue)
                Color = color.Value;
            else
                Color = new Color(Console.Colors.ForeColor, Console.Colors.BackColor);
        }

        /// <summary>
        /// Constructor that initializes the text and color
        /// </summary>
        /// <param name="text">chunk of text</param>
        /// <param name="foreground">foreground color of the text</param>
        /// <param name="background">background color of the text</param>
        public ColorToken(string text, ConsoleColor foreground, ConsoleColor background) : this (text, new Color(foreground, background)) { }


        /// <summary>
        /// Implicit conversion from string to <see cref="ColorToken"/>
        /// </summary>
        /// <example>
        /// This is useful to construct expressions like;
        /// <code>
        /// var tokens = new List&lt;ColorToken&gt;() {
        ///     "Token with default color",
        ///     "Blue text".OnBlue()
        /// };
        /// </code>
        /// </example>
        /// <param name="text"></param>
        public static implicit operator ColorToken(string text) {
            return new ColorToken(text);
        }
    }
}
