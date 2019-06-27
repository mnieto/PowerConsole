using System;
using SysConsole = System.Console;

namespace PowerConsole.ValidationBehaviour {

    /// <summary>
    /// Shows the messages in the status bar (bottom right line of the console)
    /// </summary>
    public class StatusBarBehaviour : BaseValidationBehavior, IValidationBehavior {
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

            //Write phase
            //This may overwrite existing text. It's possible to read previously existing text from console to save it and restore after showing the message
            //https://stackoverflow.com/questions/12355378/read-from-location-on-console-c-sharp
            //https://docs.microsoft.com/en-us/windows/console/reading-and-writing-blocks-of-characters-and-attributes
            SysConsole.SetCursorPosition(0, SysConsole.WindowTop + SysConsole.WindowHeight - 1);
            SysConsole.ForegroundColor = Console.Colors.ErrorColor;
            SysConsole.Write(message);

            //Wait and restore phase
            SysConsole.ForegroundColor = foreColor;
            SysConsole.BackgroundColor = backColor;
            if (Duration == 0) {
                SysConsole.ReadKey();
            } else {
                //It would be nice if we could wait in separate thread. In that case, be awere that the cursos position could be different from the stored in the save phase
                //https://stackoverflow.com/questions/46862475/changing-thread-context-in-c-sharp-console-application
                System.Threading.Thread.Sleep(Duration);
            }
            SysConsole.Write("\r" + new string(' ', message.Length));
            SysConsole.SetCursorPosition(x, y);

            return NeedReposition;
        }
    }
}
