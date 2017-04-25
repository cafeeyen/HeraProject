using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static GameData savedGames;

    public static void Save()
    {
        savedGames = GameData.data;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "hera.sav"));
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "hera.sav")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Path.Combine(Application.persistentDataPath, "hera.sav"), FileMode.Open);
            SaveLoad.savedGames = (GameData)bf.Deserialize(file);
            GameData.data = savedGames;
            file.Close();
        }
    }
}
