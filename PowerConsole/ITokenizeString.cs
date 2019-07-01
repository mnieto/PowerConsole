using System;
using System.Collections.Generic;
using System.Text;

namespace PowerConsole
{
    /// <summary>
    /// Split a string in <see cref="ColorToken"/> objects
    /// </summary>
    public interface ITokenizeString
    {
        /// <summary>
        /// Parses the <paramref name="value"/> string and split it in tokens following the implementor patterns
        /// </summary>
        /// <param name="value">The string to be parsed</param>
        /// <returns>Collection of <see cref="ColorToken"/></returns>
        IEnumerable<ColorToken> Parse(string value);
    }
}
