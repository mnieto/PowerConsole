using System;
using Console = PowerConsole;
using System.ComponentModel.DataAnnotations;

namespace PowerConsole
{
    class Program
    {
        static void Main(string[] args) {
            System.Console.WriteLine("Hello World!");
            Console.Options.BeepOnError = true;
            Console.Options.ShowErrorMessages = true;
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
