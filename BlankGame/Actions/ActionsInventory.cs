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

        public static void LookAtItem(string item)
        {
            List<Item> validItems = Item.ValidItems();

            IEnumerable<Item> checkItem = validItems.Where(p => p.Name == item);
            if (!checkItem.Any())
            {
                Console.WriteLine("That item doesnt exist");
            }
            else
            {
                Console.WriteLine(item + " Details");
                for (int i = 0; i < 50; i++)
                {
                    Console.Write("-");
                }
                Console.WriteLine();
                foreach (var prop in checkItem)
                {
                    Console.WriteLine("       Level: " + prop.Level);
                    Console.WriteLine("    Strength: " + prop.Strength);
                    Console.WriteLine("     Stamina: " + prop.Stamina);
                    Console.WriteLine("     Agility: " + prop.Agility);
                    Console.WriteLine("Intelligence: " + prop.Intellegence);
                }
            }

            Console.WriteLine();
        }

        public static Tuple<Room, List<Item>> AddToInventory(Room currentRoom, string item, List<Item> inventory)
        {

            IEnumerable<Item> itemCheck = currentRoom.Inventory.Where(p => p.Name == item);
                
            if (!itemCheck.Any())
            {
                Console.WriteLine("That item does not exist here!");
            }
            else
            {
                Item newItem = itemCheck.Single();
                inventory.Add(newItem);
                currentRoom.Inventory.Remove(newItem);
                Console.WriteLine();
                Console.WriteLine(item + " has been added to your inventory.");
            }

            return Tuple.Create(currentRoom, inventory);
        }

        public static Tuple<Room, List<Item>> RemoveFromInventory(Room currentRoom, string item, List<Item> inventory)
        {
            IEnumerable<Item> itemCheck = inventory.Where(p => p.Name == item);
            if (!itemCheck.Any())
            {
                Console.WriteLine("That is not currently in your inventory.");
            }
            else
            {
                Item removeItem = itemCheck.Single();
                inventory.Remove(removeItem);
                currentRoom.Inventory.Add(removeItem);
                Console.WriteLine();
                Console.WriteLine(item + " has been removed from your inventory.");
            }
            return Tuple.Create(currentRoom, inventory);
        }
    }
}
