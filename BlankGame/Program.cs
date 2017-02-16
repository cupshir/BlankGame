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
                } else
                {
                    currentRoom = MainMenu();
                }
            } while (currentRoom != "Exit");

            Console.ReadLine();

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
            switch (result)
            {
                case "1":
                    return "Town Square";
                case "t":
                case "T":
                    return "Cave Room 5";
                case "X":
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

            // **Dynamic Actions**
            //
            // Pickup Item action
            // If valid item, move item from room inventory to player inventory
            if (result.Contains("Pickup") || result.Contains("pickup"))
            {
                string pickedItem = result.Remove(0, 7);
                Tuple <Room, List<Item>> updatedRoomInventory = ActionsInventory.AddToInventory(room, pickedItem, currentInventory);
                gameAreas.Remove(room);
                gameAreas.Add(updatedRoomInventory.Item1);
                currentInventory = updatedRoomInventory.Item2;

                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }

            // Drop Item action
            // If item is in player inventory, move it to current room inventory
            else if (result.Contains("Drop") || result.Contains("drop"))
            {
                string removeItem = result.Remove(0, 5);
                Tuple <Room, List<Item>> updatedRoomInventory = ActionsInventory.RemoveFromInventory(room, removeItem, currentInventory);
                gameAreas.Remove(room);
                gameAreas.Add(updatedRoomInventory.Item1);
                currentInventory = updatedRoomInventory.Item2;

                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }

            // Look at Item action
            else if (result.Contains("Look at") || result.Contains("look at") || result.Contains("look At") || result.Contains("Look At"))
            {
                string inspectItem = result.Remove(0, 8);
                ActionsInventory.LookAtItem(inspectItem);
                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }

            // Move Item action
            else if (result.Contains("Move") || result.Contains("move"))
            {
                string objectToMove = result.Remove(0, 5);
                gameAreas = Actions.MoveObject(gameAreas, room.Name, objectToMove);
                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }

            // Travel Action
            // placeholder as reminder to redo below travel actions as dynamic action

            // **Static Actions**
            else
            {
                switch (result)
                {
                    // Display Help Information
                    case "help":
                    case "Help":
                        Actions.Help();
                        return Tuple.Create(gameAreas, room.Name, currentInventory);
                    
                    // Display current room information
                    case "look":
                    case "Look":
                        if (room.Name.Contains("Cave"))
                        {
                            Actions.LookCave(room, currentInventory);
                        }
                        else
                        {
                            Actions.Look(room);
                        }
                        return Tuple.Create(gameAreas, room.Name, currentInventory);

                    // Travel North
                    case "go north":
                    case "Go north":
                    case "go North":
                    case "Go North":
                        {
                            string nextRoom = Actions.Travel(room.Name, room.toNorth);
                            return Tuple.Create(gameAreas, nextRoom, currentInventory);
                        }

                    // Travel South
                    case "go south":
                    case "Go south":
                    case "go South":
                    case "Go South":
                        {
                            string nextRoom = Actions.Travel(room.Name, room.toSouth);
                            return Tuple.Create(gameAreas, nextRoom, currentInventory);
                        }

                    // Travel East
                    case "go east":
                    case "Go east":
                    case "go East":
                    case "Go East":
                        {
                            string nextRoom = Actions.Travel(room.Name, room.toEast);
                            return Tuple.Create(gameAreas, nextRoom, currentInventory);
                        }

                    // Travel West
                    case "go west":
                    case "Go west":
                    case "go West":
                    case "Go West":
                        {
                            string nextRoom = Actions.Travel(room.Name, room.toWest);
                            return Tuple.Create(gameAreas, nextRoom, currentInventory);
                        }

                    // Travel to the Cave Dungeon
                    case "enter cave":
                    case "Enter cave":
                    case "enter Cave":
                    case "Enter Cave":
                        {
                            if (room.Name == "Forest")
                            {
                                string nextRoom = Actions.Travel(room.Name, room.toCave);
                                return Tuple.Create(gameAreas, nextRoom, currentInventory);
                            }
                            else
                            {
                                return Tuple.Create(gameAreas, room.Name, currentInventory);
                            }

                        }

                    // Display Player Inventory
                    case "show inventory":
                    case "Show inventory":
                    case "show Inventory":
                    case "Show Inventory":
                        {
                            ActionsInventory.DisplayInventory(currentInventory);
                            return Tuple.Create(gameAreas, room.Name, currentInventory);
                        }

                    // Exit to Main Menu
                    case "Exit":
                    case "exit":
                        return Tuple.Create(gameAreas, "MainMenu", currentInventory);

                    // Clear Screen
                    case "Clear":
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
