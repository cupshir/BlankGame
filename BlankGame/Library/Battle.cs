using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    public class Battle
    {
        public static Tuple<Player, Monster> StartBattle(Player player, Monster mob)
        {

            Console.Clear();
            Console.WriteLine("{0} ({1}) vs {2} ({3})", player.Name, player.Hitpoints, mob.Name, mob.Hitpoints);
            Console.WriteLine();




            Console.ReadLine();

            return Tuple.Create(player, mob);
        } 


    }
}
