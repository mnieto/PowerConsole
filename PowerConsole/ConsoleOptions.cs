using PowerConsole.ValidationBehaviour;
using System.Collections.Generic;
using System.Linq;

namespace PowerConsole
{
    /// <summary>
    /// Options to configure the <see cref="Console"/> console
    /// </summary>
    public class ConsoleOptions
    {
        /// <summary>
        /// Throws an <see cref="System.Exception" /> if a conversion or validation error occurs. This property has precedence on other error management options
        /// </summary>
        /// <value>false</value>
        public bool ThrowErrorOnInvalidInput {get; set; }


        /// <summary>
        /// Add a <see cref="IValidationBehavior"/> to the Console. The default behavior is <see cref="StandardBehaviour"/>. The console can manage a collection of behaviors
        /// </summary>
        /// <param name="behavior"><see cref="IValidationBehavior"/> to add</param>
        /// <remarks>This method is a shortcut for adding from the <see cref="ValidationBehaviours"/> collection</remarks>
        public void AddValidationBehavior(IValidationBehavior behavior) {
            if (!ValidationBehaviours.Any(x => x.GetType() == behavior.GetType()))
                ValidationBehaviours.Add(behavior);
        }

        /// <summary>
        /// List of the <see cref="IValidationBehavior"/> that will be executed after an invalid input
        /// </summary>
        public List<IValidationBehavior> ValidationBehaviours { get; protected set; } = new List<IValidationBehavior>();

        /// <summary>
        /// Ctor
        /// </summary>
        public ConsoleOptions() {
            AddValidationBehavior(new StandardBehaviour());
        }
    }
}