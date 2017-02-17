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

        public static Item CreateItem(string name = "", string description = "", int level = 1, int strength = 0, int stamina = 0, int intellegence = 0, int agility = 0)
        {
            Item item = new Item()
            {
                Name = name,
                Description = description,
                Level = level,
                Strength = strength,
                Stamina = stamina,
                Intellegence = intellegence,
                Agility = agility

            };

            return item;
        }

        public static List<Item> CreateInventory()
        {
            List<Item> items = new List<Item>();
            items.Add(CreateItem(name: "Pocket Lint", description: "Fuzzy"));
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
                                 intellegence: 1));
            items.Add(CreateItem(name: "Sword of Awesomeness",
                                 description: "An epic piece of hardware that will smite its foes with ease",
                                 level: 100,
                                 strength: 100,
                                 stamina: 100,
                                 agility: 100,
                                 intellegence: 100));
            
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

    }
}
