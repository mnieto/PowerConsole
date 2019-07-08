using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PowerConsole.Components
{
    /// <summary>
    /// Shows a list of choices, and allow the user to select one
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// List of choices
        /// </summary>
        protected IEnumerable<string> Items { get; set; }

        /// <summary>
        /// Stores the valid keys that the user can type as choice selection
        /// </summary>
        protected HashSet<string> Keys { get; set; } = new HashSet<string>();

        /// <summary>
        /// Configuration options
        /// </summary>
        protected MenuOptions Options { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="choices">list of options</param>
        /// <param name="options">Menu configuration. If <c>null</c> default configuration is used</param>
        /// <exception cref="ArgumentNullException">if <paramref name="choices"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">if the number of choices are less than one or more than 25 (from a to z)</exception>
        public Menu(IEnumerable<string> choices, MenuOptions options = null) {
            Items = choices ?? throw new ArgumentNullException(nameof(choices));
            int items = choices.Count();
            if (items == 0 || items > 25)
                throw new ArgumentOutOfRangeException("Choices list must have one or more items and less than 26", nameof(choices));
            Options = options ?? new MenuOptions();
        }

        /// <summary>
        /// Shows the menu items and return the user's choice
        /// </summary>
        /// <returns>string with the user selected option</returns>
        public string Show() {
            var tokenizer = new AcceleratorCharTokenizer();
            int i = 0;
            
            foreach (var item in Items) {
                if (Options.NumerationStyle == MenuNumeration.AcceleratorKey) {
                    Console.WriteLine(tokenizer.Parse(item));
                    Keys.Add(GetAcceleratorChar(item).ToString());
                } else {
                    (string key, IEnumerable<ColorToken> tokens) = ComposeMenuItem(i, item);
                    Console.WriteLine(tokens);
                    Keys.Add(key);
                }
                i++;
            }
            return Console.ReadLine<string>(Options.ReadMessage, x => Keys.Contains(x));
        }

        private char GetAcceleratorChar(string item) {
            int pos = item.IndexOf('&');
            if (pos == -1 || pos + 1 == item.Length)
                throw new IndexOutOfRangeException($"& mark not found in {item} or there is no chars after it");
            return item[pos + 1];
        }

        private (string, IEnumerable<ColorToken>) ComposeMenuItem(int index, string item) {
            var regex = new Regex(@"(.*)\{0\}(.*)");
            var match = regex.Match(Options.NumerationFormat);
            if (!match.Success)
                throw new InvalidOperationException($"{nameof(Options.NumerationFormat)} must contain the {{0}} placeholder");
            string key = GetChoiceValue(index);
            var tokens = new List<ColorToken> {
                new ColorToken(match.Groups[1].Value),
                new ColorToken(key, Options.HighLightColor),
                new ColorToken(match.Groups[2].Value),
                new ColorToken(item)
            };
            return (key, tokens);
        }

        private string GetChoiceValue(int index) {
            string value;
            switch (Options.NumerationStyle) {
                case MenuNumeration.AlphabeticLower:
                    value = ((char)(index + 97)).ToString();
                    break;
                case MenuNumeration.AlphabeticUpper:
                    value = ((char)(index + 65)).ToString();
                    break;
                case MenuNumeration.Numeric:
                    value = (index + 1).ToString();
                    break;
                default:
                    value = string.Empty;
                    break;
            }
            return value;
        }
    }

    /// <summary>
    /// Configuration options for the <see cref="Menu"/> component
    /// </summary>
    public class MenuOptions
    {
        /// <summary>
        /// What style is used to numerate choices
        /// </summary>
        public MenuNumeration NumerationStyle { get; set; }

        /// <summary>
        /// Format string for the menu numeration. Must contain the {0} placeholder, but may contain any preceding or trailing text
        /// Default is <c>{0}. </c>.
        /// </summary>
        /// <remarks>
        /// If <see cref="NumerationStyle"/> is set to <see cref="MenuNumeration.AcceleratorKey"/> this property is ignored
        /// </remarks>
        public string NumerationFormat { get; set; } = "{0}. ";

        /// <summary>
        /// Used color to highlight the selection char. Default: the specified HightLightColor in <see cref="Console.Colors"/>
        /// </summary>
        public Color HighLightColor = new Color(Console.Colors.HighlightColor);

        /// <summary>
        /// Text shown after the choices are listed, waiting the user input
        /// </summary>
        public string ReadMessage { get; set; } = "Type selected option ";

    }

    /// <summary>
    /// What style is used to numerate choices in the <see cref="Menu"/> component
    /// </summary>
    public enum MenuNumeration
    {
        /// <summary>1,2,3...</summary>
        Numeric,

        /// <summary>a,b,c...</summary>
        AlphabeticLower,

        /// <summary>A,B,C...</summary>
        AlphabeticUpper,

        /// <summary>The first letter after the &amp; char is used as key value</summary>
        AcceleratorKey
    }

}