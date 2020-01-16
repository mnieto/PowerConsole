using System;
using System.Threading.Tasks;
using SysConsole = System.Console;

namespace PowerConsole.ValidationBehaviour {

    /// <summary>
    /// Shows the messages in the status bar (bottom right line of the console)
    /// </summary>
    public class StatusBarBehaviour : BaseValidationBehavior, IValidationBehavior {

        private ConsoleClipboard Clipboard { get; set; }

        /// <summary>
        /// Duration (in ms) the messages are shown in the bottom left corner of the console
        /// </summary>
        /// <remarks>
        /// <para>If this option is set to 0, the message is shown until the user press any key</para>
        /// <para>Try to keep this value low, because the thread is slept until the time is elapsed</para>
        /// </remarks>
        /// <value>1000</value>
        protected int Duration { get; private set; }


        /// <summary>
        /// Constructor. Set the default <see cref="Duration"/> to one second
        /// </summary>
        public StatusBarBehaviour() : this(1000) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="duration">Duration (in ms) the messages are shown./></param>
        /// <seealso cref="Duration"/>
        public StatusBarBehaviour(int duration) : base() {
            Clipboard = new ConsoleClipboard();
            Duration = duration;
            NeedReposition = true;
        }

        /// <summary>
        /// Message management
        /// </summary>
        /// <param name="message">Message to be shown</param>
        /// <returns><c>true</c>: The console needs to reposition the cursor after the message management</returns>
        public override bool ShowMessage(string message) {
            message = message.Substring(0, Math.Min(message.Length, SysConsole.LargestWindowWidth));

            //Save values phase
            int x = SysConsole.CursorLeft;
            int y = SysConsole.CursorTop;
            ConsoleColor foreColor = SysConsole.ForegroundColor;
            ConsoleColor backColor = SysConsole.BackgroundColor;

            short yPos = (short)(SysConsole.WindowTop + SysConsole.WindowHeight - 1);
            Clipboard.Copy(0, yPos, (short)message.Length, 1);
            SysConsole.SetCursorPosition(0, yPos);
            SysConsole.ForegroundColor = Console.Instance.Colors.ErrorColor;
            SysConsole.Write(message);
            SysConsole.SetCursorPosition(x, y);

            //Wait and restore phase
            SysConsole.ForegroundColor = foreColor;
            SysConsole.BackgroundColor = backColor;
            if (Duration == 0) {
                SysConsole.ReadKey();
                Clipboard.Paste();
            } else {
                Task.Factory.StartNew(() => {
                    System.Threading.Thread.Sleep(Duration);
                }).ContinueWith(c => {
                    Clipboard.Paste();
                });
            }
            return NeedReposition;
        }
    }
}
