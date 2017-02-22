using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    public class Actions
    {
        

        public static string Help()
        {
            string content = "";

            content = content + "\n";
            content = content + "Commands\n";
            content = content + "\n";
            content = content + "Look\n";
            content = content + "Look at <item>\n";
            content = content + "Pickup <item>\n";
            content = content + "Drop <item>\n";
            content = content + "Show Inventory\n";
            content = content + "Show Player\n";
            content = content + "Go <direction>\n";
            content = content + "Enter <place>\n";
            content = content + "Clear\n";
            content = content + "Exit\n";

            return content;
        }

        public static string Look(Room room)
        {
            string content = "";

            content = content + room.Description + "\n";

            if (room.moveableObject != "") { content = content + room.moveableObjectDescription + "\n";  }

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
                content = content + "\nThere is nothing to kill here\n";
            }
            content = content + "\n";
            if (room.toNorth != "") { content = content + "To the North is the " + room.toNorth + "\n"; }
            if (room.toSouth != "") { content = content + "To the South is the " + room.toSouth + "\n"; }
            if (room.toEast != "") { content = content + "To the East is the " + room.toEast + "\n"; }
            if (room.toWest != "") { content = content + "To the West is the " + room.toWest + "\n"; }

            return content;
        }

        public static string LookCave(Room room, List<Item> inventory)
        {
            string content = "";

            IEnumerable<Item> itemCheck = inventory.Where(p => p.Name == "Torch");

            if (!itemCheck.Any())
            {
                content = content + room.Description + "\n";
            } else
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
                    content = content + "\nThere is nothing to kill here\n";
                }
                content = content + "\n";
                if (room.toNorth != "") { content = content + "To the North is the " + room.toNorth + "\n"; }
                if (room.toSouth != "") { content = content + "To the South is the " + room.toSouth + "\n"; }
                if (room.toEast != "") { content = content + "To the East is the " + room.toEast + "\n"; }
                if (room.toWest != "") { content = content + "To the West is the " + room.toWest + "\n"; }
            }
            return content;
        }

        public static Tuple<List<Room>, string> MoveObject(List<Room> gameAreas, Room room, string objectToMove)
        {

            string content = "";

            bool checkMoveableObject = room.moveableObject.ToLower() == objectToMove;
            if (!checkMoveableObject)
            {
                content = content + "Nothing to move";
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

                content = content +  room.moveableObjectAction;
            }
            
            return Tuple.Create(gameAreas, content);
        }

        public static Player RockPaperScissors(Player player)
        {


            return player;
        }
    }
}
