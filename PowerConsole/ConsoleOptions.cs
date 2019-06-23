namespace PowerConsole
{

    public class ConsoleOptions
    {
        public bool BeepOnError {get; set;}
            
        /// <summary>
        /// Throws an <see="Exception" /> if a conversion or validation error occurs. This property has precedence on other error management options
        /// </summary>
        /// <value>false</value>
        public bool ThrowErrorOnInvalidInput {get; set; }


        private bool _showErrorMessage;
        private bool _standardErrorMessage;

        /// <summary>
        /// Show error messages in the bottom left corner of the console
        /// </summary>
        /// <value>false</value>
        /// <remarks>
        /// Setting this property makes that <see cref="StandardErrorMessages"/> is set to <c>false</c>
        /// </remarks>
        public bool ShowErrorMessages {
            get { return _showErrorMessage; }
            set {
                if (value)
                    _standardErrorMessage = false;
                _showErrorMessage = value;
            }
        }

        /// <summary>
        /// Show error messages in the next line as classic terminal
        /// </summary>
        /// <remarks>
        /// Setting this property makes that <see cref="ShowErrorMessages"/> is set to <c>false</c>
        /// </remarks>
        public bool StandardErrorMessages {
            get { return _standardErrorMessage; }
            set {
                if (value)
                    _showErrorMessage = false;
                _standardErrorMessage = value;
            }
        }

        /// <summary>
        /// Duration (in ms) the messages are shown in the bottom left corner of the console
        /// </summary>
        /// <remarks>
        /// <para>This option is ignored unless the <see="ShowErrorMessages" /> is set to <c>true</c></para>
        /// <para>If this option is set to 0, the message is shown until the user press any key</para>
        /// <para>Try to keep this value low, because the thread is slept until the time is elapsed</para>
        /// </remarks>
        /// <value>1000</value>
        public int StatusMessagesDuration {get; set;}  = 1000;
            
        /// <summary>
        /// If a conversion or validation error occurs, the error message is written in the StdErr stream
        /// </summary>
        /// <value>false</value>
        public bool WriteErrorsToStdErr {get; set; }    
    }
}