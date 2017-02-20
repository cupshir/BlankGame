using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    class ActionsInventory
    {
        public static void DisplayInventory(List<Item> inventory)
        {
            Console.Clear();
            Console.WriteLine();
            UI.DisplayCenterText("Current Inventory");
            UI.DisplayCenterText("---------------------------------------------------");
            Console.WriteLine();
            if (!inventory.Any())
            {
                UI.DisplayCenterText("<--Inventory Empty-->");
            }
            else
            {
                foreach (Item item in inventory)
                {
                    UI.DisplayCenterText(item.Name);
                }
            }
            Console.WriteLine();
        }

        public static void DisplayItemStats(Item item)
        {
            Console.Clear();
            UI.DisplayCenterText(item.Name + " Details");
            UI.DisplayCenterText("---------------------------------------------------");
            Console.WriteLine();
            UI.DisplayCenterText("       Level: " + item.Level);
            UI.DisplayCenterText("    Strength: " + item.Strength);
            UI.DisplayCenterText("     Stamina: " + item.Stamina);
            UI.DisplayCenterText("     Agility: " + item.Agility);
            UI.DisplayCenterText("Intelligence: " + item.Intelligence);
            Console.WriteLine();
        }

        public static Tuple<Room, List<Item>> AddToInventory(Room currentRoom, Item item, List<Item> inventory)
        {
            inventory.Add(item);
            currentRoom.Inventory.Remove(item);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(item.Name + " has been added to your inventory.");
        
            return Tuple.Create(currentRoom, inventory);
        }

        public static Tuple<Room, List<Item>> RemoveFromInventory(Room currentRoom, Item item, List<Item> inventory)
        {
            inventory.Remove(item);
            currentRoom.Inventory.Add(item);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(item.Name + " has been removed from your inventory.");

            return Tuple.Create(currentRoom, inventory);
        }
    }
}
