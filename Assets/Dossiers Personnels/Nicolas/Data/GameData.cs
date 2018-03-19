using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData 
{ 
    string playerFile = "player.json";

    public string[] LoadFile(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        //Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            string[] allJsonObjects = dataAsJson.Split('/');
            return allJsonObjects;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
            throw new System.ArgumentException(string.Format("File {0} does not exist.", fileName));
        }
    }

    public List<PlayerData> LoadPlayerData()
    {
        List<PlayerData> player = new List<PlayerData>();

        string[] allJsonObjects = LoadFile(playerFile);

        foreach (string json in allJsonObjects)
        {
            player.Add(JsonUtility.FromJson<PlayerData>(json));
        }

        return player;
    }

    //public void SaveGameData(List<PlayerData> data)
    //{

    //    string dataAsJson = JsonUtility.ToJson(data);

    //    string filePath =Application.streamingAssetsPath + playerFile;
    //    File.WriteAllText(filePath, dataAsJson);

    //}
}