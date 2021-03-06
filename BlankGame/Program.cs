﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
            string content = "";
            Tuple<List<Room>, string,  Player, string> currentState;
            do
            {
                IEnumerable<Room> rooms = blankGameAreas.Where(p => p.Name == currentRoom);
                
                if (currentRoom == "CreatePlayer")
                {
                    Console.Clear();
                    currentPlayer = Player.CreatePlayer();

                    Console.Clear();
                    currentRoom = "Town Square";
                }
                else if (currentRoom != "MainMenu") 
                {
                    Room selectedRoom = rooms.Single();
                    currentState = PlayGame(blankGameAreas, selectedRoom, currentPlayer, content);
                    blankGameAreas = currentState.Item1;
                    currentRoom = currentState.Item2;
                    currentPlayer = currentState.Item3;
                    content = currentState.Item4;
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
        public static Tuple<List<Room>, string, Player, string> PlayGame(List<Room> gameAreas, Room room, Player currentPlayer, string content)
        {

            // Draw the screen
            UI.DrawScreen(room, currentPlayer, content);

            // Clear Content Buffer
            content = "";
            
            // Get Player Input
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
                            if (selectedItem.Name == "Sword of Awesomeness")
                            {
                                room.moveableObjectDescription = "With the Rock moved, you find a hole in the ground.";
                            }

                            Tuple<Room, List<Item>, string> updatedRoomInventory = Item.AddToInventory(room, selectedItem, currentPlayer.Inventory);
                            gameAreas.Remove(room);
                            gameAreas.Add(updatedRoomInventory.Item1);
                            currentPlayer.Inventory = updatedRoomInventory.Item2;
                            content = updatedRoomInventory.Item3;
                        }
                        else
                        {
                            content = "\n\nYou can not pick that up at this time!";
                        }
                    }
                    else
                    {
                        content = "\n\nThat item does not exist here!";
                    }
                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
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
                        Tuple<Room, List<Item>, string> updatedRoomInventory = Item.RemoveFromInventory(room, selectedItem, currentPlayer.Inventory);
                        gameAreas.Remove(room);
                        gameAreas.Add(updatedRoomInventory.Item1);
                        currentPlayer.Inventory = updatedRoomInventory.Item2;
                        content = updatedRoomInventory.Item3;
                    }
                    else
                    {
                        content = "\n\nThat is not currently in your inventory.";
                    }

                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
            }

            // Look at NPC or Item action
            else if (result.Contains("look at"))
            {
                if (result.Count() > 8)
                {
                    // Check if player is looking at NPC and that NPC is in current room
                    if (result.Contains("shopkeeper") || result.Contains("receptionist"))
                    {
                        string inspectNPC = result.Remove(0, 8);
                        if (inspectNPC == "shopkeeper" || inspectNPC == "receptionist")
                        {
                            IEnumerable<NPC> checkValidNPC = room.NPCs.Where(p => p.Name.ToLower() == inspectNPC);
                            if (checkValidNPC.Count() == 1)
                            {
                                NPC npc = checkValidNPC.Single();
                                content = NPC.DisplayNPCStats(npc);
                            }
                            else
                            {
                                content = "\n\nYou can't look at that";
                            }
                        }
                    }
                    else if (result.Contains("myself"))
                    {
                        content = Player.DisplayPlayerStats(currentPlayer);
                        return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
                    }
                    // Check if player is looking at item, ensure item is in player inventory or room inventory before displaying stats
                    else
                    {
                        string inspectItem = result.Remove(0, 8);

                        IEnumerable<Item> checkValidPlayerItem = currentPlayer.Inventory.Where(p => p.Name.ToLower() == inspectItem);
                        IEnumerable<Item> checkValidRoomItem = room.Inventory.Where(p => p.Name.ToLower() == inspectItem);
                        if (checkValidPlayerItem.Count() == 1)
                        {
                            Item selectedItem = checkValidPlayerItem.Single();
                            content = Item.DisplayItemStats(selectedItem);
                        }
                        else if (checkValidRoomItem.Count() == 1)
                        {
                            Item selectedItem = checkValidRoomItem.Single();
                            content = Item.DisplayItemStats(selectedItem);
                        }
                        else
                        {
                            content = "\n\nYou can't look at that";
                        }
                    }
                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
            }

            // UnEquip a Weapon
            else if (result.Contains("unequip"))
            {
                if (result.Count() > 8)
                {
                    string unequipItem = result.Remove(0, 8);

                    IEnumerable<Item> checkValidItem = currentPlayer.Inventory.Where(p => p.Name.ToLower() == unequipItem);
                    if (checkValidItem.Count() == 1)
                    {
                        Item selectedItem = checkValidItem.Single();
                        Tuple<Player, string> updatedPlayer = Player.UnEquipWeapon(currentPlayer, selectedItem.Name);
                        currentPlayer = updatedPlayer.Item1;
                        content = updatedPlayer.Item2;
                    }
                    else
                    {
                        content = "\n\nThat item is not in your inventory";
                    }
                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
            }

            // Equip a Weapon
            else if (result.Contains("equip"))
            {
                if (result.Count() > 6)
                {
                    string equipItem = result.Remove(0, 6);

                    IEnumerable<Item> checkValidItem = currentPlayer.Inventory.Where(p => p.Name.ToLower() == equipItem);
                    if (checkValidItem.Count() == 1)
                    {
                        Item selectedItem = checkValidItem.Single();
                        Tuple<Player, string> updatedPlayer = Player.EquipWeapon(currentPlayer, selectedItem.Name);
                        currentPlayer = updatedPlayer.Item1;
                        content = updatedPlayer.Item2;
                    }
                    else
                    {
                        content = "\n\nThat item is not in your inventory";
                    }

                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
            }

            // Move Item action
            else if (result.Contains("move"))
            {
                if (result.Count() > 5)
                {
                    if (room.Name == "Cave Room 5" && room.Monsters.Count() > 0)
                    {
                        content = "\n\nYou cant get close enough to move that with all those mobs in the way";
                    }
                    else
                    {
                        string objectToMove = result.Remove(0, 5);
                        Tuple<List<Room>, string> move = Room.MoveObject(gameAreas, room, objectToMove);
                        content = move.Item2;
                        gameAreas = move.Item1;
                    }
                    
                    
                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
            }

            // Talk action
            else if (result.Contains("talk to"))
            {
                if (result.Count() > 8)
                {
                    string talkTo = result.Remove(0, 8);
                    IEnumerable<NPC> checkValidNPC = room.NPCs.Where(p => p.Name.ToLower() == talkTo);
                    if (checkValidNPC.Count() == 1)
                    {
                        NPC npc = checkValidNPC.Single();
                        Tuple<Player, Room, string> conversation = NPC.TalkToNPC(currentPlayer, npc, room);
                        currentPlayer = conversation.Item1;
                        room = conversation.Item2;
                        gameAreas.Remove(room);
                        gameAreas.Add(room);

                        IEnumerable<Item> itemCheck = currentPlayer.Inventory.Where(p => p.Name == "Torch");

                        if (itemCheck.Count() != 0 && room.Name.Contains("Cave"))
                        {
                            content = room.litDescription + "\n";
                        }
                        else
                        {
                            content = room.Description + "\n";
                        }
                    }
                    else
                    {
                        content = content + "\n\nThere is no NPC with that name around here.";
                    }



                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
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
                        content = Monster.DisplayMonsterStats(scoutMob);
                    }
                    else
                    {
                        content = "\n\nYou do not see a monster with that name around here.";
                    }
                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
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
                        Tuple<Player, Monster> battle = Battle.ExecuteBattle(currentPlayer, fightMob);
                        currentPlayer = battle.Item1;
                        if (battle.Item2.Hitpoints <= 0)
                        {
                            room.Monsters.Remove(fightMob);
                        }
                    }
                    else
                    {
                        content = "\n\nYou can not fight that!";
                    }
                }

                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
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
                        if (room.Monsters.Count > 0 && room.Name.Contains("Cave"))
                        {
                            if ((room.Name == "Cave Room 1" || room.Name == "Cave Room 2" || room.Name == "Cave Room 4" || room.Name == "Cave Room 6" || room.Name == "Cave Room 8") && travelTo == "tosouth")
                            {
                                return Tuple.Create(gameAreas, prop.GetValue(room, null).ToString(), currentPlayer, content);
                            }
                            else if ((room.Name == "Cave Room 3" || room.Name == "Cave Room 5") && travelTo == "towest")
                            {
                                return Tuple.Create(gameAreas, prop.GetValue(room, null).ToString(), currentPlayer, content);
                            }
                            else if ((room.Name == "Cave Room 7" || room.Name == "Cave Room 9" || room.Name == "Cave Boss Room") && travelTo == "toeast")
                            {
                                return Tuple.Create(gameAreas, prop.GetValue(room, null).ToString(), currentPlayer, content);
                            }
                            else
                            {
                                content = "\n\nYou can not proceed until the mobs blocking your way are dead";
                                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
                            }
                        }

                        return Tuple.Create(gameAreas, prop.GetValue(room, null).ToString(), currentPlayer, content);
                    }                    
                }
                content = "\n\nThere is nothing in that direction!";
                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);

            }

            // Use Item
            else if (result.Contains("use"))
            {
                if (result.Count() > 3)
                {
                    string useItem = result.Remove(0, 4);
                    if(useItem == "healing rock")
                    {
                        currentPlayer.Hitpoints = currentPlayer.MaxHitpoints;
                        content = "\n\nAs you rub the rock you feel your health recover";
                    }
                    else
                    {
                        content = "\n\nYou can not use that";
                    }
                }
                return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
                


            }
            
            // **Static Actions**
            else
            {
                switch (result)
                {
                    // Display Help Information
                    case "help":
                        content = UI.Help();
                        return Tuple.Create(gameAreas, room.Name, currentPlayer, content);

                    // Display current room information
                    case "look":
                        if (room.Name.Contains("Cave"))
                        {
                            content = Room.LookCave(room, currentPlayer.Inventory);
                        }
                        else
                        {
                            content = Room.Look(room);
                        }
                        return Tuple.Create(gameAreas, room.Name, currentPlayer, content);

                    // Display Player Inventory
                    case "show inventory":
                        {
                            content = Item.DisplayInventory(currentPlayer.Inventory);
                            return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
                        }
                    case "show player":
                        {
                            content = Player.DisplayPlayerStats(currentPlayer);
                            return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
                        }

                    // Save Game
                    case "save":
                        {
                            content = GameData.SaveGameToFile(gameAreas, currentPlayer, room.Name);
                            return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
                        }

                    // Load Game
                    case "load":
                        {
                            Tuple<List<Room>, Player, string, string> loadData = GameData.LoadGameFromFile(gameAreas, currentPlayer, room.Name);
                            gameAreas = loadData.Item1;
                            currentPlayer = loadData.Item2;
                            room.Name = loadData.Item3;
                            content = loadData.Item4;
                            
                            return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
                        }

                     // Exit to Main Menu
                    case "exit":
                        return Tuple.Create(gameAreas, "MainMenu", currentPlayer, content);

                    // Clear Screen
                    case "clear":
                        Console.Clear();
                        return Tuple.Create(gameAreas, room.Name, currentPlayer, content);

                    // Display current room if invalid command
                    case "":
                        Console.Clear();
                        return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
                    default:
                        Console.Clear();
                        content = content + "\n\nThat is not a valid command.\n";
                        return Tuple.Create(gameAreas, room.Name, currentPlayer, content);
                }
            }
        }
    }
}
