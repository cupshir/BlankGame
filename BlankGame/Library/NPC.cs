using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    [Serializable]
    public class NPC
    {

        public string Name { get; set; }
        public string Description { get; set; }

        // Create NPC object
        public static NPC CreateNPC(string name, string description = "")
        {
            NPC npc = new NPC()
            {
                Name = name,
                Description = description
            };

            return npc;
        }

        // Create list of Valid NPC's
        public static List<NPC> ValidNPCs()
        {
            List<NPC> npcs = new List<NPC>();
            npcs.Add(CreateNPC(name: "Shopkeeper",
                                 description: "A person that sells shit"));
            npcs.Add(CreateNPC(name: "Receptionist",
                                 description: "Mason's Mom"));
            npcs.Add(CreateNPC(name: "Innkeeper",
                                description: "Mason's Grandma"));
            return npcs;
        }

        // Add NPC to Room NPC List
        public static NPC AddNPCToRoom(string npc)
        {
            NPC addNPC = new NPC();
            if (npc != "")
            {
                List<NPC> validNPCs = NPC.ValidNPCs();
                IEnumerable<NPC> selectedNPC = validNPCs.Where(p => p.Name == npc);
                if (selectedNPC.Count() == 1)
                {
                    addNPC = selectedNPC.Single();
                }
                else
                {
                    Console.WriteLine("Error adding " + npc + ".");
                }

            }
            return addNPC;
        }

        // Talk to NPC
        public static Tuple<Player, Room, string> TalkToNPC(Player player, NPC npc, Room room)
        {
            string content = "";

            switch (npc.Name)
            {
                case "Shopkeeper":
                    {
                        Tuple<Player, NPC, Room, string> converstaion = Shopkeeper.TalkToShopkeeper(player, npc, room);
                        player = converstaion.Item1;
                        npc = converstaion.Item2;
                        room = converstaion.Item3;
                        content = converstaion.Item4;
                        break;
                    }
                case "Receptionist":
                    {
                        Tuple<Player, NPC, Room, string> converstaion = Receptionist.TalkToReceptionist(player, npc, room);
                        player = converstaion.Item1;
                        npc = converstaion.Item2;
                        room = converstaion.Item3;
                        content = converstaion.Item4;
                        break;
                    }
                case "Innkeeper":
                    {
                        Tuple<Player, NPC, Room, string> converstaion = Innkeeper.TalkToInnkeeper(player, npc, room);
                        player = converstaion.Item1;
                        npc = converstaion.Item2;
                        room = converstaion.Item3;
                        content = converstaion.Item4;
                        break;
                    }
                default:
                    break;
            }

            return Tuple.Create(player, room, content);

        }

        // Display NPC Stats
        public static string DisplayNPCStats(NPC npc)
        {
            string content = "";

            content = content + "\n";
            content = content + npc.Name + " Stats\n";
            content = content + "\n";
            content = content + "Description: " + npc.Description + "\n";
            content = content + "\n";

            return content;
        }
    }
}
