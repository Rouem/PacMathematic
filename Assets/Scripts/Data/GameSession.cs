using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class GameSession
{
    private const string key = "mathemons"; 
    public static void Save(GameSessionData data){
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key,json);
        PlayerPrefs.Save();
    }

    public static GameSessionData Load(){
        string json = PlayerPrefs.GetString(key);
        GameSessionData storedData = JsonUtility.FromJson<GameSessionData>(json);
        if(storedData != null)
            return storedData;
        else
            return new GameSessionData();
    }

}

[System.Serializable]
public class GameSessionData{
    public string mapName = "no data";
    public string TimeLevel = "no data";
    public float timer = 0;
}
