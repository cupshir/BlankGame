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
            List<Monster> caveMobs = Monster.CreateMonsters();
            List<Room> blankGameAreas = Room.CreateRooms();
            List<Item> playerInventory = Item.CreateInventory();


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

               public static Tuple<List<Room>, string, List<Item>> PlayGame(List<Room> areas, Room room, List<Item> inventory)
//        public static Tuple<List<Room>, string, List<Item>> PlayGame(List<Room> areas, Room room, List<Item> inventory, out List<Room> outAreas, out Room outRoom, out List<Room> outInventory )
        {
            List<Room> gameAreas = areas;
            List<Item> currentInventory = inventory;
            Console.WriteLine();
            Console.WriteLine("Current Location: " + room.Name);
            Console.WriteLine();
            Console.Write("Action: ");
            string result = Console.ReadLine();
            if (result.Contains("Pickup") || result.Contains("pickup"))
            {
                string pickedItem = result.Remove(0, 7);
                currentInventory = Actions.AddToInventory(room, pickedItem, currentInventory);
                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }
            else if (result.Contains("Drop") || result.Contains("drop"))
            {
                string removeItem = result.Remove(0, 5);
                currentInventory = Actions.RemoveFromInventory(removeItem, currentInventory);
                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }
            else if (result.Contains("Look at") || result.Contains("look at") || result.Contains("look At") || result.Contains("Look At"))
            {
                string inspectItem = result.Remove(0, 8);
                Actions.LookAtItem(inspectItem);
                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }
            else if (result.Contains("Move") || result.Contains("move"))
            {
                string objectToMove = result.Remove(0, 5);
                gameAreas = Actions.MoveObject(gameAreas, room.Name, objectToMove);
                return Tuple.Create(gameAreas, room.Name, currentInventory);
            }
            else
            {
                switch (result)
                {
                    case "help":
                    case "Help":
                        Actions.Help();
                        return Tuple.Create(gameAreas, room.Name, currentInventory);
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
                    case "go north":
                    case "Go north":
                    case "go North":
                    case "Go North":
                        {
                            string nextRoom = Actions.Travel(room, "toNorth");
                            return Tuple.Create(gameAreas, nextRoom, currentInventory);
                        }
                    case "go south":
                    case "Go south":
                    case "go South":
                    case "Go South":
                        {
                            string nextRoom = Actions.Travel(room, "toSouth");
                            return Tuple.Create(gameAreas, nextRoom, currentInventory);
                        }
                    case "go east":
                    case "Go east":
                    case "go East":
                    case "Go East":
                        {
                            string nextRoom = Actions.Travel(room, "toEast");
                            return Tuple.Create(gameAreas, nextRoom, currentInventory);
                        }
                    case "go west":
                    case "Go west":
                    case "go West":
                    case "Go West":
                        {
                            string nextRoom = Actions.Travel(room, "toWest");
                            return Tuple.Create(gameAreas, nextRoom, currentInventory);
                        }
                    case "enter cave":
                    case "Enter cave":
                    case "enter Cave":
                    case "Enter Cave":
                        {
                            if (room.Name == "Forest")
                            {
                                string nextRoom = Actions.Travel(room, "toCave");
                                return Tuple.Create(gameAreas, nextRoom, currentInventory);
                            }
                            else
                            {
                                return Tuple.Create(gameAreas, room.Name, currentInventory);
                            }

                        }
                    case "show inventory":
                    case "Show inventory":
                    case "show Inventory":
                    case "Show Inventory":
                        {
                            Actions.DisplayInventory(currentInventory);
                            return Tuple.Create(gameAreas, room.Name, currentInventory);
                        }
                    case "Exit":
                    case "exit":
                        return Tuple.Create(gameAreas, "MainMenu", currentInventory);
                    case "Clear":
                    case "clear":
                        Console.Clear();
                        return Tuple.Create(gameAreas, room.Name, currentInventory);
                    default:
                        Console.WriteLine("That is not a valid command.");
                        return Tuple.Create(gameAreas, room.Name, currentInventory);
                }
            }
        }

    }



    class Monster
    {
        public string Name { get; set; }
        public bool Alive { get; set; }
        public int Level { get; set; }
        public int Hitpoints { get; set; }
        public int AttackPower { get; set; }
        public int DefenseRating { get; set; }

        public static List<Monster> CreateMonsters()
        {
            List<Monster> mobs = new List<Monster>()
            {
                new Monster {Name = "Snake", Alive = true, Level = 1, Hitpoints = 10, AttackPower = 5, DefenseRating = 1},
                new Monster {Name = "Rat", Alive = true, Level = 1, Hitpoints = 10, AttackPower = 1, DefenseRating = 1},
                new Monster {Name = "Spider", Alive = true, Level = 1, Hitpoints = 10, AttackPower = 5, DefenseRating = 1},
                new Monster {Name = "Turtle", Alive = true, Level = 1, Hitpoints = 50, AttackPower = 1, DefenseRating = 5}
            };
            return mobs;
        }

    }



}
