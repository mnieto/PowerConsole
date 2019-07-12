using PowerConsole.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace PowerConsole.Test.Components
{
    public class MenuTest
    {
        [Fact]
        public void menu_with_zero_items_throws_exception() {
            var items = new List<string>();
            Assert.Throws<ArgumentOutOfRangeException>(() => new Menu(items));
        }

        [Fact]
        public void menu_with_more_than_25_items_throws_exception() {
            var items = new List<string>();
            for (int i = 1; i <= 26; i++) {
                items.Add(i.ToString());
            }
            Assert.Throws<ArgumentOutOfRangeException>(() => new Menu(items));
        }

        [Fact]
        public void menu_with_default_options_shows_numbers() {
            var items = new string[] { "one", "two" };
            const string response = "2";

            var menu = new Menu(items);
            var format = new MenuOptions().NumerationFormat;

            var redirect = new ConsoleRedirect();
            var output = redirect.Redirect(response, () => menu.Show());
            using (var reader = new StreamReader(output)) {
                var choices = reader.ReadToEnd().Split(Environment.NewLine);
                Assert.Collection(choices.Take(2),  //Ignore the last line with the user prompt
                    x => Assert.Equal(string.Format(format, 1) + items[0], x),
                    x => Assert.Equal(string.Format(format, 2) + items[1], x)
                );
            }
        }

        [Fact]
        public void menu_with_lower_letter_numeration_format() {
            var items = new string[] { "one", "two" };
            const string response = "a";

            var menu = new Menu(items, new MenuOptions {
                NumerationStyle = MenuNumeration.AlphabeticLower
            });
            var format = menu.Options.NumerationFormat;

            var redirect = new ConsoleRedirect();
            var output = redirect.Redirect(response, () => menu.Show());
            using (var reader = new StreamReader(output)) {
                var choices = reader.ReadToEnd().Split(Environment.NewLine);
                Assert.Collection(choices.Take(2),  //Ignore the last line with the user prompt
                    x => Assert.Equal(string.Format(format, 'a') + items[0], x),
                    x => Assert.Equal(string.Format(format, 'b') + items[1], x)
                );
            }
        }


        [Fact]
        public void menu_with_upper_letter_numeration_format() {
            var items = new string[] { "one", "two" };
            const string response = "A";

            var menu = new Menu(items, new MenuOptions {
                NumerationStyle = MenuNumeration.AlphabeticUpper
            });
            var format = menu.Options.NumerationFormat;

            var redirect = new ConsoleRedirect();
            var output = redirect.Redirect(response, () => menu.Show());
            using (var reader = new StreamReader(output)) {
                var choices = reader.ReadToEnd().Split(Environment.NewLine);
                Assert.Collection(choices.Take(2),  //Ignore the last line with the user prompt
                    x => Assert.Equal(string.Format(format, 'A') + items[0], x),
                    x => Assert.Equal(string.Format(format, 'B') + items[1], x)
                );
            }
        }

        [Fact]
        public void menu_with_accelerator_key_numeration_format() {
            var items = new string[] { "&one", "&two" };
            const string response = "t";

            var menu = new Menu(items, new MenuOptions {
                NumerationStyle = MenuNumeration.AcceleratorKey
            });
            var format = menu.Options.NumerationFormat;

            var redirect = new ConsoleRedirect();
            var output = redirect.Redirect(response, () => menu.Show());
            using (var reader = new StreamReader(output)) {
                var choices = reader.ReadToEnd().Split(Environment.NewLine);
                Assert.Collection(choices.Take(2),  //Ignore the last line with the user prompt
                    x => Assert.Equal(items[0].Substring(1), x),
                    x => Assert.Equal(items[1].Substring(1), x)
                );
            }
        }

    }
}
