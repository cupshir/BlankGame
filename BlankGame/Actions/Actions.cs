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
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("Look - Looks around the area");
            Console.WriteLine("Look at <item> - Look at an item");
            Console.WriteLine("Pickup <item> - Pickup an item");
            Console.WriteLine("Drop <item> - Remove item from inventory");
            Console.WriteLine("Show Inventory - Display current inventory");
            Console.WriteLine("Show Player - Display player stats");
            Console.WriteLine("Go <direction> - Travel in specified direction");
            Console.WriteLine("Enter <place> - Enter a place");
            Console.WriteLine("Clear - Clear the screen");
            Console.WriteLine("Exit - Exits the game");
            Console.WriteLine();
        }

        public static void Look(Room room)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(room.Description);
            Console.WriteLine();
            if (room.moveableObject != "") { Console.WriteLine(room.moveableObject);  }
            Console.WriteLine();
            if (room.Monsters.Count > 0)
            {
                foreach (Monster mob in room.Monsters)
                {
                    Console.WriteLine(mob.Name + " is here");
                }
            } else
            {
                Console.WriteLine("There is nothing here");
            }
            Console.WriteLine();
            if (room.toNorth != "") { Console.WriteLine("To the North is the " + room.toNorth); }
            if (room.toSouth != "") { Console.WriteLine("To the South is the " + room.toSouth); }
            if (room.toEast != "") { Console.WriteLine("To the East is the " + room.toEast); }
            if (room.toWest != "") { Console.WriteLine("To the West is the " + room.toWest); }
            Console.WriteLine();
        }

        public static void LookCave(Room room, List<Item> inventory)
        {
            Console.Clear();
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
                if (room.Monsters.Count > 0)
                {
                    foreach (Monster mob in room.Monsters)
                    {
                        Console.WriteLine(mob.Name + " is here");
                    }
                }
                else
                {
                    Console.WriteLine("There is nothing here");
                }
                Console.WriteLine();
                if (room.toNorth != "") { Console.WriteLine("To the North is the " + room.toNorth); }
                if (room.toSouth != "") { Console.WriteLine("To the South is the " + room.toSouth); }
                if (room.toEast != "") { Console.WriteLine("To the East is the " + room.toEast); }
                if (room.toWest != "") { Console.WriteLine("To the West is the " + room.toWest); }
            }
            Console.WriteLine();
        }

        public static List<Room> MoveObject(List<Room> gameAreas, Room room, string objectToMove)
        {
            Console.Clear();

            bool checkMoveableObject = room.moveableObject.ToLower() == objectToMove;
            if (!checkMoveableObject)
            {
                Console.WriteLine("Nothing to move");
            } else
            {
                gameAreas.Remove(room);
                room.moveableObjectDescription = room.movedObjectDescription;
                if (room.Name == "Cave Room 5")
                {
                    IEnumerable<Item> getItem = room.Inventory.Where(p => p.Name == "Sword of Awesomeness");
                    
                    Item updateItem = getItem.Single();
                    room.Inventory.Remove(updateItem);
                    updateItem.CanPickup = true;
                    room.Inventory.Add(updateItem);
                }
                gameAreas.Add(room);

                Console.WriteLine(room.moveableObjectAction);
            }
            
            return gameAreas;
        }
    }
}
