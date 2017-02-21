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
            Player currentPlayer = new Player();

            // Games running State?
            string currentRoom = "MainMenu";
            Tuple<List<Room>, string,  Player> currentState;
            do
            {
                IEnumerable<Room> rooms = blankGameAreas.Where(p => p.Name == currentRoom);
                
                if (currentRoom == "CreatePlayer")
                {
                    Console.Clear();
                    currentPlayer = Player.CreatePlayer();

                    Console.Clear();
                    currentRoom = "Cave Room 1";
                }
                else if (currentRoom != "MainMenu") 
                {
                    Room selectedRoom = rooms.Single();
                    currentState = PlayGame(blankGameAreas, selectedRoom, currentPlayer);
                    currentRoom = currentState.Item2;
                    if (currentRoom == "")
                    {
                        currentRoom = selectedRoom.Name;
                    }
                }
                else
                {
                    currentRoom = UI.DisplayMainMenu();
                }

            } while (currentRoom != "Exit");
        }


        // Execute player command
        public static Tuple<List<Room>, string, Player> PlayGame(List<Room> gameAreas, Room room, Player currentPlayer)
        {
            


            // Get player action and process
            UI.DisplayActionBar(room.Name);

            string result = Console.ReadLine();
            result = result.ToLower();
            
            // **Dynamic Actions**

            // Pickup Item action
            // If valid item, move item from room inventory to player inventory
            if (result.Contains("pickup"))
            {
                if (result.Count() > 7)
                {
                    string pickedItem = result.Remove(0, 7);
                    IEnumerable<Item> checkValidItem = room.Inventory.Where(p => p.Name.ToLower() == pickedItem);
                    if (checkValidItem.Count() == 1)
                    {
                        Item selectedItem = checkValidItem.Single();
                        if (selectedItem.CanPickup == true)
                        {
                            Tuple<Room, List<Item>> updatedRoomInventory = Item.AddToInventory(room, selectedItem, currentPlayer.Inventory);
                            gameAreas.Remove(room);
                            gameAreas.Add(updatedRoomInventory.Item1);
                            currentPlayer.Inventory = updatedRoomInventory.Item2;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine("You can not pick that up at this time!");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("That item does not exist here!");
                    }
                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer);
            }

            // Drop Item action
            // If item is in player inventory, move it to current room inventory
            else if (result.Contains("drop"))
            {
                if (result.Count() > 5)
                {
                    string removeItem = result.Remove(0, 5);
                    IEnumerable<Item> checkValidItem = currentPlayer.Inventory.Where(p => p.Name.ToLower() == removeItem);
                    if (checkValidItem.Count() == 1)
                    {
                        Item selectedItem = checkValidItem.Single();
                        Tuple<Room, List<Item>> updatedRoomInventory = Item.RemoveFromInventory(room, selectedItem, currentPlayer.Inventory);
                        gameAreas.Remove(room);
                        gameAreas.Add(updatedRoomInventory.Item1);
                        currentPlayer.Inventory = updatedRoomInventory.Item2;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("That is not currently in your inventory.");
                    }

                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer);
            }

            // Look at Item action
            else if (result.Contains("look at"))
            {
                if (result.Count() > 8)
                {
                    string inspectItem = result.Remove(0, 8);
                
                    IEnumerable<Item> checkValidItem = currentPlayer.Inventory.Where(p => p.Name.ToLower() == inspectItem);
                    if (checkValidItem.Count() == 1)
                    {
                        Item selectedItem = checkValidItem.Single();
                        Item.DisplayItemStats(selectedItem);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("That item is not in your inventory");
                    }
                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer);
            }

            // Move Item action
            else if (result.Contains("move"))
            {
                if (result.Count() > 5)
                {
                    string objectToMove = result.Remove(0, 5);
                    gameAreas = Actions.MoveObject(gameAreas, room, objectToMove);
                    
                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer);
            }

            // Talk action
            else if (result.Contains("talk to"))
            {
                if (result.Count() > 8)
                {
                    string objectToMove = result.Remove(0, 8);
                    

                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer);
            }

            // Display Monster Stats
            else if (result.Contains("scout"))
            {
                if (result.Count() > 6)
                {
                    string scoutMobName = result.Remove(0, 6);
                    IEnumerable<Monster> checkValidMob = room.Monsters.Where(p => p.Name.ToLower() == scoutMobName);
                    if (checkValidMob.Count() == 1)
                    {
                        Monster scoutMob = checkValidMob.Single();
                        Monster.DisplayMonsterStats(scoutMob);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("You do not see a monster with that name around here.");
                    }
                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer);
            }

            // Fight Monster
            else if (result.Contains("fight"))
            {
                if (result.Count() > 6)
                {
                    string fightMobName = result.Remove(0, 6);
                    IEnumerable<Monster> checkValidMob = room.Monsters.Where(p => p.Name.ToLower() == fightMobName);
                    if (checkValidMob.Count() == 1)
                    {
                        Monster fightMob = checkValidMob.Single();
                        Console.Clear();
                        Tuple<Player, Monster> battle = Battle.StartBattle(currentPlayer, fightMob);
                        currentPlayer = battle.Item1;
                        if (battle.Item2.Hitpoints <= 0)
                        {
                            room.Monsters.Remove(fightMob);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("You can not fight that!");
                    }
                }

                return Tuple.Create(gameAreas, room.Name, currentPlayer);
            }

            // Travel Action
            else if (result.Contains("go") || result.Contains("enter"))
            {
                string travelTo = "";
                if (result.Contains("go"))
                {
                    if (result.Count() > 3)
                    {
                        travelTo = "to" + result.Remove(0, 3);
                    }
                }
                else if (result.Contains("enter"))
                {
                    if (result.Count() > 6)
                    { 
                        travelTo = "to" + result.Remove(0, 6);
                    }
                }

                foreach (var prop in room.GetType().GetProperties())
                {
                    if (prop.ToString().ToLower().Contains(travelTo) && prop.GetValue(room, null).ToString() != "")
                    {
                        Console.Clear();
                        return Tuple.Create(gameAreas, prop.GetValue(room, null).ToString(), currentPlayer);
                    }                    
                }
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("There is nothing in that direction!");
                return Tuple.Create(gameAreas, room.Name, currentPlayer);

            }
            
            // **Static Actions**
            else
            {
                switch (result)
                {
                    // Display Help Information
                    case "help":
                        Actions.Help();
                        return Tuple.Create(gameAreas, room.Name, currentPlayer);
                    
                    // Display current room information
                    case "look":
                        if (room.Name.Contains("Cave"))
                        {
                            Actions.LookCave(room, currentPlayer.Inventory);
                        }
                        else
                        {
                            Actions.Look(room);
                        }
                        return Tuple.Create(gameAreas, room.Name, currentPlayer);

                    // Display Player Inventory
                    case "show inventory":
                        {
                            Item.DisplayInventory(currentPlayer.Inventory);
                            return Tuple.Create(gameAreas, room.Name, currentPlayer);
                        }
                    case "show player":
                        {
                            Player.DisplayPlayerStats(currentPlayer);
                            return Tuple.Create(gameAreas, room.Name, currentPlayer);
                        }



                    // Exit to Main Menu
                    case "exit":
                        return Tuple.Create(gameAreas, "MainMenu", currentPlayer);

                    // Clear Screen
                    case "clear":
                        Console.Clear();
                        return Tuple.Create(gameAreas, room.Name, currentPlayer);

                    // Display current room if invalid command
                    case "":
                        Console.Clear();
                        return Tuple.Create(gameAreas, room.Name, currentPlayer);
                    default:
                        Console.Clear();
                        Console.SetCursorPosition(0, 10);
                        UI.DisplayCenterText("That is not a valid command.");
                        return Tuple.Create(gameAreas, room.Name, currentPlayer);
                }
            }
        }

    }

}
