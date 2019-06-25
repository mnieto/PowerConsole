using System;
using SysConsole = System.Console;

namespace PowerConsole.ValidationBehaviour {
    /// <summary>
    /// If a conversion or validation error occurs, the error message is written in the StdErr stream
    /// </summary>
    public class WriteToStdErr : BaseValidationBehavior, IValidationBehavior {
        public override bool ShowMessage(string message) {
            SysConsole.Error.WriteLine(message);
            return NeedReposition;
        }
    }
}
