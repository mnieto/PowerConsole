using System;
using System.Collections.Generic;
using System.Linq;


namespace PowerConsole
{
    public class Choice
    {

        protected List<OptionItem> Choices { get; set; }
        protected ChoiceOptions Options { get; set; }
        public Choice(IEnumerable<string> choices, ChoiceOptions options = null) {
            if (choices == null) throw new ArgumentNullException(nameof(choices));

            if (options == null)
                Options = new ChoiceOptions();
            Choices = new List<OptionItem>(choices.Count());
            int i = 0;
            foreach (string item in choices) {
                Choices.Add(new OptionItem {
                    Value = GetChoiceValue(i++),
                    Text = item
                });
            }
        }

        public Choice(IEnumerable<OptionItem> choices, ChoiceOptions options = null) {
            if (options == null)
                Options = new ChoiceOptions();
            Choices = choices?.ToList() ?? throw new ArgumentNullException(nameof(choices));
        }


        public Choice(IEnumerable<string> choices, IEnumerable<char> keys, ChoiceOptions options = null) {
            if (options == null)
                Options = new ChoiceOptions();
            Options.NumerationStyle = ChoiceNumeration.FirstLetter;

            if (choices == null) throw new ArgumentNullException(nameof(choices));
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (choices.Count() != keys.Count())
                throw new ArgumentException($"{nameof(choices)} and {nameof(keys)} must have the same number of elements");

            int i = 0;
            var keyList = keys.ToList();
            foreach (string item in choices) {
                Choices.Add(new OptionItem {
                    Value = keyList[i++].ToString(),
                    Text = item
                });
            }
        }

        public void Write() {
            foreach (var item in Choices) {
                if (Options.NumerationStyle == ChoiceNumeration.FirstLetter) {
                    //TODO
                } else {
                    string format = Options.NumerationFormat + "{1}";
                    string text = string.Format(format, item.Value, item.Text);
                    System.Console.WriteLine(text);
                }
            }
        }


        private string GetChoiceValue(int index) {
            string value;
            switch (Options.NumerationStyle) {
                case ChoiceNumeration.AlphabeticLower:
                    value = ((char)index + 96).ToString();
                    break;
                case ChoiceNumeration.AlphabeticUpper:
                    value = ((char)index + 65).ToString();
                    break;
                default:
                    value = string.Empty;
                    break;
            }
            return value;
        }
    }

    public class ChoiceOptions
    {
        public ChoiceNumeration NumerationStyle { get; set; }
        public string NumerationFormat { get; set; }
        public ConsoleColor HightLightColor = Console.Colors.HightLightColor;

    }

    public enum ChoiceNumeration
    {
        Numeric,
        AlphabeticLower,
        AlphabeticUpper,
        FirstLetter
    }


    public class OptionItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}