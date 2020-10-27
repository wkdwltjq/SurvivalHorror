using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // 파일 입출력을 위한 네임스페이스
using System.Runtime.Serialization.Formatters.Binary;

public static class DataManager
{
    public static void Save(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.dataPath, "SaveData.bin");
        FileStream stream = File.Create(path);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData Load()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Path.Combine(Application.dataPath, "SaveData.bin");
            FileStream stream = File.OpenRead(path);
            GameData data = (GameData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            return default;
        }
    }
}
