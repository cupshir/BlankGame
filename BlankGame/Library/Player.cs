using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame.Library
{
    class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hitpoints { get; set; }
        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int AttackPower { get; set; }
        public int DefenseRating { get; set; }
        public List<Item> Inventory { get; set; }

    }
}
