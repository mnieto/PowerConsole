using System;
using SysConsole = System.Console;

namespace PowerConsole.ValidationBehaviour {

    /// <summary>
    /// Beeps on validation message
    /// </summary>
    public class BeepBehaviour : BaseValidationBehavior, IValidationBehavior {
        /// <summary>
        /// Beeps on validation message
        /// </summary>
        /// <param name="message">The message string is ignored</param>
        /// <returns><c>false</c>: The console doesn't needs to reposition the cursor after the message management</returns>
        public override bool ShowMessage(string message) {
            SysConsole.Beep();
            return NeedReposition;
        }
    }
}
