using System;

namespace PowerConsole
{
    public class DefaultColors
    {
        public ConsoleColor ErrorColor {get; set; } = ConsoleColor.DarkRed;
        public ConsoleColor WarnColor {get; set; } = ConsoleColor.Yellow;
        public ConsoleColor InfoColor {get; set; } = ConsoleColor.Blue;    

        public ConsoleColor BackColor {get; set; } = System.Console.BackgroundColor;
        public ConsoleColor ForeColor {get; set; } = System.Console.ForegroundColor;
        public ConsoleColor HightLightColor {get; set; } = ConsoleColor.White;
    }
}
