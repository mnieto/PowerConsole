using PowerConsole.ValidationBehaviour;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PowerConsole.Test
{
    public class ConfigurationTest
    {
        [Fact]
        public void duplicate_validation_behaviour_is_ignored() {
            var console = Console.Create(cfg => {
                cfg.AddValidationBehavior(new CustomBehaviour(() => System.Console.Beep(), false));
                cfg.AddValidationBehavior(new StandardBehaviour());
                Assert.Equal(2, cfg.ValidationBehaviours.Count);
            });
        }
    }
}
