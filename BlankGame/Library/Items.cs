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
        public int Intellegence { get; set; }
        public int Agility { get; set; }
        public string StartingLoc { get; set; }

        public static List<Item> CreateInventory()
        {
            List<Item> items = new List<Item>()
            {
                new Item {Name = "Pocket Lint", Description = "Fuzzy", Level = 1, Strength = 0, Stamina = 0, Intellegence = 0, Agility = 0, StartingLoc = "" }
            };
            return items;
        }

        public static List<Item> ValidItems()
        {
            List<Item> items = new List<Item>()
            {
                new Item {Name = "Torch", Description = "A stick thats on fire...that is...bright...hint..hint...hint", Level = 1, Strength = 0, Stamina = 0, Agility = 0, Intellegence = 0, StartingLoc ="Forest" },
                new Item {Name = "Torch2", Description = "A stick thats on fire...that is...bright...hint..hint...hint", Level = 2, Strength = 0, Stamina = 0, Agility = 0, Intellegence = 0, StartingLoc ="Forest" },
                new Item {Name = "Torch3", Description = "A stick thats on fire...that is...bright...hint..hint...hint", Level = 3, Strength = 0, Stamina = 0, Agility = 0, Intellegence = 0, StartingLoc ="Forest" },
                new Item {Name = "n00b Sword", Description = "A shiny basic sword perfect for a n00bie like you", Level = 1, Strength = 1, Stamina = 1, Agility = 1, Intellegence = 1, StartingLoc = "" },
                new Item {Name = "Sword of Awesomeness", Description = "An epic piece of hardware that will smite its foes with ease", Level = 100, Strength = 100, Stamina = 100, Agility = 100, Intellegence = 100, StartingLoc = "Cave Room 5" }

            };
            return items;
        }

        public static Item AddItemToRoomInventory(string item)
        {
            Item addItem = new Item();
            List<Item> roomItems = new List<Item>();
            if (item != "")
            {
                List<Item> validItems = Item.ValidItems();
                IEnumerable<Item> selectedItem = validItems.Where(p => p.Name == item);
                addItem = selectedItem.Single();
            }
            return addItem;
        }

    }
}
