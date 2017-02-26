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
            DisplayCenterText("S) Start Game");
            DisplayCenterText("x) Exit");
            Console.WriteLine();
            DisplayCenterText("Tip: When in doubt, Help");
            Console.WriteLine();


            string result = Console.ReadLine();
            switch (result.ToLower())
            {
                case "s":
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

        // Draw Screen
        public static void DrawScreen(Room room, Player player, string content)
        {
            // Clear Screen
            Console.Clear();

            // Display Title Bar
            DrawTitleBar(room.Name);

            // Display Main Section
            if (content == "")
            {
                IEnumerable<Item> itemCheck = player.Inventory.Where(p => p.Name == "Torch");
                
                if (itemCheck.Count() != 0 && room.Name.Contains("Cave"))
                {
                    content = room.litDescription + "\n";
                }
                else
                {
                    content = room.Description + "\n";
                }
            }
            DrawMainArea(content);

            // Display Action Bar
            DrawActionBar(player.Name);
        }

        // Display Title Bar
        public static void DrawTitleBar(string title)
        {
            DisplayCenterText(title);
            DrawLine(120);
        }

        // Display center area containg content
        public static void DrawMainArea(string content)
        {
            Console.WriteLine();
            var result = content.Split(new[] { '\r', '\n' });
            foreach (var item in result)
            {
                UI.DisplayCenterText(item);
            }
        }
        

        // Display action bar at bottom of screen
        public static void DrawActionBar(string prompt)
        {
            Console.SetCursorPosition(0, 25);
            DrawLine(120);
            Console.WriteLine();
            Console.Write(prompt + ": ");
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

        // Display Commands
        public static string Help()
        {
            string content = "";

            content = content + "\n";
            content = content + "Commands\n";
            content = content + "\n";
            content = content + "Look\n";
            content = content + "Look at <item>\n";
            content = content + "Pickup <item>\n";
            content = content + "Drop <item>\n";
            content = content + "Show Inventory\n";
            content = content + "Show Player\n";
            content = content + "Go <direction>\n";
            content = content + "Enter <place>\n";
            content = content + "Clear\n";
            content = content + "Exit\n";

            return content;
        }
    }    
}
