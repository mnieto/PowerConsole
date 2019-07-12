using System;
using System.IO;
using SysConsole = System.Console;

namespace PowerConsole.Test
{
    /// <summary>
    /// Allows to redirect console input and output, so we can test the behavior of <see cref="PowerConsole.Console"/>
    /// </summary>
    internal class ConsoleRedirect
    {

        public Stream Redirect(string input, Action action) {
            using (var reader = new StringReader(input)) {
                return Redirect(reader, action);
            }
        }

        public Stream Redirect(TextReader reader, Action action) {

            if (reader == null)
                return Redirect(action);
            var standardIn = SysConsole.In;
            Stream result = null;
            SysConsole.SetIn(reader);

            result = Redirect(action);

            SysConsole.SetIn(standardIn);
            return result;
        }

        public Stream Redirect(Action action) {
            var standardOut = SysConsole.Out;
            Stream result = null;

            //We want not to dispose writer because we need to keep the underling stream open
            var writer = new StreamWriter(new MemoryStream());
            SysConsole.SetOut(writer);

            action?.Invoke();

            writer.Flush();
            result = writer.BaseStream;

            SysConsole.SetOut(standardOut);
            result.Position = 0;
            return result;
        }

    }
}
