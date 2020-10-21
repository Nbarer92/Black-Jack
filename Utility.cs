using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BlackJack
{

    class Utility
    {
        public static void WriteLineInColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void WriteInColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void Sleep(int miliseconds = 1500)
        {
            Thread.Sleep(miliseconds);
        }

        public static void printDotAnimation(int timer = 10)
        {
            for (var x = 0; x < timer; x++)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Console.WriteLine();
        }

        public static void Line()
        {
            Console.WriteLine("\n--------------------------------------------------");
        }
    }
}
