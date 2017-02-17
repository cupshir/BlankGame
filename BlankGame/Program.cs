using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Game Rooms and player Inventory
            List<Room> blankGameAreas = Room.CreateRooms();
            List<Item> playerInventory = Item.CreateInventory();

            // Games running State?
            string currentRoom = "MainMenu";
            Tuple<List<Room>, string, List<Item>> currentState;
            do
            {
                IEnumerable<Room> rooms = blankGameAreas.Where(p => p.Name == currentRoom);
                
                if (currentRoom != "MainMenu")
                {
                    Room selectedRoom = rooms.Single();
                    currentState = PlayGame(blankGameAreas, selectedRoom, playerInventory);
                    currentRoom = currentState.Item2;
                    if (currentRoom == "")
                    {
                        currentRoom = selectedRoom.Name;
                    }
                }
                else
                {
                    currentRoom = MainMenu();
                }
            } while (currentRoom != "Exit");
        }

        // Game's Main Menu
        private static string MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to Blank Game");
            Console.WriteLine("1) Start Game");
            Console.WriteLine("X) Exit");
            Console.WriteLine("T) Test Room (Cave Room 5)");
            Console.WriteLine("Note: When in doubt, Help");
            string result = Console.ReadLine();
            switch (result.ToLower())
            {
                case "1":
                    return "Town Square";
                case "t":
                    return "Cave Room 5";
                case "x":
                    Console.WriteLine("Thanks for playing!!!");
                    return "Exit";
                default:
                    return "MainMenu";
            }
        }

        // Execute player command
        public static Tuple<List<Room>, string, List<Item>> PlayGame(List<Room> gameAreas, Room room, List<Item> currentInventory)
        {

            // Display current room - Update to its own method in future
            Console.WriteLine();
            Console.WriteLine("Current Location: " + room.Name);
            Console.WriteLine();

            // Get player action and process
            Console.Write("Action: ");
            string result = Console.ReadLine();
            result = result.ToLower();

            // **Dynamic Actions**
            
            // Pickup Item action
            // If valid item, move item from room inventory to player inventory
            if (result.Contains("pickup"))
            {
                string pickedItem = result.Remove(0, 7);
                IEnumerable<Item> checkValidItem = room.Inventory.Where(p => p.Name.ToLower() == pickedItem);
                if (checkValidItem.Count() == 1)
                {
                    Item selectedItem = checkValidItem.Single();
                    if (selectedItem.CanPickup == true)
                    {
                        Tuple<Room, List<Item>> updatedRoomInventory = ActionsInventory.AddToInventory(room, selectedItem, currentInventory);
                        gameAreas.Remove(room);
                        gameAreas.Add(updatedRoomInventory.Item1);
                        currentInventory = updatedRoomInventory.Item2;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("You can not pick that up at this time!");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("That item does not exist here!");
                }
                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }

            // Drop Item action
            // If item is in player inventory, move it to current room inventory
            else if (result.Contains("drop"))
            {
                string removeItem = result.Remove(0, 5);
                IEnumerable<Item> checkValidItem = currentInventory.Where(p => p.Name.ToLower() == removeItem);
                if (checkValidItem.Count() == 1)
                {
                    Item selectedItem = checkValidItem.Single();
                    Tuple<Room, List<Item>> updatedRoomInventory = ActionsInventory.RemoveFromInventory(room, selectedItem, currentInventory);
                    gameAreas.Remove(room);
                    gameAreas.Add(updatedRoomInventory.Item1);
                    currentInventory = updatedRoomInventory.Item2;
                }
                else
                {
                    Console.WriteLine("That is not currently in your inventory.");
                }

                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }

            // Look at Item action
            else if (result.Contains("look at"))
            {
                string inspectItem = result.Remove(0, 8);
                IEnumerable<Item> checkValidItem = currentInventory.Where(p => p.Name.ToLower() == inspectItem);
                if (checkValidItem.Count() == 1)
                {
                    Item selectedItem = checkValidItem.Single();
                    ActionsInventory.DisplayItemStats(selectedItem);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("That item is not in your inventory");
                }
                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }

            // Move Item action
            else if (result.Contains("move"))
            {
                string objectToMove = result.Remove(0, 5);
                gameAreas = Actions.MoveObject(gameAreas, room, objectToMove);
                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }

            // Travel Action
            else if (result.Contains("go") || result.Contains("enter"))
            {
                string travelTo = "";
                if (result.Contains("go"))
                {
                    travelTo = "to" + result.Remove(0, 3);
                }
                else if (result.Contains("enter"))
                {
                    travelTo = "to" + result.Remove(0, 6);
                }

                foreach (var prop in room.GetType().GetProperties())
                {
                    if (prop.ToString().ToLower().Contains(travelTo) && prop.GetValue(room, null).ToString() != "")
                    {
                        return Tuple.Create(gameAreas, prop.GetValue(room, null).ToString(), currentInventory);
                    }                    
                }

                Console.WriteLine("");
                Console.WriteLine("There is nothing in that direction!");
                return Tuple.Create(gameAreas, room.Name, currentInventory);

            }
            
            // **Static Actions**
            else
            {
                switch (result)
                {
                    // Display Help Information
                    case "help":
                        Actions.Help();
                        return Tuple.Create(gameAreas, room.Name, currentInventory);
                    
                    // Display current room information
                    case "look":
                        if (room.Name.Contains("Cave"))
                        {
                            Actions.LookCave(room, currentInventory);
                        }
                        else
                        {
                            Actions.Look(room);
                        }
                        return Tuple.Create(gameAreas, room.Name, currentInventory);

                    // Display Player Inventory
                    case "show inventory":
                        {
                            ActionsInventory.DisplayInventory(currentInventory);
                            return Tuple.Create(gameAreas, room.Name, currentInventory);
                        }

                    // Exit to Main Menu
                    case "exit":
                        return Tuple.Create(gameAreas, "MainMenu", currentInventory);

                    // Clear Screen
                    case "clear":
                        Console.Clear();
                        return Tuple.Create(gameAreas, room.Name, currentInventory);
                    
                    // Display current room if invalid command
                    default:
                        Console.WriteLine("That is not a valid command.");
                        return Tuple.Create(gameAreas, room.Name, currentInventory);
                }
            }
        }

    }

}
