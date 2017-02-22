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

        public static string DisplayPlayerStats(Player currentPlayer)
        {
            string content = "";

            content = content + "\n";
            content = content + currentPlayer.Name + " Details\n";
            content = content + "\n";
            content = content + "Level: " + currentPlayer.Level + "\n";
            content = content + "Hitpoints: " + currentPlayer.Hitpoints + "\n";
            content = content + "Attack Power: " + currentPlayer.AttackPower + "\n";
            content = content + "Defense Rating: " + currentPlayer.DefenseRating + "\n";
            content = content + "Strength: " + currentPlayer.Strength + "\n";
            content = content + "Stamina: " + currentPlayer.Stamina + "\n";
            content = content + "Agility: " + currentPlayer.Agility + "\n";
            content = content + "Intelligence: " + currentPlayer.Intelligence + "\n";

            return content;
        }

    }
}
