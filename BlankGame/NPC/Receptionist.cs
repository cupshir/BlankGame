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
            string content = "Hello, how can I help you?";
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
                    if (itemToGive.Contains("mob shit"))
                    {
                        IEnumerable<Item> offeredItem = player.Inventory.Where(p => p.Name == "mob shit");
                        if (offeredItem.Count() == 1)
                        {
                            content = "Dinner!";
                        }
                    }
                    else
                    {
                        content = "I dont want that...do you not listen?";
                    }


                }

                // Loan Response
                else if (result.Contains("loan"))
                {
                    content = "You want ME to give YOU money?\n\nThats cute.\n\nGo kill 10 monsters and bring me their scat,\nin exchange I will give you some mooneh!";
                }
                else
                {
                    // Static responses
                    switch (result)
                    {
                        case "hi":
                            content = "How can I help you?";
                            break;
                        case "bye":
                            topic = "goodbye";
                            break;
                        case "help":
                            content = "Help? I guess i can LOAN you the information you need";
                            break;
                        default:
                            content = "I dont understand that, u tard";
                            break;
                    }
                }

                
            
            } while (topic != "goodbye");

            // Display Goodbye screen
            content = "Buhbye!";
            Console.Clear();
            UI.DrawTitleBar(receptionist.Name);
            UI.DrawMainArea(content);
            UI.DrawActionBar("Goodbye");
            Thread.Sleep(3000);

            return Tuple.Create(player, receptionist, room, content);
            
        }
    }
}
