using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlankGame
{
    public class Innkeeper
    {
        // Talk to Innkeeper
        public static Tuple<Player, NPC, Room, string> TalkToInnkeeper(Player player, NPC innkeeper, Room room)
        {
            string content = "Welcome, pull up a comfy chair and relax!";
            string topic = "hello";

            do
            {
                // Display Chat screen
                Console.Clear();
                UI.DrawTitleBar(innkeeper.Name);
                UI.DrawMainArea(content);
                UI.DrawActionBar("Talk");

                // Get Player Input
                string result = Console.ReadLine();
                result = result.ToLower();

                switch (result)
                {
                    case "rest":
                        content = "Sit in one of those comfy chairs and\nyou will feel right as rain";
                        break;
                    case "sit":
                        {
                            content = "The chair feels so amazing.\n\nHow amazing?\n\nSo amazing your health is fully restored!";
                            topic = "goodbye";
                            Console.Clear();
                            UI.DrawTitleBar(innkeeper.Name);
                            UI.DrawMainArea(content);
                            UI.DrawActionBar("Resting");
                            player.Hitpoints = player.MaxHitpoints;
                            Thread.Sleep(2000);
                            break;
                        }
                    default:
                        content = "Uh, huh. Im sure thats nice sweety.";
                        break;
                }


            } while (topic != "goodbye");

            // Display Goodbye screen
            content = "Come back soon!";
            Console.Clear();
            UI.DrawTitleBar(innkeeper.Name);
            UI.DrawMainArea(content);
            UI.DrawActionBar("Goodbye");
            Thread.Sleep(3000);

            return Tuple.Create(player, innkeeper, room, content);
        }
    }
}
