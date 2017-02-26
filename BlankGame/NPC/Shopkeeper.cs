using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlankGame
{
    public class Shopkeeper
    {
        // Talk to Shopkeeper
        public static Tuple<Player, NPC, Room, string> TalkToShopkeeper(Player player, NPC shopkeeper, Room room)
        {
            string content = "Hello, how can I help you?";
            string topic = "hello";

            do
            {
                // Display chat screen
                Console.Clear();
                UI.DrawTitleBar(shopkeeper.Name);
                UI.DrawMainArea(content);
                UI.DrawActionBar("Talk");

                // Get player input
                string result = Console.ReadLine();

                // Dynamic responses

                // Play Rock Paper Scissors game with Shopkeeper
                if (result.Contains("play"))
                {
                    content = PlayRockPaperScissors(player.Name);
                    Console.Clear();
                    UI.DrawTitleBar(shopkeeper.Name);
                    UI.DrawMainArea(content);
                    UI.DrawActionBar("Results");
                    Thread.Sleep(3000);
                    if (content.Contains("NOOOO")) 
                    {
                        IEnumerable<Item> getSword = room.Inventory.Where(p => p.Name == "n00b Sword");
                        if (getSword.Count() == 1)
                        {
                            Item n00bSword = getSword.Single();
                            Tuple<Room, List<Item>, string> updateRoom = Item.AddToInventory(room, n00bSword, player.Inventory);
                            room = updateRoom.Item1;
                            player.Inventory = updateRoom.Item2;
                            topic = "goodbye";
                        }
                    }
                    content = "";
                }

                // Buy an item
                else if (result.Contains("buy"))
                {
                    if (result.Count() > 4)
                    {
                        string itemToBuy = result.Remove(0, 4);
                        if (itemToBuy.Contains("sword"))
                        {
                            content = "You cant afford my uber sword...\n\n...but, since im such a nice Shopkeeper,\nI will give you a sword if you\ncan beat me in a game.";
                        }
                        else if (itemToBuy.ToLower().Contains("healing potion"))
                        {
                            content = "If you had money, you could buy this...";
                        }
                        else
                        {
                            content = "You cant afford that";
                        }
                    }
                    else
                    {
                        content = "Buy what?";
                    }
                }

                // About side game
                else if (result.Contains("game"))
                {
                    content = "The game is Rock, Paper, Scissors!";
                }

                // Static responses
                else
                {
                    switch (result.ToLower())
                    {
                        case "hi":
                            content = "How can I help you?";
                            break;
                        case "bye":
                            topic = "goodbye";
                            break;
                        case "help":
                            content = "Bye to get the conversation started...\n...or was it buy...";
                            break;
                        default:
                            content = "I dont understand that, u tard";
                            break;
                    }
                }
                

            } while (topic != "goodbye");

            // Display goodbye screen
            content = "Thank you, please come again!";
            Console.Clear();
            UI.DrawTitleBar(shopkeeper.Name);
            UI.DrawMainArea(content);
            UI.DrawActionBar("Goodbye");
            Thread.Sleep(3000);

            return Tuple.Create(player, shopkeeper, room, content);
            

        }

        // Results content for Rock Paper Scissors side game when playing with Shopkeeper
        public static string PlayRockPaperScissors(string player)
        {
            string content = "";

            string match = RockPaperScissors.PlayRockPaperScissors(player, "Shopkeeper");

            if (match == "win")
            {
                content = content + "You have defeated me, NOOOO\n\nEnjoy this sword desinged for a n00b like you!";
            }
            else if (match == "loss")
            {
                content = content + "I have defeated you, LOL\n";
            }
            else if (match == "draw")
            {
                content = content + "We tie, play again\n";
            }

            return content;
        }
    }
}
