using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace PowerConsole.Test
{
    public class RegexTokenizerTest
    {
        [Fact]
        public void EmptyPatternThrowsException() {
            Assert.Throws<ArgumentNullException>(() =>
                new RegexTokenizer("", new Color())
            );
        }

        [Fact]
        public void BeginningMatchTest() {
            const string text = "123 some text";
            var sut = new RegexTokenizer(@"\d+", new Color(ConsoleColor.Blue));
            Assert.Collection(sut.Parse(text),
                x => Assert.Equal("123", x.Text),
                x => Assert.Equal(" some text", x.Text)
            );
        }

        [Fact]
        public void InMiddleMatchTest() {
            const string text = "2019/07/07 ERR error message";
            var sut = new RegexTokenizer("ERR", new Color(ConsoleColor.Yellow));
            Assert.Collection(sut.Parse(text),
                x => Assert.Equal("2019/07/07 ", x.Text),
                x => Assert.Equal("ERR", x.Text),
                x => Assert.Equal(" error message", x.Text)
            );
        }

        [Fact]
        public void EndingMatchTest() {
            const string text = "Error in machine ID 123";
            var sut = new RegexTokenizer(@"Machine id (\d+)", new Color(ConsoleColor.Blue), RegexOptions.IgnoreCase);
            Assert.Collection(sut.Parse(text),
                x => Assert.Equal("Error in machine ID ", x.Text),
                x => Assert.Equal("123", x.Text)
            );
        }

        [Fact]
        public void MultipleMatchesTest() {
            const string text = "Traffic light has Red, Yellow and Green colors";
            var sut = new RegexTokenizer(@"(Red|Yellow|Green)", new Color(ConsoleColor.Blue), RegexOptions.IgnoreCase);
            Assert.Collection(sut.Parse(text),
                x => Assert.Equal("Traffic light has ", x.Text),
                x => Assert.Equal("Red", x.Text),
                x => Assert.Equal(", ", x.Text),
                x => Assert.Equal("Yellow", x.Text),
                x => Assert.Equal(" and ", x.Text),
                x => Assert.Equal("Green", x.Text),
                x => Assert.Equal(" colors", x.Text)
            );
        }

        [Fact]
        public void NonCapturingGroupIsIgnored() {
            const string text = "2019/07/07 ERR some error message";
            var sut = new RegexTokenizer(@"\d{4}\/\d{2}\/\d{2} (?:ERR)*\s*(.+)", new Color(ConsoleColor.Red));
            Assert.Collection(sut.Parse(text),
                x => Assert.Equal("2019/07/07 ERR ", x.Text),
                x => Assert.Equal("some error message", x.Text)
            );
        }
    }
}
