using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    [Serializable]
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
        public List<Item> Inventory { get; set; }
        public string EquippedWeapon { get; set; }
        
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
            newPlayer.Name = GetPlayerName("new");
            newPlayer.Inventory = Item.CreateInventory();
            newPlayer.EquippedWeapon = "Fists";
            
            return newPlayer;
        }

        // Get Players Name
        public static string GetPlayerName(string option)
        {
            string playerName = "";
            string content = "";
            string title = "";
            string prompt = "Name";
            if (option == "load")
            {
                title = "Load a game";
                content = "Enter your name";
            }
            else
            {
                title = "Create a new player";
                content = "Pick a name";
            }

            do
            {
                UI.DrawTitleBar(title);
                UI.DrawMainArea(content);
                UI.DrawActionBar(prompt);

                playerName = Console.ReadLine();
            } while (playerName == "");
            
            return playerName;
        }

        // Display Player stats
        public static string DisplayPlayerStats(Player currentPlayer)
        {
            string content = "";

            content = content + "\n";
            content = content + currentPlayer.Name + " Details\n";
            content = content + "\n";
            content = content + "Level: " + currentPlayer.Level + "\n";
            content = content + "Hitpoints: " + currentPlayer.Hitpoints + "\n";
            content = content + "Strength: " + currentPlayer.Strength + "\n";
            content = content + "Stamina: " + currentPlayer.Stamina + "\n";
            content = content + "Agility: " + currentPlayer.Agility + "\n";
            content = content + "Intelligence: " + currentPlayer.Intelligence + "\n";
            content = content + "Attack Power: " + currentPlayer.AttackPower + "\n";
            content = content + "Equipped Weapon: " + currentPlayer.EquippedWeapon + "\n";
            
            return content;
        }

        public static Tuple<Player, string> EquipWeapon(Player player, string weapon)
        {
            string content = "";

            IEnumerable<Item> itemInInventory = player.Inventory.Where(p => p.Name == weapon);
            if (itemInInventory.Count() == 1)
            {
                if (player.EquippedWeapon != "Fists")
                {
                    Tuple<Player, string> unEquip = UnEquipWeapon(player, player.EquippedWeapon);
                    player = unEquip.Item1;
                    content = unEquip.Item2;
                }

                Item itemToEquip = itemInInventory.Single();
                player.EquippedWeapon = itemToEquip.Name;
                player.Strength = player.Strength + itemToEquip.Strength;
                player.Stamina = player.Stamina + itemToEquip.Stamina;
                player.Intelligence = player.Intelligence + itemToEquip.Intelligence;
                player.Agility = player.Agility + itemToEquip.Agility;
                player.AttackPower = player.AttackPower * itemToEquip.AttackPower;

                content = content + player.Name + " has equipped " + itemToEquip.Name + "!\n";
            }
            else
            {
                content = "You have trouble equipping that...\n";
            }
            return Tuple.Create(player, content);
        }

        public static Tuple<Player, string> UnEquipWeapon(Player player, string weapon)
        {
            string content = "";

            IEnumerable<Item> itemInInventory = player.Inventory.Where(p => p.Name == weapon);
            if (itemInInventory.Count() == 1 && player.EquippedWeapon == weapon)
            {
                Item itemToUnEquip = itemInInventory.Single();
                player.EquippedWeapon = "Fists";
                player.Strength = player.Strength - itemToUnEquip.Strength;
                player.Stamina = player.Stamina - itemToUnEquip.Stamina;
                player.Intelligence = player.Intelligence - itemToUnEquip.Intelligence;
                player.Agility = player.Agility - itemToUnEquip.Agility;
                player.AttackPower = player.AttackPower / itemToUnEquip.AttackPower;

                content = player.Name + " has unequipped " + itemToUnEquip.Name + ".\n\n";
            }
            else
            {
                content = "That is not currently equipped...so you cannot unequip it!\n";
            }
            return Tuple.Create(player, content);
        }

    }
}
