using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    class UI
    {
        // Game's Main Menu
        public static string DisplayMainMenu()
        {

            Console.Clear();
            Console.SetCursorPosition(0, 5);

            DisplayCenterText("Welcome to Blank Game");
            DisplayCenterText("1) Start Game");
            DisplayCenterText("x) Exit");
            Console.WriteLine();
            DisplayCenterText("Tip: When in doubt, Help");
            Console.WriteLine();


            string result = Console.ReadLine();
            switch (result.ToLower())
            {
                case "1":
                    Console.Clear();
                    return "CreatePlayer";
                case "x":
                    Console.Clear();
                    return "Exit";
                default:
                    Console.Clear();
                    return "MainMenu";
            }
        }

        // Display action bar at bottom of screen
        public static void DisplayActionBar(string room)
        {
            Console.SetCursorPosition(0, 27);
            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
            Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
            Console.SetCursorPosition(0, 25);
            DrawLine(120);
            Console.WriteLine();
            Console.Write(room + ": ");
            
        }

        // Display Text centered in the Screen
        public static void DisplayCenterText(string text)
        {
            
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);


        }

        // Draw Line
        public static void DrawLine(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("_");
            }
        }


       
    }    
}
