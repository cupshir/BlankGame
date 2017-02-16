using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    class Monsters
    {
       
        public string Name { get; set; }
        public bool Alive { get; set; }
        public int Level { get; set; }
        public int Hitpoints { get; set; }
        public int AttackPower { get; set; }
        public int DefenseRating { get; set; }

        public static List<Monsters> CreateMonsters()
        {
            List<Monsters> mobs = new List<Monsters>()
        {
            new Monsters {Name = "Snake", Alive = true, Level = 1, Hitpoints = 10, AttackPower = 5, DefenseRating = 1},
            new Monsters {Name = "Rat", Alive = true, Level = 1, Hitpoints = 10, AttackPower = 1, DefenseRating = 1},
            new Monsters {Name = "Spider", Alive = true, Level = 1, Hitpoints = 10, AttackPower = 5, DefenseRating = 1},
            new Monsters {Name = "Turtle", Alive = true, Level = 1, Hitpoints = 50, AttackPower = 1, DefenseRating = 5}
        };
            return mobs;
        }

        
    }
}
