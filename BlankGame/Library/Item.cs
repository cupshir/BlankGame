using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    [Serializable]
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int Agility { get; set; }
        public int AttackPower { get; set; }
        public bool CanPickup { get; set; }
        public int Quantity { get; set; }

        // Create an Item object
        public static Item CreateItem(string name = "", string description = "", int level = 1, 
                                      int agility = 1, int attackPower = 1, bool canPickup = true,
                                      int quantity = 1)
        {
            Item item = new Item()
            {
                Name = name,
                Description = description,
                Level = level,
                Agility = agility,
                AttackPower = attackPower,
                CanPickup = canPickup,
                Quantity = quantity
            };

            return item;
        }

        // Create an Inventory list
        public static List<Item> CreateInventory()
        {
            List<Item> items = new List<Item>();
            items.Add(CreateItem(name: "Pocket Lint", description: "Its Fuzzy!", agility: 0));
            return items;
        }

        // Create list of all valid items in game
        public static List<Item> ValidItems()
        {
            List<Item> items = new List<Item>();
            items.Add(CreateItem(name: "Torch",
                                 description: "A stick thats on fire...that is...bright...hint..wink..hint..."));
            items.Add(CreateItem(name: "n00b Sword",
                                 description: "A shiny basic sword perfect for a n00bie like you",
                                 level: 1,
                                 agility: 10,
                                 attackPower: 20,
                                 canPickup: false));
            items.Add(CreateItem(name: "Healing Rock",
                                 description: "Restores your health, so dont die. Can be used many times",
                                 canPickup: false));
            items.Add(CreateItem(name: "Big Bag O'Money",
                                 description: "A big fat bag of the good stuff...cash money!",
                                 canPickup: false));
            items.Add(CreateItem(name: "Sword of Awesomeness",
                                 description: "An epic piece of hardware that will smite its foes with ease",
                                 level: 100,
                                 agility: 100,
                                 attackPower: 100,
                                 canPickup: false));
            
            return items;
        }

        // Add item to Room Inventory
        public static Item AddItemToRoomInventory(string item)
        {
            Item addItem = new Item();
            if (item != "")
            {
                List<Item> validItems = Item.ValidItems();
                IEnumerable<Item> selectedItem = validItems.Where(p => p.Name == item);
                if(selectedItem.Count() == 1)
                {
                    addItem = selectedItem.Single();
                }
                else
                {
                    Console.WriteLine("\n\nError adding " + item + ".");
                }
                
            }
            return addItem;
        }

        // Display inventory
        public static string DisplayInventory(List<Item> inventory)
        {
            string content = "";
            content = content + "Current Inventory\n\n";
            if (!inventory.Any())
            {
                content = content + "<--Inventory Empty-->\n";
            }
            else
            {
                foreach (Item item in inventory)
                {
                    content = content + item.Name + " (" + item.Quantity +  ")\n";
                }
            }
            return content;
        }

        // Display Item Stats
        public static string DisplayItemStats(Item item)
        {
            string content = "";
            content = content + item.Name + " Details\n\n";
            content = content + "       Owned: " + item.Quantity + "\n\n";
            content = content + "       Level: " + item.Level + "\n";
            content = content + "Attack Power: " + item.AttackPower + "\n";
            content = content + "     Agility: " + item.Agility + "\n\n\n";
            content = content + item.Description + "\n";

            return content;
        }

        // Add Item to Player Inventory and remove from Room inventory
        public static Tuple<Room, List<Item>, string> AddToInventory(Room currentRoom, Item item, List<Item> inventory)
        {
            inventory.Add(item);
            currentRoom.Inventory.Remove(item);
            string content = item.Name + " has been added to your inventory.";

            return Tuple.Create(currentRoom, inventory, content);
        }

        // Remove Item from player inventory and add to Room inventory
        public static Tuple<Room, List<Item>, string> RemoveFromInventory(Room currentRoom, Item item, List<Item> inventory)
        {
            inventory.Remove(item);
            currentRoom.Inventory.Add(item);
            string content = item.Name + " has been removed from your inventory.";

            return Tuple.Create(currentRoom, inventory, content);
        }
    }
}
