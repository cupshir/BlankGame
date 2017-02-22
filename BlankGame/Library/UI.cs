﻿using System;
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


       
    }    
}
