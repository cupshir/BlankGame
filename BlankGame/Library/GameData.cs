using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    [Serializable]
    public class GameData
    {
        public List<Room> savedGameRooms;
        public Player savedPlayer;
        public string savedCurrentRoom;
    }
}
