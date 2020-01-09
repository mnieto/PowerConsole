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

            bool clipboadDemo = new Choice("Show clipboard demo?").Show<bool>();
            if (clipboadDemo) {
                ShowClipboardDemo(console);
            }

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

        private static void ShowClipboardDemo(IConsole console) {
            console.WriteLine("We'll replace line #2 and restore it after 2 seconds");
            console.WriteLine("Then will copy line #2 and paste at last,");
            console.WriteLine("but changing the foreground color of first letter");
            console.WriteLine("Line #1");
            console.WriteLine("Line #2");
            console.WriteLine("Line #3");

            var clip = new ConsoleClipboard();
            string replace = "Replacement text";
            clip.Copy(0, 6, (short)replace.Length, 1);  //y parameter: Take in account the 2 first lines from main and the 3 description lines
                                                        //width param: replacement text is longer that the original text. 
            console.At(0, 6).Write(replace);
            console.At(0, 9);                           //Reposition the cursor for next normal write operation in main

            System.Threading.Thread.Sleep(2000);
            clip.Paste();
            clip.ColorAttributes[0, 0] = 0b1110;        //see https://docs.microsoft.com/en-us/windows/console/char-info-str for available values
            clip.Paste(0, 8);
        }
    }

}
