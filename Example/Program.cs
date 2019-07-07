using System.ComponentModel.DataAnnotations;
using PowerConsole.ValidationBehaviour;

namespace PowerConsole
{
    class Program
    {
        static void Main(string[] args) {
            

            Console.Configure(cfg => {
                cfg.AddValidationBehavior(new CustomBehaviour(() => System.Console.Beep(), false));
            });

            Console.WriteLine("Hello World!".White().OnRed());

            //Simple read without validation
            string name = Console.ReadLine<string>("What's your name? ".Blue(), new Color(System.ConsoleColor.Yellow));

            //Using ValidationAttribute
            var ageValidator = new RangeAttribute(18, int.MaxValue) {
                ErrorMessage = "You must be an adult"
            };
            int age = Console.ReadLine<int>($"What's your age, {name}? ", ageValidator);

            //Using custom validation
            string phone = Console.ReadLine<string>("And your phone? ", "It's not a valid phone number",
                x => System.Text.RegularExpressions.Regex.IsMatch(x, @"\d{9}"),
                new Color(null, System.ConsoleColor.DarkBlue));

            //Using custom validation without message
            bool guessValidResponse = Console.ReadLine<bool>("Are you happy? ", x => x == true, new Color(System.ConsoleColor.Yellow));

            //Parse string
            var parser = new AcceleratorCharString(new Color(System.ConsoleColor.Blue));
            Console.WriteLine("Hello &world", parser);


            System.Console.Write("Press any key to continue");
            System.Console.ReadKey();
        }
    }

}
