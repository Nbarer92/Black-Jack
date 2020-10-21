using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public static class Screen
    {
        public static void SplashScreen()
        {
            Console.Write("Loading");
            Utility.printDotAnimation(20);
            Console.Clear();
            Console.Title = "Steve C# Console-Based BlackJack Game (Version 2)";
            Console.Write("Steve C# Console-Based BlackJack Game ");
            Utility.WriteInColor(" ♠ ", ConsoleColor.White);
            Utility.WriteInColor(" ♥ ", ConsoleColor.Red);
            Utility.WriteInColor(" ♣ ", ConsoleColor.White);
            Utility.WriteInColor(" ♦ ", ConsoleColor.Red);
        }

        public static void PromptPlayerName()
        {
            Console.Write("\n\nEnter player's name: ");
        }
        public static void PrintShufflingDeck()
        {
            Console.Write("Shuffling cold deck");
            Utility.printDotAnimation();
        }
    }
}
