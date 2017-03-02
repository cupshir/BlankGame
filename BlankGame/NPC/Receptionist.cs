using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlankGame
{
    class Receptionist
    {
        // Talk to Receptionist
        public static Tuple<Player, NPC, Room, string> TalkToReceptionist(Player player, NPC receptionist, Room room)
        {
            string content = "\n\nHello, how can I help you?";
            string topic = "hello";

            do
            {
                // Display Chat screen
                Console.Clear();
                UI.DrawTitleBar(receptionist.Name);
                UI.DrawMainArea(content);
                UI.DrawActionBar("Talk");

                // Get player input
                string result = Console.ReadLine();
                result = result.ToLower();
                // Dynamic responses

                // Give action
                if (result.Contains("give"))
                {
                    string itemToGive = result.Remove(0, 5);
                    if (itemToGive.Contains("shit"))
                    {
                        IEnumerable<Item> offeredItem = player.Inventory.Where(p => p.Name == "Shit");
                        if (offeredItem.Count() == 1)
                        {
                            Item shit = offeredItem.Single();
                            if (shit.Quantity >= 10)
                            {
                                shit.Quantity = shit.Quantity - 10;
                                player.Inventory.Remove(shit);
                                player.Inventory.Add(shit);

                                IEnumerable<Item> moneyBag = room.Inventory.Where(p => p.Name == "Big Bag O'Money");
                                if (moneyBag.Count() == 1)
                                {
                                    Item bagOMoney = moneyBag.Single();
                                    room.Inventory.Remove(bagOMoney);
                                    player.Inventory.Add(bagOMoney);
                                    content = "\n\nThank you for this smelly shit!\nHere is your Big Bag O'Money";
                                    topic = "goodbye";
                                    Console.Clear();
                                    UI.DrawTitleBar(receptionist.Name);
                                    UI.DrawMainArea(content);
                                    //UI.DrawActionBar("Talk");
                                    Thread.Sleep(3000);
                                }
                                else
                                {
                                    content = "\n\nI have no more money to give...";
                                }
                            }
                            else
                            {
                                content = "\n\nI said 10 pieces of shit...you only have " + shit.Quantity + " pieces of shit in your possesion";
                            }
                            
                        }
                    }
                    else if (itemToGive.Contains("money"))
                    {
                        content = "\n\nYou want me...to give you...\n...this money for free?!?!\n\nYa, no.";
                    }
                    else
                    {
                        content = "\n\nI dont want that...do you not listen?";
                    }


                }

                // Loan Response
                else if (result.Contains("loan"))
                {
                    content = "\n\nYou want ME to give YOU money?\n\nThats cute.\n\nGo kill 10 monsters and bring me their scat,\nin exchange I will give you some mooneh!";
                }
                else
                {
                    // Static responses
                    switch (result)
                    {
                        case "hi":
                            content = "\n\nHow can I help you?";
                            break;
                        case "bye":
                            topic = "goodbye";
                            break;
                        case "help":
                            content = "\n\nHelp? I guess i can LOAN you the information you need";
                            break;
                        default:
                            content = "\n\nI dont understand that, u tard";
                            break;
                    }
                }

                
            
            } while (topic != "goodbye");

            // Display Goodbye screen
            content = "\n\nBuhbye!";
            Console.Clear();
            UI.DrawTitleBar(receptionist.Name);
            UI.DrawMainArea(content);
            //UI.DrawActionBar("Goodbye");
            Thread.Sleep(3000);

            return Tuple.Create(player, receptionist, room, content);
            
        }
    }
}
