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
            string content = "\n\nHello, how can I help you?";
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
                result = result.ToLower();

                // Dynamic responses

                // Play Rock Paper Scissors game with Shopkeeper
                if (result.Contains("play"))
                {
                    IEnumerable<Item> getSword = room.Inventory.Where(p => p.Name == "n00b Sword");
                    if (getSword.Count() == 1)
                    {
                        content = PlayRockPaperScissors(player.Name);
                        Console.Clear();
                        UI.DrawTitleBar(shopkeeper.Name);
                        UI.DrawMainArea(content);
                        UI.DrawActionBar("Results");
                        Thread.Sleep(3000);
                        if (content.Contains("NOOOO"))
                        {
                                Item n00bSword = getSword.Single();
                                Tuple<Room, List<Item>, string> updateRoom = Item.AddToInventory(room, n00bSword, player.Inventory);
                                room = updateRoom.Item1;
                                player.Inventory = updateRoom.Item2;
                                topic = "goodbye";
                        }
                        content = "";
                    }
                    else
                    {
                        content = "\n\nI have nothing more to give you, but we can play for fun!";
                        Console.Clear();
                        UI.DrawTitleBar(shopkeeper.Name);
                        UI.DrawMainArea(content);
                        UI.DrawActionBar(player.Name);
                        Thread.Sleep(2000);
                        content = PlayRockPaperScissors(player.Name);
                        UI.DrawTitleBar(shopkeeper.Name);
                        UI.DrawMainArea(content);
                        UI.DrawActionBar("Results");
                    }
                }

                // Buy an item
                else if (result.Contains("buy"))
                {
                    if (result.Count() > 4)
                    {
                        string itemToBuy = result.Remove(0, 4);
                        itemToBuy = itemToBuy.ToLower();
                        if (itemToBuy.Contains("sword"))
                        {
                            content = "\n\nYou cant afford my uber sword...\n\n...but, since im such a nice Shopkeeper,\nI will give you a sword if you\ncan beat me in a game.";
                        }
                        else if (itemToBuy.Contains("healing rock"))
                        {
                            IEnumerable<Item> checkForMoney = player.Inventory.Where(p => p.Name == "Big Bag O'Money");
                            if (checkForMoney.Count() == 1)
                            {
                                IEnumerable<Item> checkForPotion = room.Inventory.Where(p => p.Name == "Healing Rock");
                                if (checkForPotion.Count() == 1)
                                {
                                    Item potion = checkForPotion.Single();
                                    room.Inventory.Remove(potion);
                                    player.Inventory.Add(potion);
                                    content = "\n\nExcellent, I take your money...you get this stone\nerrm I mean Healing rock";
                                    topic = "goodbye";
                                    Console.Clear();
                                    UI.DrawTitleBar(shopkeeper.Name);
                                    UI.DrawMainArea(content);
                                    UI.DrawActionBar("Talk");
                                    Thread.Sleep(3000);
                                }
                            }
                            else
                            {
                                content = "\n\nIf you had money, you could buy this...";
                            }
                        }
                        else
                        {
                            content = "\n\nYou cant afford that";
                        }
                    }
                    else
                    {
                        content = "\n\nBuy what?";
                    }
                }

                // About side game
                else if (result.Contains("game"))
                {
                    content = "\n\nThe game is Rock, Paper, Scissors!";
                }

                // Static responses
                else
                {
                    switch (result)
                    {
                        case "hi":
                            content = "\n\nHow can I help you?";
                            break;
                        case "bye":
                            topic = "goodbye";
                            break;
                        case "help":
                            content = "\n\nBye to get the conversation started...\n...or was it buy...";
                            break;
                        default:
                            content = "\n\nI dont understand that, u tard";
                            break;
                    }
                }
                

            } while (topic != "goodbye");

            // Display goodbye screen
            content = "\n\nThank you, please come again!";
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
                content = content + "\n\nYou have defeated me, NOOOO\n\nEnjoy this sword desinged for a n00b like you!";
            }
            else if (match == "loss")
            {
                content = content + "\n\nI have defeated you, LOL\n";
            }
            else if (match == "draw")
            {
                content = content + "\n\nWe tie, play again\n";
            }

            return content;
        }
    }
}
