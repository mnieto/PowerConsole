using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PowerConsole
{
    /// <summary>
    /// Tokenize a string based on a <see cref="Regex"/> expression
    /// </summary>
    /// <remarks>
    /// Tokens are built based on the matching expressions groups, not the matches themselves.
    /// For example, given the "Error in machine ID 123" text and the "Machine id (\d+)" pattern
    /// the tokenizer will return a default color string with Error in machine ID and
    /// other token with the selected color with the "123" text
    /// </remarks>
    public class RegexTokenizer : ITokenizeString
    {
        private Regex _regex;
        private Color _color;

        /// <summary>
        /// Initialize a <see cref="RegexTokenizer"/> with a regex pattern and the highlight color 
        /// </summary>
        /// <param name="pattern">Regex pattern</param>
        /// <param name="color"><see cref="Color"/> to highlight the matched text</param>
        /// <param name="regexOptions">Optional options for the regular expression</param>
        public RegexTokenizer(string pattern, Color color, RegexOptions? regexOptions = null) {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentNullException(nameof(pattern));

            _regex = regexOptions.HasValue ? new Regex(pattern, regexOptions.Value) : new Regex(pattern);
            _color = color;
        }

        /// <summary>
        /// Initialize a <see cref="RegexTokenizer"/> with a regex pattern and the highlight color 
        /// </summary>
        /// <param name="regex">Initialized <see cref="Regex"/></param>
        /// <param name="color"><see cref="Color"/> to highlight the matched text</param>
        public RegexTokenizer(Regex regex, Color color) {
            _regex = regex ?? throw new ArgumentNullException(nameof(regex));
            _color = color;
        }

        /// <summary>
        /// Parses the <paramref name="value"/> string and split it in tokens. 
        /// Matched text is highlighted, meanwhile the rest of text is written with default color
        /// </summary>
        /// <param name="value">The string to be parsed</param>
        /// <returns>Collection of <see cref="ColorToken"/></returns>
        public IEnumerable<ColorToken> Parse(string value) {
            MatchCollection matches = _regex.Matches(value);
            int start = 0;
            foreach (Match match in matches) {
                var groups = match.Groups;
                int i = match.Captures.Count == groups.Count ? 0 : 1;
                for(; i < groups.Count; i++) {
                    int length = groups[i].Index - start;
                    if (length > 0)
                        yield return new ColorToken(value.Substring(start, length));
                    yield return new ColorToken(value.Substring(groups[i].Index, groups[i].Length), _color);
                    start = groups[i].Index + groups[i].Length;
                }
            }
            if (start < value.Length)
                yield return new ColorToken(value.Substring(start));
        }
    }
}
