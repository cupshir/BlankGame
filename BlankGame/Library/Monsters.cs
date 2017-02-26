using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    [Serializable]
    public class Monster
    {
       
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hitpoints { get; set; }
        public int Agility { get; set; }
        public int AttackPower { get; set; }
        public int xpWorth { get; set; }

        // Create monster object
        public static Monster CreateMonster(string name = "", int level = 1, int hitpoints = 50, int attackpower = 5, 
                                            int agility = 5, int xpWorth = 100)
        {
            Monster mob = new Monster()
            {
                Name = name,
                Level = level,
                Hitpoints = hitpoints,
                AttackPower = attackpower,
                Agility = agility,
                xpWorth = xpWorth
            };

            return mob;
        }

        // Add monster object to List
        public static Monster AddMonsterToRoom(string name)
        {
            Monster mob = new Monster();
            if (name != "")
            {
                if (name == "Uber Boss")
                {
                    mob = CreateMonster(name: name, level: 10, hitpoints: 1000, attackpower: 100, agility: 100, xpWorth: 1000);
                }
                else if (name == "Zombie" || name =="Vampire" || name == "Goon")
                {
                    mob = CreateMonster(name: name, level: 5, hitpoints: 500, attackpower: 50, agility: 50, xpWorth: 500);
                }
                else if (name == "Bear" || name == "Imp" || name == "Pig")
                {
                    mob = CreateMonster(name: name, level: 2, hitpoints: 100, attackpower: 10, agility: 10, xpWorth: 200);
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

        // Display Monster Stats
        public static string DisplayMonsterStats(Monster mob)
        {
            string content = "";

            content = content + "\n";
            content = content + mob.Name + " Stats\n";
            content = content + "\n";
            content = content + "Level: " + mob.Level + "\n";
            content = content + "Hitpoints: " + mob.Hitpoints + "\n";
            content = content + "Attack Power: " + mob.AttackPower + "\n";
            content = content + "Agility: " + mob.Agility + "\n";
            content = content + "\n";

            return content;
        }
    }
}
