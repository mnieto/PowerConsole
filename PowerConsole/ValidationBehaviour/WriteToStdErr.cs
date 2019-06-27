using System;
using SysConsole = System.Console;

namespace PowerConsole.ValidationBehaviour {
    /// <summary>
    /// If a conversion or validation error occurs, the error message is written in the StdErr stream
    /// </summary>
    public class WriteToStdErr : BaseValidationBehavior, IValidationBehavior {

        /// <summary>
        /// Writes the message in <see cref="SysConsole.Error"/> stream
        /// </summary>
        /// <param name="message"></param>
        /// <returns><c>false</c>, as it not needs to reposition the cursor</returns>
        public override bool ShowMessage(string message) {
            SysConsole.Error.WriteLine(message);
            return NeedReposition;
        }
    }
}
