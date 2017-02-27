using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlankGame
{
    [Serializable]
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hitpoints { get; set; }
        public int MaxHitpoints { get; set; }
        public int Agility { get; set; }
        public int AttackPower { get; set; }
        public List<Item> Inventory { get; set; }
        public string EquippedWeapon { get; set; }
        public int Experience { get; set; }

        // Create player object
        public static Player CreatePlayer()
        {
            Player newPlayer = new Player();
            newPlayer.Level = 1;
            newPlayer.Hitpoints = 50;
            newPlayer.MaxHitpoints = 50;
            newPlayer.Agility = 1;
            newPlayer.AttackPower = 1;
            newPlayer.Name = GetPlayerName("new");
            newPlayer.Inventory = Item.CreateInventory();
            newPlayer.EquippedWeapon = "Fists";
            newPlayer.Experience = 0;
            
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
            content = content + "Experience: " + currentPlayer.Experience + "\n";
            content = content + "\n";
            content = content + "Hitpoints: " + currentPlayer.Hitpoints + "\n";
            content = content + "Agility: " + currentPlayer.Agility + "\n";
            content = content + "Attack Power: " + currentPlayer.AttackPower + "\n";
            content = content + "\n";
            content = content + "Equipped Weapon: " + currentPlayer.EquippedWeapon + "\n";
            
            return content;
        }

        // Equip Weapon and add item stats to player stats
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

        // UnEquip Weapon and remove item stats from player stats
        public static Tuple<Player, string> UnEquipWeapon(Player player, string weapon)
        {
            string content = "";

            IEnumerable<Item> itemInInventory = player.Inventory.Where(p => p.Name == weapon);
            if (itemInInventory.Count() == 1 && player.EquippedWeapon == weapon)
            {
                Item itemToUnEquip = itemInInventory.Single();
                player.EquippedWeapon = "Fists";
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

        // Increase player XP by mobs worth
        public static Player IncreaseXP(Player player, int xp)
        {
            player.Experience = player.Experience + xp;

            int playerLevel = SetPlayerLevel(player.Experience);
            if (playerLevel > player.Level)
            {
                player.Level = playerLevel;
                LevelUpStats(player);
                PlayerLevelUp();
            }
            
            return player;
        }

        // Set Player level based on player XP
        public static int SetPlayerLevel(int xp)
        {
            int playerLevel = 1;
            if (xp >= 50000)
            {
                playerLevel = 10;
            }
            else if (xp >= 20000)
            {
                playerLevel = 9;
            }
            else if (xp >= 10000)
            {
                playerLevel = 8;
            }
            else if (xp >= 5000)
            {
                playerLevel = 7;
            }
            else if (xp >= 2000)
            {
                playerLevel = 6;
            }
            else if (xp >= 1000)
            {
                playerLevel = 5;
            }
            else if (xp >= 500)
            {
                playerLevel = 4;
            }
            else if (xp >= 200)
            {
                playerLevel = 3;
            }
            else if (xp >= 100)
            {
                playerLevel = 2;
            }
            else
            {
                playerLevel = 1;
            }
            return playerLevel;
        }

        // Display Player Level Up message
        public static void PlayerLevelUp()
        {
            Console.Clear();
            UI.DrawTitleBar("Level Up!!!");
            UI.DrawMainArea("Congrats on not sucking!!!!");
            //UI.DrawActionBar("Cheers");
            Thread.Sleep(3000);
        }

        // Increase player stats for leveling up
        public static Player LevelUpStats(Player player)
        {

            player.MaxHitpoints = (int)Math.Round((player.MaxHitpoints * 1.5), 0);
            player.Agility = (int)Math.Round((player.Agility * 1.25), 0);
            player.AttackPower = (int)Math.Round((player.AttackPower * 1.25), 0);

            player.Hitpoints = player.MaxHitpoints;
            
            return player;
        }
    }
}
