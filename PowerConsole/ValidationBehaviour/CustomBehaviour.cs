using System;

namespace PowerConsole.ValidationBehaviour
{
    /// <summary>
    /// Allows to define an action that will be invoked by <see cref="PowerConsole"/> when an invalid input is detected
    /// </summary>
    public class CustomBehaviour : BaseValidationBehavior, IValidationBehavior {

        /// <summary>
        /// Action to be invoked
        /// </summary>
        protected Action<string> ShowAction;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customAction">Action to be invoked</param>
        /// <param name="needReposition"><c>true</c> if <paramref name="customAction"/> needs to reposition the cursor after invocation. Default is <c>false</c></param>
        public CustomBehaviour(Action<string> customAction, bool needReposition = false) {
            ShowAction = customAction;
            NeedReposition = needReposition;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customAction">Action to be invoked. The message is ignored</param>
        /// <param name="needReposition"><c>true</c> if <paramref name="customAction"/> needs to reposition the cursor after invocation. Default is <c>false</c></param>
        public CustomBehaviour(Action customAction, bool needReposition = false) {
            ShowAction = new Action<string>(x => customAction());
        }

        /// <summary>
        /// Message management
        /// </summary>
        /// <param name="message">Message to be shown, logged,...</param>
        /// <returns><c>true</c>if the console needs to reposition the cursor after the message management</returns>
        public override bool ShowMessage(string message) {
            ShowAction(message);
            return NeedReposition;
        }
    }
}
