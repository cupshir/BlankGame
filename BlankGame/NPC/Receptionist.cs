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

                // Dynamic responses


                // Static responses
                switch (result.ToLower())
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
