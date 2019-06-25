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


            //Simple read without validation
            string name = Console.ReadLine<string>("What's your name? ");

            //Using ValidationAttributre
            var ageValidator = new RangeAttribute(18, int.MaxValue) {
                ErrorMessage = "You must be an adult"
            };
            int age = Console.ReadLine<int>($"What's your age, {name}? ", ageValidator);

            //Using custom validation
            string phone = Console.ReadLine<string>("And your phone? ", "It's not a valid phone number", x => System.Text.RegularExpressions.Regex.IsMatch(x, @"\d{9}"));

            //Using custom validation without message
            bool guessValidResponse = Console.ReadLine<bool>("Are you happy? ", x => x == true);


            System.Console.Write("Press any key to continue");
            System.Console.ReadKey();
        }
    }

}
