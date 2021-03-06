﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    [Serializable]
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string litDescription { get; set; }
        public string moveableObject { get; set; }
        public string moveableObjectDescription { get; set; }
        public string moveableObjectAction { get; set; }
        public string movedObjectDescription { get; set; }
        public string toNorth { get; set; }
        public string toSouth { get; set; }
        public string toEast { get; set; }
        public string toWest { get; set; }
        public string toCave { get; set; }
        public string toDown { get; set; }
        public string toUp { get; set; }
        public List<Item> Inventory { get; set; }
        public List<Monster> Monsters { get; set; }
        public List<NPC> NPCs { get; set; }

        // Create a room
        public static Room CreateRoom(string name, string description = "", string litDescription = "", string moveableObject = "", string moveableObjectAction = "",
                                      string moveableObjectDescription = "", string movedObjectDescription = "", string toCave = "", string toEast = "", string toNorth = "",
                                      string toSouth = "", string toWest = "", string toDown = "", string toUp = "", List<string> items = default(List<string>), 
                                      List<string> mobs = default(List<string>), List<string> npcs = default(List<string>))
        {
            Room room = new Room()
            {
                Name = name,
                Description = description,
                litDescription = litDescription,
                moveableObject = moveableObject,
                moveableObjectAction = moveableObjectAction,
                moveableObjectDescription = moveableObjectDescription,
                movedObjectDescription = movedObjectDescription,
                toCave = toCave,
                toEast = toEast,
                toNorth = toNorth,
                toSouth = toSouth,
                toWest = toWest,
                toUp = toUp,
                toDown = toDown,
                Inventory = new List<Item>(),
                Monsters = new List<Monster>(),
                NPCs = new List<NPC>()
            };

            if (items != default(List<string>))
            {
                foreach (string item in items)
                {
                    room.Inventory.Add(Item.AddItemToRoomInventory(item));
                }
            }

            if (mobs != default(List<string>))
            {
                foreach (string mob in mobs)
                {
                    room.Monsters.Add(Monster.AddMonsterToRoom(mob));
                }
            }

            if (npcs != default(List<string>))
            {
                foreach (string npc in npcs)
                {
                    room.NPCs.Add(NPC.AddNPCToRoom(npc));
                }
            }
            
            return room;
        }

        // Create list of all rooms in game
        public static List<Room> CreateRooms()
        {
            List<string> roomItems = new List<string>();
            List<string> mobNames = new List<string>();
            List<string> npcNames = new List<string>();
            List<Room> rooms = new List<Room>();            
            rooms.Add(CreateRoom(name: "Town Square", 
                                 description: "As you look around, you notice that the town square is pretty basic.\n In fact, it is exactly like every other town square in existance.",
                                 toNorth: "Town Hall",
                                 toUp: "The Cloud", 
                                 toWest: "Store", 
                                 toSouth: "Forest"));
            roomItems.Clear();
            roomItems.Add("Torch");
            rooms.Add(CreateRoom(name: "Forest", 
                                 description: "There are trees and more trees. Oh, there is a cave too.",
                                 items: roomItems,
                                 toNorth: "Town Square", 
                                 toCave: "Cave Room 1"));
            rooms.Add(CreateRoom(name: "The Cloud",
                                 description: "This is not a real room just testing new travel system...if you reading this it worked!",
                                 toDown: "Town Square"));
            npcNames.Clear();
            npcNames.Add("Receptionist");
            roomItems.Clear();
            roomItems.Add("Big Bag O'Money");
            rooms.Add(CreateRoom(name: "Town Hall",
                                 description: "The Town Hall is sterile and menacing. There is a receptionist sitting\nat a desk on one side of the room and offices on the other side.",
                                 items: roomItems,
                                 npcs: npcNames,
                                 toWest: "Inn",
                                 toSouth: "Town Square"));
            npcNames.Clear();
            npcNames.Add("Innkeeper");
            rooms.Add(CreateRoom(name: "Inn",
                                 description: "The Inn is cozy and warmly. There are many empty tables scattered about.\nOn one side of the room is a fire place with some comfy looking chairs.\nThe Innkeeper is moving about randomly cleaning the tables.",
                                 npcs: npcNames,
                                 toEast: "Town Hall",
                                 toSouth: "Store"));
            roomItems.Clear();
            roomItems.Add("n00b Sword");
            roomItems.Add("Healing Rock");
            npcNames.Clear();
            npcNames.Add("Shopkeeper");
            rooms.Add(CreateRoom(name: "Store",
                                 description: "You are standing in the town store. The walls are littered with amazing stuff that you cant afford.\n\nThere is a Shopkeeper standing behind a counter.",
                                 npcs: npcNames,
                                 items: roomItems,
                                 toNorth: "Inn",
                                 toEast: "Town Square"));
            mobNames.Clear();
            mobNames.Add("Snake");
            mobNames.Add("Rat");
            mobNames.Add("Spider");
            rooms.Add(CreateRoom(name: "Cave Room 1",
                                 description: "The cave is pitch black making it impossible to see any details...\nif only there was a way to light things up...",
                                 litDescription: "The cave is much larger then you expected. The ceiling is at least 15ft up.",
                                 mobs: mobNames,
                                 toNorth: "Cave Room 2",
                                 toSouth: "Forest"));
            mobNames.Clear();
            mobNames.Add("Spider");
            mobNames.Add("Snake");
            mobNames.Add("Rat");
            rooms.Add(CreateRoom(name: "Cave Room 2",
                                 description: "The cave is pitch black making it impossible to see any details...\nif only there was a way to light things up...",
                                 litDescription: "This room is as big as the last room. Again, you find nothing special",
                                 mobs: mobNames,
                                 toEast: "Cave Room 3",
                                 toSouth: "Cave Room 1"));
            mobNames.Clear();
            mobNames.Add("Spider");
            mobNames.Add("Pig");
            mobNames.Add("Rat");
            rooms.Add(CreateRoom(name: "Cave Room 3",
                                 description: "The cave is pitch black making it impossible to see any details...\nif only there was a way to light things up...",
                                 litDescription: "The ceiling is starting to slope downwards. You notice some bones piled on the floor.",
                                 mobs: mobNames,
                                 toNorth: "Cave Room 4",
                                 toWest: "Cave Room 2"));
            mobNames.Clear();
            mobNames.Add("Imp");
            mobNames.Add("Pig");
            mobNames.Add("Rat");
            rooms.Add(CreateRoom(name: "Cave Room 4",
                                 description: "The cave is pitch black making it impossible to see any details...\nif only there was a way to light things up...",
                                 litDescription: "The room is about the size of a standard Inn room. You notice multiple ways to go.\nThe way to the East is small and looks barely passible.",
                                 mobs: mobNames,
                                 toNorth: "Cave Room 6",
                                 toEast: "Cave Room 5",
                                 toSouth: "Cave Room 3"));
            roomItems.Clear();
            roomItems.Add("Torch");
            roomItems.Add("Sword of Awesomeness");
            mobNames.Clear();
            mobNames.Add("Pig");
            mobNames.Add("Bear");
            mobNames.Add("Imp");
            rooms.Add(CreateRoom(name: "Cave Room 5",
                                 description: "The cave is pitch black making it impossible to see any details...\nif only there was a way to light things up...",
                                 mobs: mobNames,
                                 items: roomItems,
                                 litDescription: "The room is small and cramped. There are no other exits except for the way you came.",
                                 moveableObject: "Rock",
                                 moveableObjectDescription: "There is a large heavy Rock laying on the ground in the middle of the room",
                                 moveableObjectAction: "With great effort you roll the rock over to reveal a hole in the ground.\nInside is the amazing shiny Sword of Awesomeness!",
                                 movedObjectDescription: "With the Rock moved, you find a hole in the ground. Inside is the amazing shiny Sword of Awesomeness!",
                                 toWest: "Cave Room 4"));
            mobNames.Clear();
            mobNames.Add("Bear");
            mobNames.Add("Zombie");
            mobNames.Add("imp");
            rooms.Add(CreateRoom(name: "Cave Room 6",
                                 description: "The cave is pitch black making it impossible to see any details...\nif only there was a way to light things up...",
                                 mobs: mobNames,
                                 litDescription: "Its...a...cave...",
                                 toWest: "Cave Room 7",
                                 toSouth: "Cave Room 4"));
            mobNames.Clear();
            mobNames.Add("Vampire");
            mobNames.Add("Goon");
            mobNames.Add("Zombie");
            rooms.Add(CreateRoom(name: "Cave Room 7",
                                 description: "The cave is pitch black making it impossible to see any details...\nif only there was a way to light things up...",
                                 mobs: mobNames,
                                 litDescription: "Its...a...cave...",
                                 toNorth: "Cave Room 8",
                                 toEast: "Cave Room 6",
                                 toWest: "Cave Room 9"));
            mobNames.Clear();
            mobNames.Add("Vampire");
            mobNames.Add("Zombie");
            mobNames.Add("Goon");
            rooms.Add(CreateRoom(name: "Cave Room 8",
                                 description: "The cave is pitch black making it impossible to see any details...\nif only there was a way to light things up...",
                                 mobs: mobNames,
                                 litDescription: "Another dead end. Nothing stands out as important.",
                                 toSouth: "Cave Room 7"));
            mobNames.Clear();
            mobNames.Add("Goon");
            mobNames.Add("Vampire");
            mobNames.Add("Zombie");
            rooms.Add(CreateRoom(name: "Cave Room 9",
                                 description: "The cave is pitch black making it impossible to see any details...\nif only there was a way to light things up...",
                                 litDescription: "This cave looks like all the others, however the sound of feint snoring causes the hairs\non the back of you neck to rise.\n\nAny sane person would turn around...",
                                 mobs: mobNames,
                                 toEast: "Cave Room 7",
                                 toWest: "Cave Boss Room"));
            mobNames.Clear();
            mobNames.Add("Uber Boss");
            rooms.Add(CreateRoom(name: "Cave Boss Room",
                                 description: "The cave is pitch black making it impossible to see any details...\nif only there was a way to light things up...",
                                 mobs: mobNames,
                                 litDescription: "The room is quite expansive! So big, that you barely notice the giant Dragon sleeping in the middle of the room.",
                                 toEast: "Cave Room 9"));
            

            return rooms;
        }

        // Display current room info
        public static string Look(Room room)
        {
            string content = "";

            content = content + room.Description + "\n";

            if (room.moveableObject != "") { content = content + room.moveableObjectDescription + "\n"; }

            if (room.Inventory.Count() > 0)
            {
                IEnumerable<Item> isTorchHere = room.Inventory.Where(p => p.Name == "Torch");
                content = content + "\n";
                foreach (Item item in room.Inventory)
                {
                    if (isTorchHere.Count() == 1 && room.Name == "Forest")
                    {
                        content = content + "A burning torch is mounted to the side of the cave\n";
                    }
                    else
                    {
                        content = content + item.Name + " is here\n";
                    }
                    
                }
            }
            else
            {
                content = content + "\n\nThere are no items here\n";
            }

            if (room.Monsters.Count > 0)
            {
                content = content + "\n";
                foreach (Monster mob in room.Monsters)
                {
                    content = content + mob.Name + " is here\n";
                }
            }
            else
            {
                content = content + "\n\nThere is nothing to kill here\n";
            }
            content = content + "\n";
            if (room.toNorth != "") { content = content + "To the North is the " + room.toNorth + "\n"; }
            if (room.toSouth != "") { content = content + "To the South is the " + room.toSouth + "\n"; }
            if (room.toEast != "") { content = content + "To the East is the " + room.toEast + "\n"; }
            if (room.toWest != "") { content = content + "To the West is the " + room.toWest + "\n"; }

            return content;
        }

        // Display current Cave room info
        public static string LookCave(Room room, List<Item> inventory)
        {
            string content = "";

            IEnumerable<Item> itemCheck = inventory.Where(p => p.Name == "Torch");

            if (!itemCheck.Any())
            {
                content = content + room.Description + "\n";
            }
            else
            {
                content = content + room.litDescription + "\n";

                if (room.moveableObject != "") { content = content + room.moveableObjectDescription + "\n"; }
                if (room.Monsters.Count > 0)
                {
                    content = content + "\n";
                    foreach (Monster mob in room.Monsters)
                    {
                        content = content + mob.Name + " is here\n";
                    }
                }
                else
                {
                    content = content + "\n\nThere is nothing to kill here\n";
                }
                content = content + "\n";
                if (room.toNorth != "") { content = content + "To the North is the " + room.toNorth + "\n"; }
                if (room.toSouth != "") { content = content + "To the South is the " + room.toSouth + "\n"; }
                if (room.toEast != "") { content = content + "To the East is the " + room.toEast + "\n"; }
                if (room.toWest != "") { content = content + "To the West is the " + room.toWest + "\n"; }
            }
            return content;
        }

        // Move object in room
        public static Tuple<List<Room>, string> MoveObject(List<Room> gameAreas, Room room, string objectToMove)
        {

            string content = "";

            bool checkMoveableObject = room.moveableObject.ToLower() == objectToMove;
            if (!checkMoveableObject)
            {
                content = content + "Nothing to move";
            }
            else
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

                content = content + room.moveableObjectAction;
            }

            return Tuple.Create(gameAreas, content);
        }
    }
}
