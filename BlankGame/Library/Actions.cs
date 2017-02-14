using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    public class Actions
    {
        

        public static void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("Look - Looks around the area");
            Console.WriteLine("Look at <item> - Look at an item");
            Console.WriteLine("Pickup <item> - Pickup an item");
            Console.WriteLine("Drop <item> - Remove item from inventory");
            Console.WriteLine("Show Inventory - Display current inventory");
            Console.WriteLine("Go <direction> - Travel in specified direction");
            Console.WriteLine("Enter <place> - Enter a place");
            Console.WriteLine("Clear - Clear the screen");
            Console.WriteLine("Exit - Exits the game");
            Console.WriteLine();
        }

        public static void Look(Room room)
        {
            Console.WriteLine();
            Console.WriteLine(room.Description);
            if (room.moveableObject != "") { Console.WriteLine(room.moveableObject);  }
            Console.WriteLine();
            if (room.toNorth != "") { Console.WriteLine("To the North is the " + room.toNorth); }
            if (room.toSouth != "") { Console.WriteLine("To the South is the " + room.toSouth); }
            if (room.toEast != "") { Console.WriteLine("To the East is the " + room.toEast); }
            if (room.toWest != "") { Console.WriteLine("To the West is the " + room.toWest); }
            Console.WriteLine();
        }

        public static void LookCave(Room room, List<Item> inventory)
        {
            IEnumerable<Item> itemCheck = inventory.Where(p => p.Name == "Torch");

            Console.WriteLine();
            if (!itemCheck.Any())
            {
                Console.WriteLine(room.Description);
                Console.WriteLine("If only you had something to light up the way");
            } else
            {
                Console.WriteLine(room.litDescription);
                if (room.moveableObject != "") { Console.WriteLine(room.moveableObjectDescription); }
                Console.WriteLine();
                if (room.toNorth != "") { Console.WriteLine("To the North is the " + room.toNorth); }
                if (room.toSouth != "") { Console.WriteLine("To the South is the " + room.toSouth); }
                if (room.toEast != "") { Console.WriteLine("To the East is the " + room.toEast); }
                if (room.toWest != "") { Console.WriteLine("To the West is the " + room.toWest); }
            }
            Console.WriteLine();
        }

        public static string Travel(Room room, string direction)
        {
            switch (direction)
            {
                case "toNorth":
                    if (room.toNorth != "")
                    {
                        return room.toNorth;
                    }
                    else
                    {
                        CantTravel();
                        return room.Name;
                    }
                case "toSouth":
                    if (room.toSouth != "")
                    {
                        return room.toSouth;
                    }
                    else
                    {
                        CantTravel();
                        return room.Name;
                    }
                case "toEast":
                    if (room.toEast != "")
                    {
                        return room.toEast;
                    }
                    else
                    {
                        CantTravel();
                        return room.Name;
                    }
                case "toWest":
                    if (room.toWest != "")
                    {
                        return room.toWest;
                    }
                    else
                    {
                        CantTravel();
                        return room.Name;
                    }
                case "toCave":
                    if (room.toCave != "")
                    {
                        return room.toCave;
                    }
                    else
                    {
                        CantTravel();
                        return room.Name;
                    }
                default:
                    return room.Name;
            }
        }

        private static void CantTravel()
        {
            Console.WriteLine();
            Console.WriteLine("There is nothing that way!");
            Console.WriteLine();
        }

        public static List<Room> MoveObject(List<Room> gameAreas, string room, string objectToMove)
        {
            IEnumerable<Room> currentRoom = gameAreas.Where(p => p.Name == room);
            IEnumerable<Room> checkMoveableObject = currentRoom.Where(p => p.moveableObject == objectToMove);
            if (!checkMoveableObject.Any())
            {
                Console.WriteLine("Nothing to move");
            } else
            {
                Room updateRoom = currentRoom.Single();
                gameAreas.Remove(updateRoom);
                updateRoom.moveableObjectDescription = updateRoom.movedObjectDescription;
                if (updateRoom.Name == "Cave Room 5") { updateRoom.canPickItem = true; }

                gameAreas.Add(updateRoom);

                Console.WriteLine(updateRoom.moveableObjectAction);
            }
            
            return gameAreas;
        }

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
            } else
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

        public static List<Item> AddToInventory(Room currentRoom, string item, List<Item> inventory)
        {
            List<Item> validItems = Item.ValidItems();

            IEnumerable<Item> itemCheck = validItems.Where(p => p.Name == item);
            if (!itemCheck.Any())
            {
                Console.WriteLine("You cannot add that to your inventory!");
            } else
            {
                Item newItem = itemCheck.Single();
                if ((newItem.StartingLoc == currentRoom.Name) && (currentRoom.canPickItem == true))
                {
                    inventory.Add(newItem);
                    Console.WriteLine();
                    Console.WriteLine(item + " has been added to your inventory.");
                }
                else
                {
                    Console.WriteLine("There is no item like that around here");
                }
            }

            return inventory;
        }

        public static List<Item> RemoveFromInventory(string item, List<Item> inventory)
        {
            IEnumerable<Item> itemCheck = inventory.Where(p => p.Name == item);
            if (!itemCheck.Any())
            {
                Console.WriteLine("That is not currently in your inventory.");
            } else
            {
                Item removeItem = itemCheck.Single();
                inventory.Remove(removeItem);
                Console.WriteLine();
                Console.WriteLine(item + " has been removed from your inventory.");
            }
            return inventory;
        }
    }
}
