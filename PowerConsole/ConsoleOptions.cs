using PowerConsole.ValidationBehaviour;
using System.Collections.Generic;

namespace PowerConsole
{
    public class ConsoleOptions
    {
        /// <summary>
        /// Throws an <see="Exception" /> if a conversion or validation error occurs. This property has precedence on other error management options
        /// </summary>
        /// <value>false</value>
        public bool ThrowErrorOnInvalidInput {get; set; }

            
        public void AddValidationBehavior(IValidationBehavior behavior) {
            ValidationBehaviours.Add(behavior);
        }

        public List<IValidationBehavior> ValidationBehaviours { get; protected set; } = new List<IValidationBehavior>();

        public ConsoleOptions() {
            AddValidationBehavior(new StandardBehaviour());
        }
    }
}