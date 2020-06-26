using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System;

namespace DemonSlayer.Assets.Scripts.Saving
{
    public class SaveSystem : MonoBehaviour
    {
        public static void SavePlayer(Player player)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.sav";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(player);

            Debug.Log("Current level is: " + data.currentStage);

            formatter.Serialize(stream, data);
            stream.Close();
            Debug.Log("GAME SAVED");
        }

        public static PlayerData LoadPlayer()
        {
            string path = Application.persistentDataPath + "/player.sav";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                Debug.Log("Game LOADED");
                return data;
            }
            else
            {
                return null;
            }
        }
    }
}