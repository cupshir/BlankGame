using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
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


        public static Room CreateRoom(string name, string description = "", string litDescription = "", string moveableObject = "", string moveableObjectAction = "",
                                      string moveableObjectDescription = "", string movedObjectDescription = "", string toCave = "", string toEast = "", string toNorth = "",
                                      string toSouth = "", string toWest = "", string toDown = "", string toUp = "", List<string> items = default(List<string>))
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
                Inventory = new List<Item>()
            };

            if (items != default(List<string>))
            {
                foreach (string item in items)
                {
                    room.Inventory.Add(Item.AddItemToRoomInventory(item));
                }
            }
            
            return room;
        }

        public static List<Room> CreateRooms()
        {
            List<string> roomItems = new List<string>();
            List<Room> rooms = new List<Room>();            
            rooms.Add(CreateRoom(name: "Town Square", 
                                 description: "As you look around, you notice that the town square is pretty basic. In fact,\nit is exactly like every other town square in existance.",
                                 toNorth: "Town Hall",
                                 toUp: "The Cloud", 
                                 toWest: "Store", 
                                 toSouth: "Forest"));
            roomItems.Clear();
            roomItems.Add("Torch");
            rooms.Add(CreateRoom(name: "Forest", 
                                 description: "There are trees and more trees. Oh, there is a cave too. There is a\nburning torch near the entrance to the cave",
                                 items: roomItems,
                                 toNorth: "Town Square", 
                                 toCave: "Cave Room 1"));
            rooms.Add(CreateRoom(name: "The Cloud",
                                 description: "This is not a real room just testing new travel system...if you reading this it worked!",
                                 toDown: "Town Square"));
            rooms.Add(CreateRoom(name: "Town Hall",
                                 description: "The Town Hall is sterile and menacing. There is a receptionist sititng\nat a desk on one side of the room and offices on the other side.",
                                 toWest: "Inn",
                                 toSouth: "Town Square"));
            rooms.Add(CreateRoom(name: "Inn",
                                 description: "The Inn is cozy and warmly. There are many empty tables scattered through\nthe first floor. On one side of the room is a fire place with some\ncomfy looking chairs. The Innkeeper is moving about randomly cleaning the tables.",
                                 toEast: "Town Hall",
                                 toSouth: "Store"));
            roomItems.Clear();
            roomItems.Add("n00b Sword");
            rooms.Add(CreateRoom(name: "Store",
                                 description: "You are standing in the town store. The walls are littered with amazing stuff that you cant afford.\nThere is a Shopkeeper standing behind a counter.",
                                 items: roomItems,
                                 toNorth: "Inn",
                                 toEast: "Town Square"));
            rooms.Add(CreateRoom(name: "Cave Room 1",
                                 description: "The cave is pitch black making it impossible to see any details.",
                                 litDescription: "The cave is much larger then you expected. The ceiling is at least 15ft up.\nYou dont notice anything out of the ordinary",
                                 toNorth: "Cave Room 2",
                                 toSouth: "Forest"));
            rooms.Add(CreateRoom(name: "Cave Room 2",
                                 description: "The cave is pitch black making it impossible to see any details.",
                                 litDescription: "This room is as big as the last room. Again, you find nothing special",
                                 toEast: "Cave Room 3",
                                 toSouth: "Cave Room 1"));
            rooms.Add(CreateRoom(name: "Cave Room 3",
                                 description: "The cave is pitch black making it impossible to see any details.",
                                 litDescription: "The ceiling is starting to slope downwards. You notice some bones piled on the floor.",
                                 toNorth: "Cave Room 4",
                                 toWest: "Cave Room 2"));
            rooms.Add(CreateRoom(name: "Cave Room 4",
                                 description: "The cave is pitch black making it impossible to see any details.",
                                 litDescription: "The room is about the size of a standard Inn room. You notice multiple ways to go.\nThe way to the East is small and looks barely passible.",
                                 toNorth: "Cave Room 6",
                                 toEast: "Cave Room 5",
                                 toSouth: "Cave Room 3"));
            roomItems.Clear();
            roomItems.Add("Torch");
            roomItems.Add("Sword of Awesomeness");
            rooms.Add(CreateRoom(name: "Cave Room 5",
                                 description: "The cave is pitch black making it impossible to see any details.",
                                 items: roomItems,
                                 litDescription: "The room is small and cramped. There are no other exits except for the way you came.",
                                 moveableObject: "Rock",
                                 moveableObjectDescription: "There is a medium sized Rock laying on the ground in the middle of the room",
                                 moveableObjectAction: "With great effort you roll the rock over to reveal a hole in the ground.\nInside is the amazing shiny Sword of Awesomeness!",
                                 movedObjectDescription: "There is a hole in the ground. Inside is the amazing shiny Sword of Awesomeness!",
                                 toWest: "Cave Room 4"));
            rooms.Add(CreateRoom(name: "Cave Room 6",
                                 description: "The cave is pitch black making it impossible to see any details.",
                                 litDescription: "Its...a...cave...",
                                 toWest: "Cave Room 7",
                                 toSouth: "Cave Room 4"));
            rooms.Add(CreateRoom(name: "Cave Room 7",
                                 description: "The cave is pitch black making it impossible to see any details.",
                                 litDescription: "Its...a...cave...",
                                 toNorth: "Cave Room 8",
                                 toEast: "Cave Room 6",
                                 toWest: "Cave Room 9"));
            rooms.Add(CreateRoom(name: "Cave Room 8",
                                 description: "The cave is pitch black making it impossible to see any details.",
                                 litDescription: "Another dead end. Nothing stands out as important.",
                                 toSouth: "Cave Room 7"));
            rooms.Add(CreateRoom(name: "Cave Room 9",
                                 description: "The cave is pitch black making it impossible to see any details.",
                                 litDescription: "This cave looks like all the others, however the sound of fient snoring causes the hairs\non the back of you neck to rise.\n\nAny sane person would turn around...",
                                 toEast: "Cave Room 7",
                                 toWest: "Cave Boss Room"));
            rooms.Add(CreateRoom(name: "Cave Boss Room",
                                 description: "The cave is pitch black making it impossible to see any details.",
                                 litDescription: "The room is quite expansive! So big, that you barely notice the giant Dragon sleeping in the middle of the room.",
                                 toEast: "Cave Room 9"));
            return rooms;
        }
    }
}
