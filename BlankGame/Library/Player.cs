using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hitpoints { get; set; }
        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int AttackPower { get; set; }
        public int DefenseRating { get; set; }
        public List<Item> Inventory { get; set; }

        public static Player CreatePlayer()
        {
            Player newPlayer = new Player();
            newPlayer.Level = 1;
            newPlayer.Hitpoints = 100;
            newPlayer.Strength = 10;
            newPlayer.Stamina = 10;
            newPlayer.Intelligence = 10;
            newPlayer.Agility = 10;
            newPlayer.AttackPower = 10;
            newPlayer.DefenseRating = 1;
            newPlayer.Name = GetPlayerName();
            newPlayer.Inventory = Item.CreateInventory();
            
            return newPlayer;
        }

        private static string GetPlayerName()
        {
            string playerName = "";

            do
            {
                Console.Clear();
                Console.SetCursorPosition(0, 5);
                UI.DisplayCenterText("Create a new player");
                Console.WriteLine();
                UI.DisplayCenterText("Pick a name ");

                Console.SetCursorPosition(0, 26);
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
                Console.SetCursorPosition(0, 24);
                for (int i = 0; i < 120; i++)
                {
                    Console.Write("_");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Enter a name: ");

                playerName = Console.ReadLine();
            } while (playerName == "");
            
            return playerName;
        }

        public static void DisplayPlayerStats(Player currentPlayer)
        {
            Console.Clear();
            UI.DisplayCenterText(currentPlayer.Name + " Details");
            UI.DisplayCenterText("---------------------------------------------------");
            Console.WriteLine();
            UI.DisplayCenterText("        Level: " + currentPlayer.Level);
            UI.DisplayCenterText("     Hitpoints: " + currentPlayer.Hitpoints);
            UI.DisplayCenterText("  Attack Power: " + currentPlayer.AttackPower);
            UI.DisplayCenterText("Defense Rating: " + currentPlayer.DefenseRating);
            UI.DisplayCenterText("      Strength: " + currentPlayer.Strength);
            UI.DisplayCenterText("       Stamina: " + currentPlayer.Stamina);
            UI.DisplayCenterText("       Agility: " + currentPlayer.Agility);
            UI.DisplayCenterText("  Intelligence: " + currentPlayer.Intelligence);


        }

    }
}
