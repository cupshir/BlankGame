using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public bool CanPickup { get; set; }

        public static Item CreateItem(string name = "", string description = "", int level = 1, int strength = 0, int stamina = 0, int intelligence = 0, int agility = 0, bool canPickup = true)
        {
            Item item = new Item()
            {
                Name = name,
                Description = description,
                Level = level,
                Strength = strength,
                Stamina = stamina,
                Intelligence = intelligence,
                Agility = agility,
                CanPickup = canPickup
            };

            return item;
        }

        public static List<Item> CreateInventory()
        {
            List<Item> items = new List<Item>();
            items.Add(CreateItem(name: "Pocket Lint", description: "Fuzzy"));
            items.Add(CreateItem(name: "Torch"));
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
                                 strength: 1,
                                 stamina: 1,
                                 agility: 1,
                                 intelligence: 1));
            items.Add(CreateItem(name: "Sword of Awesomeness",
                                 description: "An epic piece of hardware that will smite its foes with ease",
                                 level: 100,
                                 strength: 100,
                                 stamina: 100,
                                 agility: 100,
                                 intelligence: 100,
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
