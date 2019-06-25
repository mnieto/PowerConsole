using System;
using SysConsole = System.Console;

namespace PowerConsole.ValidationBehaviour {

    /// <summary>
    /// Shows the validation message in the next immediate line after the current input line
    /// </summary>
    /// <remarks>
    /// This is the default behaviour in the <see cref="ConsoleOptions.ValidationBehaviours"/> list
    /// </remarks>
    public class StandardBehaviour : BaseValidationBehavior, IValidationBehavior {

        /// <summary>
        /// Shows the validation message in the next immediate line after the current input line
        /// </summary>
        /// <param name="message">Message to be shown, logged,...</param>
        /// <returns><c>false</c>: The console doesn't needs to reposition the cursor after the message management</returns>
        public override bool ShowMessage(string message) {
            SysConsole.WriteLine(message);
            return NeedReposition;
        }
    }
}
