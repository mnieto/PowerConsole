using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerConsole.Components
{
    /// <summary>
    /// Prompts a question with a preset of possible responses. One of them can be the default response
    /// </summary>
    public class Choice
    {
        private IConsole Console { get; set; }
        
        /// <summary>
        /// Gets the <see cref="ChoiceOptions"/> object that defines the behaviour of the Choice object
        /// </summary>
        public ChoiceOptions Options { get; private set; }

        /// <summary>
        /// Constructs a Choice object with default configuration values
        /// </summary>
        /// <param name="console"><see cref="IConsole"/> used to output</param>
        /// <param name="text">Text to show as question</param>
        /// <param name="choices">List of possible choices. See remarks for choices rules</param>
        /// <remarks>
        /// Each char of <paramref name="choices"/> is a possible response.
        /// If the char is upper letter is considered as the default response.
        /// If <paramref name="choices"/> has more than one upper letter, only the latest is considered the default response
        /// <para>If no choices is specified, [Y/n] are the default</para>
        /// </remarks>
        public Choice(IConsole console, ColorToken text, string choices = null) {
            Options = new ChoiceOptions();
            Options.Text = text ?? String.Empty;
            if (choices != null) {
                Options.SetChoices(choices);
            }
            Console = console ?? PowerConsole.Console.Instance;
        }

        /// <summary>
        /// Constructs a Choice object with default configuration values
        /// </summary>
        /// <param name="console"><see cref="IConsole"/> used to output</param>
        /// <param name="options"><see cref="ChoiceOptions"/> with the configuration to be used</param>
        public Choice(IConsole console, ChoiceOptions options) {
            Console = console ?? PowerConsole.Console.Instance;
            Options = options ?? throw new ArgumentException(nameof(options));
        }

        /// <summary>
        /// Constructs a Choice object with default configuration values
        /// </summary>
        /// <param name="text">Text to show as question</param>
        /// <param name="choices">List of possible choices. See remarks for choices rules</param>
        /// <remarks>
        /// Each char of <paramref name="choices"/> is a possible response.
        /// If the char is upper letter is considered as the default response.
        /// If <paramref name="choices"/> has more than one upper letter, only the latest is considered the default response
        /// <para>If no choices is specified, [Y/n] are the default</para>
        /// </remarks>
        public Choice(ColorToken text, string choices = null) : this(null, text, choices) { }

        /// <summary>
        /// Constructs a Choice object with default configuration values
        /// </summary>
        /// <param name="options"><see cref="ChoiceOptions"/> with the configuration to be used</param>
        public Choice(ChoiceOptions options) : this(null, options) { }

        /// <summary>
        /// Prompts the question to the user and returns the user's choice
        /// </summary>
        /// <remarks>
        /// If the user press ENTER the default choice value is returned
        /// </remarks>
        /// <returns>String with the user selected option</returns>
        public string Show() {
            string value = Console
                .Ask()
                .Write(Options.Text)
                .Write(FormatChoices())
                .ReadLine<string>(x => x == String.Empty || Options.Choices.Contains(x, StringComparer.CurrentCultureIgnoreCase));
            return value == string.Empty ? Options.Choices[Options.DefaultChoiceIndex] : value;
        }

        private IEnumerable<ColorToken> FormatChoices() {
            var result = new List<ColorToken>();
            result.Add(new ColorToken(Options.Brackets.Open.ToString()));
            int i = 0;
            foreach (string choice in Options.Choices) {
                if (i == Options.DefaultChoiceIndex) {
                    result.Add(new ColorToken(choice.ToUpper(), Console.Colors.HighlightColor));
                } else {
                    result.Add(new ColorToken(choice.ToLower()));
                }
                if (i < Options.Choices.Count() - 1) {
                    result.Add(new ColorToken(Options.SeparatorChar.ToString()));
                }
                i++;
            }
            result.Add(new ColorToken(Options.Brackets.Close.ToString()));
            return result;
        }
    }

    /// <summary>
    /// Configuration options for the <see cref="Choice"/> component
    /// </summary>
    public class ChoiceOptions
    {
        private int _defaultChoiceIndex = 0;
        private IList<string> _choices = new List<string> { "Y", "n" };


        /// <summary>
        /// List of possible accepted responses. If one of them is upper letter is considered as the default response
        /// </summary>
        public IList<string> Choices {
            get {
                return _choices;
            }
            set {
                if (value == null) throw new ArgumentNullException(nameof(Choices));
                if (value.Count < 2) throw new ArgumentOutOfRangeException(nameof(Choices));
                _choices = value;
            }
        } 

        internal void SetChoices(string choices) {
            if (string.IsNullOrEmpty(choices)) throw new ArgumentNullException(nameof(choices));
            if (choices.Length < 2) throw new ArgumentOutOfRangeException(nameof(choices));

            Choices.Clear();
            for (int i = 0; i < choices.Length; i++) {
                Choices.Add(choices[i].ToString());
                if (char.IsUpper(choices[i])) {
                    DefaultChoiceIndex = i;
                }
            }
        }

        /// <summary>
        /// Sets the default response index from the <see cref=" Choices"/> list
        /// </summary>
        /// <remarks>
        /// If no default choice is required, set this property to -1. 
        /// The default value is 0 (the first choice in the list)
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">If the index value is out the boundaries of the list of choices and is != -1</exception>
        public int DefaultChoiceIndex {
            get { return _defaultChoiceIndex; }
            set {
                if (value < -1 || value >= Choices.Count())
                    throw new ArgumentOutOfRangeException(nameof(DefaultChoiceIndex));
                SetChoiceCase(value);
                _defaultChoiceIndex = value;
            }
        }
        
        /// <summary>
        /// Char used to separate items in the list of choices. Default is /
        /// </summary>
        public char SeparatorChar { get; set; } = '/';
        
        /// <summary>
        /// Pair of chars used to enclose the choices list. Default is []
        /// </summary>
        public (char Open, char Close) Brackets { get; set; } = ('[', ']');
        
        /// <summary>
        /// <see cref="ColorToken"/> with the text to ask to the user
        /// </summary>
        public ColorToken Text { get; set; }

        private void SetChoiceCase(int index) {
            for (int i = 0; i < Choices.Count(); i++) {
                if (i == index) {
                    Choices[i] = Choices[i].ToUpper();
                } else {
                    Choices[i] = Choices[i].ToLower();
                }
            }
        }
    }
}
