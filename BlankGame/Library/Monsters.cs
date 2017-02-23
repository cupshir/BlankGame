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
        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int AttackPower { get; set; }

        public static Monster CreateMonster(string name = "", int level = 1, int hitpoints = 50, int attackpower = 5, int strength = 5, int stamina = 5,
                                            int intelligence = 5, int agility = 5)
        {
            Monster mob = new Monster()
            {
                Name = name,
                Level = level,
                Hitpoints = hitpoints,
                AttackPower = attackpower,
                Strength = strength,
                Stamina = stamina,
                Intelligence = intelligence,
                Agility = agility
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
                    mob = CreateMonster(name: name, level: 10, hitpoints: 500, attackpower: 10, strength: 10, stamina: 10, intelligence: 10, agility: 10);
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

        public static string DisplayMonsterStats(Monster mob)
        {
            string content = "";

            content = content + "\n";
            content = content + mob.Name + " Stats\n";
            content = content + "\n";
            content = content + "Level: " + mob.Level + "\n";
            content = content + "Hitpoints: " + mob.Hitpoints + "\n";
            content = content + "Attack Power: " + mob.AttackPower + "\n";
            content = content + "Strength: " + mob.Strength + "\n";
            content = content + "Stamina: " + mob.Stamina + "\n";
            content = content + "Intelligence: " + mob.Intelligence + "\n";
            content = content + "Agility: " + mob.Agility + "\n";
            content = content + "\n";

            return content;
        }
        
    }
}
