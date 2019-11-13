using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PowerConsole.Components;

namespace PowerConsole.Test.Components
{
    public class ChoiceTest
    {
        [Fact]
        public void choices_must_be_two_or_more() {
            var sut = new ChoiceOptions();
            List<string> optoins = new List<string> { "A" };
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Choices = optoins);
        }

        [Fact]
        public void choices_cannot_be_empty() {
            Assert.Throws<ArgumentNullException>(() => new Choice("question", ""));
        }

        [Fact]
        public void default_choice_index_matches_with_latest_upper_letter_choices() {
            var sut = new Choice("question", "abCdEf");
            Assert.Equal(4, sut.Options.DefaultChoiceIndex);
        }

        [Fact]
        public void setting_default_choice_option_changes_the_case_in_choices() {
            var sut = new Choice("question", "aBb");
            sut.Options.DefaultChoiceIndex = 0;
            Assert.Equal("A", sut.Options.Choices[0]);
            Assert.Equal("b", sut.Options.Choices[1]);
        }

        [Fact]
        public void default_choice_index_throws_exception_when_greater_than_choices_list() {
            var sut = new Choice("question", "abc");
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Options.DefaultChoiceIndex = 3);
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Options.DefaultChoiceIndex = -2);
        }
            
        [Fact]
        public void default_choices_are_Yn() {
            var sut = new Choice("question");
            Assert.Collection(sut.Options.Choices,
                x => Assert.Equal("Y", x),
                x => Assert.Equal("n", x)
            );
        }

        [Fact]
        public void invalid_cast_exception_trying_to_show_other_than_int_or_bool() {
            var sut = new Choice("question");
            Assert.Throws<InvalidCastException>(() => sut.Show<string>());
        }

        [Fact]
        public void cast_to_true_when_default_choice_is_selected() {
            var sut = new Choice("question");
            var redirect = new ConsoleRedirect();
            string typedText = "y";
            bool response = false;
            var output = redirect.Redirect(typedText, () => response = sut.Show<bool>());
            Assert.True(response);
        }

        [Fact]
        public void cast_to_1_when_default_choice_is_selected() {
            var sut = new Choice("question");
            var redirect = new ConsoleRedirect();
            string typedText = "y";
            int response = 0;
            var output = redirect.Redirect(typedText, () => response = sut.Show<int>());
            Assert.Equal(1, response);
        }
    }
}

