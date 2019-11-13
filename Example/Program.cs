using System.ComponentModel.DataAnnotations;
using PowerConsole.Components;
using PowerConsole.ValidationBehaviour;

namespace PowerConsole
{
    class Program
    {
        static void Main(string[] args) {

            var console = Console.Create(cfg => {
                cfg.AddValidationBehavior(new CustomBehaviour(() => System.Console.Beep(), false));
            });

            console.WriteLine("Hello World!".White().OnRed());

            //Simple read without validation
            string name = console.Write("What's your name? ".Blue()).ReadLine<string>(Color.Yellow);

            //Using ValidationAttribute
            var ageValidator = new RangeAttribute(18, int.MaxValue) {
                ErrorMessage = "You must be an adult"
            };
            int age = console.Ask($"What's your age, {name}? ").ReadLine<int>(ageValidator);

            //Using custom validation
            const string pattern = @"\d{9}";
            string phone = console
                .Ask()
                .Write("And your phone? ")
                .ReadLine<string>("It's not a valid phone number",
                            x => System.Text.RegularExpressions.Regex.IsMatch(x, pattern),
                            new Color(null, System.ConsoleColor.DarkRed));

            //Using regex to highlight matching text
            var tokenizer = new RegexTokenizer(pattern, new Color(System.ConsoleColor.White));
            console.WriteLine(tokenizer.Parse($"We have updated your {phone} number"));

            //Choices
            var choiceSelection = new Choice("Are you happy?").Show();
            console.WriteLine($"Your choice: {choiceSelection}");

            //Parse string
            var parser = new AcceleratorCharTokenizer(Color.Blue);
            console.WriteLine("This is &your menu:", parser);

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
            string menuSelection = menu.Show();
            console.WriteLine($"Thanks for your choice: {menuSelection}");

            //Password reading
            console.WriteLine("Enter your password");
            string pass = console.ReadPassword(true);
            console.WriteLine($"This is your password {pass}");


            System.Console.Write("Press any key to continue");
            System.Console.ReadKey();
        }
    }

}
