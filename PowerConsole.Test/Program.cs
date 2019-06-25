using System.ComponentModel.DataAnnotations;
using PowerConsole.ValidationBehaviour;

namespace PowerConsole
{
    class Program
    {
        static void Main(string[] args) {
            System.Console.WriteLine("Hello World!");

            Console.Configure(cfg => {
                cfg.AddValidationBehavior(new CustomBehaviour(() => System.Console.Beep(), false));
            });

            string name = Console.ReadLine<string>("What's your name? ");
            var ageValidator = new RangeAttribute(18, int.MaxValue) {
                ErrorMessage = "You must be an adult"
            };

            int age = Console.ReadLine<int>($"What's your age, {name}? ", ageValidator);


            System.Console.Write("Press any key to continue");
            System.Console.ReadKey();
        }
    }

}
