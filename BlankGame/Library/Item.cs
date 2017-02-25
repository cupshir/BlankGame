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

        public static Item CreateItem(string name = "", string description = "", int level = 1, 
                                      int agility = 1, int attackPower = 1, bool canPickup = true)
        {
            Item item = new Item()
            {
                Name = name,
                Description = description,
                Level = level,
                Agility = agility,
                AttackPower = attackPower,
                CanPickup = canPickup
            };

            return item;
        }

        public static List<Item> CreateInventory()
        {
            List<Item> items = new List<Item>();
            items.Add(CreateItem(name: "Pocket Lint", description: "Fuzzy"));
            //items.Add(CreateItem(name: "n00b Sword", description: "A shiny basic sword perfect for a n00bie like you", attackPower: 2));
            return items;
        }

        public static List<Item> ValidItems()
        {
            List<Item> items = new List<Item>();
            items.Add(CreateItem(name: "Torch",
                                 description: "A stick thats on fire...that is...bright...hint..hint...hint"));
            items.Add(CreateItem(name: "n00b Sword",
                                 description: "A shiny basic sword perfect for a n00bie like you",
                                 level: 1,
                                 agility: 10,
                                 attackPower: 20));
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
                    Console.WriteLine("Error adding " + item + ".");
                }
                
            }
            return addItem;
        }

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
                    content = content + item.Name + "\n";
                }
            }
            return content;
        }

        public static string DisplayItemStats(Item item)
        {
            string content = "";
            content = content + item.Name + " Details\n\n";
            content = content + "       Level: " + item.Level + "\n";
            content = content + "Attack Power: " + item.AttackPower + "\n";
            content = content + "     Agility: " + item.Agility + "\n";
            
            return content;
        }

        public static Tuple<Room, List<Item>, string> AddToInventory(Room currentRoom, Item item, List<Item> inventory)
        {
            inventory.Add(item);
            currentRoom.Inventory.Remove(item);
            string content = item.Name + " has been added to your inventory.";
            
            return Tuple.Create(currentRoom, inventory, content);
        }

        public static Tuple<Room, List<Item>, string> RemoveFromInventory(Room currentRoom, Item item, List<Item> inventory)
        {
            inventory.Remove(item);
            currentRoom.Inventory.Add(item);
            string content = item.Name + " has been removed from your inventory.";

            return Tuple.Create(currentRoom, inventory, content);
        }

    }
}
