using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

        public static string SaveGameToFile(List<Room> gameRooms, Player player, string currentRoom)
        {
            string content = "";
            GameData saveData = new GameData();

            saveData.savedGameRooms = gameRooms;
            saveData.savedCurrentRoom = currentRoom;
            saveData.savedPlayer = player;

            string userFile = player.Name + ".sav";
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BlankGame");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, userFile);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, saveData);
            stream.Close();

            content = content + "Game has been saved";
            return content;
        }

        public static Tuple<List<Room>, Player, string, string> LoadGameFromFile(List<Room> gameRooms, Player currentPlayer, string currentRoom)
        {
            string content = "";
            GameData loadData = new GameData();

            Console.Clear();
            string loadFile = Player.GetPlayerName("load") + ".sav";
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BlankGame");
            path = Path.Combine(path, loadFile);

            if (Directory.Exists(path))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                loadData = (GameData)formatter.Deserialize(stream);
                stream.Close();

                content = content + "Game has been loaded";
            }
            else
            {
                loadData.savedGameRooms = gameRooms;
                loadData.savedPlayer = currentPlayer;
                loadData.savedCurrentRoom = currentRoom;

                content = "A save file for that name does not exist.";
            }

            return Tuple.Create(loadData.savedGameRooms, loadData.savedPlayer, loadData.savedCurrentRoom, content);
        }
    }
}
