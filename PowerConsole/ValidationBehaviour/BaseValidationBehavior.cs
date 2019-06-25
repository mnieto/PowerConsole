using System;

namespace PowerConsole.ValidationBehaviour {

    /// <summary>
    /// Base implementation of <see cref="IValidationBehavior"/>
    /// </summary>
    public abstract class BaseValidationBehavior : IValidationBehavior {

        /// <summary>
        /// Message management
        /// </summary>
        /// <param name="message">Message to be shown, logged,...</param>
        /// <returns><c>true</c>if the console needs to reposition the cursor after the message management</returns>
        public abstract bool ShowMessage(string message);

        /// <summary>
        /// Get or set the flag to indicate to the <see cref="PowerConsole"/> if it's needed to reposition the cursor after the <see cref="ShowMessage(string)"/> method is completed. Default is <c>False</c>
        /// </summary>
        public bool NeedReposition { get; protected set; } = false;

        /// <summary>
        /// <c>True</c> If the implementor needs to prevent the remaining behaviors in the <see cref="ConsoleOptions.ValidationBehaviours"/> list. Default is <c>false</c>
        /// </summary>
        public bool StopChain { get; set; } = false;

    }
}
