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
        public List<Item> Inventory { get; set; }

        public static List<Room> CreateRooms()
        {
            List<Room> rooms = new List<Room>()
            {
            new Room { Name = "Town Square",
                        Description = "As you look around, you notice that the town square is pretty basic. In fact, it is exactly like every other town square in existance.",
                        toNorth = "Town Hall",
                        toEast = "",
                        toWest = "Store",
                        toSouth = "Forest",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Forest", Description = "There are trees and more trees. Oh, there is a cave too. There is a burning torch near the entrance to the cave",
                        toNorth = "Town Square",
                        toEast = "",
                        toWest = "",
                        toSouth = "",
                        toCave = "Cave Room 1",
                        Inventory = Item.UpdateRoomInventory("Torch")},
            new Room { Name = "Town Hall", Description = "The Town Hall is sterile and menacing. There is a receptionist sititng at a desk on one side of the room and offices on the other side.",
                        toNorth = "",
                        toEast = "",
                        toWest = "Inn",
                        toSouth = "Town Square",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Inn", Description = "The Inn is cozy and warmly. There are many empty tables scattered through the first floor. On one side of the room is a fire place with some comfy looking chairs. The Innkeeper is moving about randomly cleaning the tables.",
                        toNorth = "",
                        toEast = "Town Hall",
                        toWest = "",
                        toSouth = "Store",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Store", Description = "You are standing in the town store. The walls are littered with amazing stuff that you cant afford. There is a Shopkeeper standing behind a counter.",
                        toNorth = "Inn",
                        toEast = "Town Square",
                        toWest = "",
                        toSouth = "",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Cave Room 1", Description = "The cave is pitch black making it impossible to see any details.",
                        litDescription = "The cave is much larger then you expected. The ceiling is at least 15ft up. You dont notice anything out of the ordinary",
                        toNorth = "Cave Room 2",
                        toEast = "",
                        toWest = "",
                        toSouth = "Forest",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Cave Room 2", Description = "The cave is pitch black making it impossible to see any details.",
                        litDescription = "This room is as big as the last room. Again, you find nothing special",
                        toNorth = "",
                        toEast = "Cave Room 3",
                        toWest = "",
                        toSouth = "Cave Room 1",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Cave Room 3", Description = "The cave is pitch black making it impossible to see any details.",
                        litDescription = "The ceiling is starting to slope downwards. You notice some bones piled on the floor.",
                        toNorth = "Cave Room 4",
                        toEast = "",
                        toWest = "Cave Room 2",
                        toSouth = "",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Cave Room 4", Description = "The cave is pitch black making it impossible to see any details.",
                        litDescription = "The room is about the size of a standard Inn room. You notice multiple ways to go. The way to the East is small and looks barely passible.",
                        toNorth = "Cave Room 6",
                        toEast = "Cave Room 5",
                        toWest = "",
                        toSouth = "Cave Room 3",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Cave Room 5", Description = "The cave is pitch black making it impossible to see any details.",
                        litDescription = "The room is small and cramped. There are no other exits except for the way you came.",
                        moveableObject = "Rock",
                        moveableObjectDescription = "There is a medium sized Rock laying on the ground in the middle of the room",
                        moveableObjectAction = "With great effort you roll the rock over to reveal a hole in the ground. Inside is the amazing shiny Sword of Awesomeness!",
                        movedObjectDescription = "There is a hole in the ground. Inside is the amazing shiny Sword of Awesomeness!",
                        toNorth = "",
                        toEast = "",
                        toWest = "Cave Room 4",
                        toSouth = "",
                        Inventory = Item.UpdateRoomInventory("Sword of Awesomeness")},
            new Room { Name = "Cave Room 6", Description = "The cave is pitch black making it impossible to see any details.",
                        litDescription = "Its...a...cave...",
                        toNorth = "",
                        toEast = "",
                        toWest = "Cave Room 7",
                        toSouth = "Cave Room 4",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Cave Room 7", Description = "The cave is pitch black making it impossible to see any details.",
                        litDescription = "Its...a...cave...",
                        toNorth = "Cave Room 8",
                        toEast = "Cave Room 6",
                        toWest = "Cave Room 9",
                        toSouth = "",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Cave Room 8", Description = "The cave is pitch black making it impossible to see any details.",
                        litDescription = "Another dead end. Nothing stands out as important.",
                        toNorth = "",
                        toEast = "",
                        toWest = "",
                        toSouth = "Cave Room 7",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Cave Room 9", Description = "The cave is pitch black making it impossible to see any details.",
                        litDescription = "This cave looks like all the others, however the sound of fient snoring causes the hairs on the back of you neck to rise. Any sane person would turn around...",
                        toNorth = "",
                        toEast = "Cave Room 7",
                        toWest = "Cave Boss Room",
                        toSouth = "",
                        Inventory = Item.UpdateRoomInventory("")},
            new Room { Name = "Cave Boss Room", Description = "The cave is pitch black making it impossible to see any details.",
                        litDescription = "The room is quite expansive! So big, that you barely notice the giant Dragon sleeping in the middle of the room.",
                        toNorth = "",
                        toEast = "Cave Room 9",
                        toWest = "",
                        toSouth = "",
                        Inventory = Item.UpdateRoomInventory("")}
            };
            return rooms;
        }
    }
}
