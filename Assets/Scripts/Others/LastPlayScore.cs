using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastPlayScore : MonoBehaviour
{
    public Text lastPlay;

    private void OnEnable() {
        GameSessionData data = GameSession.Load();
        lastPlay.text = "Map Name:\n"+data.mapName.ToUpper()+"\n\nFinish Time:\n"+data.TimeLevel;
    }
}
