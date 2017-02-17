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
            Console.WriteLine();
            Console.WriteLine("Current Inventory");
            for (int i = 0; i < 50; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("");
            if (!inventory.Any())
            {
                Console.WriteLine("<--Inventory Empty-->");
            }
            else
            {
                foreach (Item item in inventory)
                {
                    Console.WriteLine(item.Name);
                }
            }
            Console.WriteLine();
        }

        public static void DisplayItemStats(Item item)
        {
                Console.WriteLine(item.Name + " Details");
                for (int i = 0; i < 50; i++)
                {
                    Console.Write("-");
                }
                Console.WriteLine();
                Console.WriteLine("       Level: " + item.Level);
                Console.WriteLine("    Strength: " + item.Strength);
                Console.WriteLine("     Stamina: " + item.Stamina);
                Console.WriteLine("     Agility: " + item.Agility);
                Console.WriteLine("Intelligence: " + item.Intellegence);
            
            Console.WriteLine();
        }

        public static Tuple<Room, List<Item>> AddToInventory(Room currentRoom, Item item, List<Item> inventory)
        {
            inventory.Add(item);
            currentRoom.Inventory.Remove(item);
            Console.WriteLine();
            Console.WriteLine(item.Name + " has been added to your inventory.");
        
            return Tuple.Create(currentRoom, inventory);
        }

        public static Tuple<Room, List<Item>> RemoveFromInventory(Room currentRoom, Item item, List<Item> inventory)
        {
            inventory.Remove(item);
            currentRoom.Inventory.Add(item);
            Console.WriteLine();
            Console.WriteLine(item.Name + " has been removed from your inventory.");

            return Tuple.Create(currentRoom, inventory);
        }
    }
}
