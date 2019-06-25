using System;
using SysConsole = System.Console;

namespace PowerConsole.ValidationBehaviour
{

    /// <summary>
    /// Defines how the console shows the error message after an invalid input
    /// </summary>
    public interface IValidationBehavior
    {
        /// <summary>
        /// Invoked when is needed to show a validation message
        /// </summary>
        /// <param name="message">Message to show</param>
        /// <returns><c>true</c>if the console needs to reposition the cursor after the message management</returns>
        /// <remarks>
        /// Implementors can do different actions with the validation messages, like add them to log, show them in the StdErr, show a popup, beep,...
        /// </remarks>
        bool ShowMessage(string message);

        /// <summary>
        /// Get or set the flag to indicate to the <see cref="PowerConsole"/> if it's needed to reposition the cursor after the <see cref="ShowMessage(string)"/> method is completed
        /// </summary>
        bool NeedReposition { get; }

        /// <summary>
        /// <c>True</c> If the implementor needs to prevent the remaining behaviors in the <see cref="ConsoleOptions.ValidationBehaviours"/> list
        /// </summary>
        bool StopChain { get; set; }
    }
}
