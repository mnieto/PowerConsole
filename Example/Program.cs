using System.ComponentModel.DataAnnotations;
using PowerConsole.Components;
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
            string name = Console.ReadLine<string>("What's your name? ".Blue(), Color.Yellow);

            //Using ValidationAttribute
            var ageValidator = new RangeAttribute(18, int.MaxValue) {
                ErrorMessage = "You must be an adult"
            };
            int age = Console.ReadLine<int>($"What's your age, {name}? ", ageValidator);

            //Using custom validation
            const string pattern = @"\d{9}";
            string phone = Console.ReadLine<string>("And your phone? ", "It's not a valid phone number",
                x => System.Text.RegularExpressions.Regex.IsMatch(x, pattern),
                new Color(null, System.ConsoleColor.DarkBlue));

            //Using regex to highlight matching text
            var tokenizer = new RegexTokenizer(pattern, new Color(System.ConsoleColor.White));
            Console.WriteLine(tokenizer.Parse($"We have updated your {phone} number"));

            //Using custom validation without message
            bool guessValidResponse = Console.ReadLine<bool>("Are you happy? ", x => x == true, Color.Yellow);

            //Parse string
            var parser = new AcceleratorCharTokenizer(Color.Blue);
            Console.WriteLine("This is &your menu:", parser);

            //Create a menu
            var choices = new string[] {
                "Salad",
                "Vegetables",
                "Beans",
                "Omelette"
            };
            var menu = new Menu(choices, new MenuOptions {
                NumerationFormat = "  {0}.- ",
                DefaultItem = "2"
            });
            string choice = menu.Show();
            Console.WriteLine($"Thanks for your choice: {choice}");



            System.Console.Write("Press any key to continue");
            System.Console.ReadKey();
        }
    }

}
