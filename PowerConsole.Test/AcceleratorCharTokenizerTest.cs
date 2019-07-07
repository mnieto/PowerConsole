using System;
using Xunit;
using PowerConsole;
using System.Linq;

namespace PowerConsole.Test
{
    public class AcceleratorCharTokenizerTest
    {
        [Fact]
        public void ParseWithNoAcceleratorChar() {
            string text = "Hello world!";
            var parser = new AcceleratorCharTokenizer();
            var tokens = parser.Parse(text);

            Assert.Single(tokens);
            Assert.Equal(text, tokens.First().Text);
        }

        [Fact]
        public void ParseWithAcceleratorCharInMiddle() {
            string text = "Hello &world!";
            var parser = new AcceleratorCharTokenizer();
            var tokens = parser.Parse(text);

            Assert.Collection(tokens,
                x => Assert.Equal("Hello ", x.Text),
                x => Assert.Equal("w", x.Text),
                x => Assert.Equal("orld!", x.Text)
            );
        }

        [Fact]
        public void ParseWithEscapedAcceleratorChar() {
            string text = "Hello world && moon";
            var parser = new AcceleratorCharTokenizer();
            var tokens = parser.Parse(text);

            Assert.Single(tokens);
            Assert.Equal(text.Replace("&&", "&"), tokens.First().Text);
        }

        [Fact]
        public void ParseAtFirstChar() {
            string text = "&Hello world!";
            var parser = new AcceleratorCharTokenizer();
            var tokens = parser.Parse(text);

            Assert.Collection(tokens,
                x => Assert.Equal("H", x.Text),
                x => Assert.Equal(text.Substring(2), x.Text)
            );
        }

        [Fact]
        public void ParseAtEnd() {
            string text = "Hello &world&";
            var parser = new AcceleratorCharTokenizer();
            var tokens = parser.Parse(text);

            Assert.Collection(tokens,
                x => Assert.Equal("Hello ", x.Text),
                x => Assert.Equal("w", x.Text),
                x => Assert.Equal("orld", x.Text)
            );

        }

        [Fact]
        public void AcceleratorCharHasHighlightColor() {
            string text = "Hello &world&";
            var parser = new AcceleratorCharTokenizer();
            var tokens = parser.Parse(text);

            Assert.Equal(3, tokens.Count());
            var token = tokens.First();
            Assert.Equal(Console.Colors.ForeColor, token.Color.Foreground);
            Assert.Equal(Console.Colors.BackColor, token.Color.Background);

            token = tokens.Skip(1).First();
            Assert.Equal(Console.Colors.HightLightColor, token.Color.Foreground);
            Assert.Equal(Console.Colors.BackColor, token.Color.Background);

        }
    }
}
