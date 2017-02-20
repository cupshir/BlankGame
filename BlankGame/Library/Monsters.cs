using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    public class Monster
    {
       
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hitpoints { get; set; }
        public int AttackPower { get; set; }
        public int DefenseRating { get; set; }

        public static Monster CreateMonster(string name = "", int level = 1, int hitpoints = 10, int attackpower = 1, int defenserating = 1)
        {
            Monster mob = new Monster()
            {
                Name = name,
                Level = level,
                Hitpoints = hitpoints,
                AttackPower = attackpower,
                DefenseRating = defenserating
            };

            return mob;
        }

        public static Monster AddMonsterToRoom(string name)
        {
            Monster mob = new Monster();
            if (name != "")
            {
                if (name == "Uber Boss")
                {
                    mob = CreateMonster(name: name, level: 10, hitpoints: 500, attackpower: 10, defenserating: 10);
                }
                else
                {
                    mob = CreateMonster(name);
                }
                
                return mob;
            }
            else
            {
                Console.WriteLine("Error adding " + name + ".");
            }
            return mob;

        }

        public static void DisplayMonsterStats(Monster mob)
        {
            Console.Clear();
            UI.DisplayCenterText(mob.Name + " Stats");
            UI.DisplayCenterText("---------------------------------------------------");
            Console.WriteLine();
            UI.DisplayCenterText("         Level: " + mob.Level);
            UI.DisplayCenterText("     Hitpoints: " + mob.Hitpoints);
            UI.DisplayCenterText("  Attack Power: " + mob.AttackPower);
            UI.DisplayCenterText("Defense Rating: " + mob.DefenseRating);
            Console.WriteLine();
        }
        
    }
}
