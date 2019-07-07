using System;
using System.Collections.Generic;
using System.Text;

namespace PowerConsole
{
    /// <summary>
    /// Split the string searching the <see cref="AcceleratorChar"/> as separator char
    /// </summary>
    public class AcceleratorCharTokenizer : ITokenizeString
    {
        /// <summary>
        /// Separator char. The next character to this is highlighted. If you need to write the accelerator char it self, repeat it twice
        /// </summary>
        public char AcceleratorChar { get; set; }

        /// <summary>
        /// Default color of the string. If <c>null</c> the current <see cref="DefaultColors.ForeColor"/> of <see cref="Console.Colors"/> is used
        /// </summary>
        public Color DefaultColor { get; set; }

        /// <summary>
        /// Highlighted color for the next char to <see cref="AcceleratorChar"/>. 
        /// If <c>null</c> the current <see cref="DefaultColors.HightLightColor"/> of <see cref="Console.Colors"/> is used
        /// </summary>
        public Color HighlightColor { get; set; }


        /// <summary>
        /// Constructor with default accelerator char
        /// </summary>
        /// <param name="highlightColor">Highlighted color for the next char to <see cref="AcceleratorChar"/>.</param>
        /// <param name="defaultColor">Default color of the string. If <c>null</c> the current <see cref="DefaultColors.ForeColor"/> of <see cref="Console.Colors"/> is used</param>
        public AcceleratorCharTokenizer(Color? highlightColor, Color? defaultColor = null) : this ('&', highlightColor, defaultColor) { }


        /// <summary>
        /// Constructor with default accelerator char
        /// </summary>
        /// <param name="acceleratorChar">the accelerator char marker</param>
        /// <param name="highlightColor">
        /// Highlighted color for the next char to <see cref="AcceleratorChar"/>. 
        /// If <c>null</c> the current <see cref="DefaultColors.HightLightColor"/> of <see cref="Console.Colors"/> is used
        /// </param>
        /// <param name="defaultColor">Default color of the string. If <c>null</c> the current <see cref="DefaultColors.ForeColor"/> of <see cref="Console.Colors"/> is used</param>
        public AcceleratorCharTokenizer(char acceleratorChar = '&', Color? highlightColor = null, Color? defaultColor = null) {
            AcceleratorChar = acceleratorChar;
            HighlightColor = highlightColor ?? new Color(Console.Colors.HightLightColor, Console.Colors.BackColor);
            DefaultColor = defaultColor ?? new Color(Console.Colors.ForeColor, Console.Colors.BackColor);
        }

        /// <summary>
        /// Search the <see cref="AcceleratorChar "/> and tokenize the string
        /// </summary>
        /// <param name="value">The string to be parsed</param>
        /// <returns>Collection of <see cref="ColorToken"/></returns>
        public IEnumerable<ColorToken> Parse(string value) {
            int i = 0;
            StringBuilder sb = new StringBuilder();
            while (i < value.Length) {
                char c = value[i];
                if (c == AcceleratorChar) {
                    if (i + 1 < value.Length) {
                        if (value[i + 1] == AcceleratorChar) {
                            sb.Append(c);
                        } else {
                            if (sb.Length > 0) {
                                yield return new ColorToken(sb.ToString(), DefaultColor);
                                sb.Clear();
                            }
                            yield return new ColorToken(value[i + 1].ToString(), HighlightColor);
                        }
                        i++;
                    } else {
                        yield return new ColorToken(sb.ToString(), DefaultColor);
                        sb.Clear();
                    }
                } else {
                    sb.Append(c);
                }
                i++;
            }
            if (sb.Length > 0)
                yield return new ColorToken(sb.ToString(), DefaultColor);
        }
    }
}
