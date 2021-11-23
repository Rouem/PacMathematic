using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public string mapName = "";

    public Text monstersT, potionsT, timeT;

    int minutes = 0, seconds = 0;


    public GameObject[] monsters, potions;

    private int currentMonsters = 0, currentPotions = 0;

    public void defeatMonster()
    {
        currentMonsters++;
        UpdateMonsters();

    }

    public void catchPotion()
    {
        currentPotions++;
        UpdatePotions();
    }

    private float timer = 0;

    private bool levelFinished = false;
    private bool gameStart = false;

    public void UpdateMonsters()
    {
        monstersT.text = "(" + currentMonsters + "/" + monsters.Length + ")";
        CheckGameConditions();
    }

    public void UpdatePotions()
    {
        potionsT.text = "(" + currentPotions + "/" + potions.Length + ")";
        CheckGameConditions();
    }

    public void UpdateTimer()
    {
        timer += Time.unscaledDeltaTime;
        minutes = (int)(timer / 60);
        seconds = (int)(timer - (minutes * 60));
        timeT.text = "Time " + minutes + ":" + seconds;
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        ResetObject();
    }

    private void Update()
    {
        if (!gameStart)
        {
            if (Input.anyKeyDown)
            {
                gameStart = true;
                Time.timeScale = 1f;
            }
        }
        else
        {
            if (!levelFinished)
                UpdateTimer();
        }
    }


    void CheckGameConditions()
    {
        levelFinished = (currentMonsters >= monsters.Length) && (currentPotions >= potions.Length);
        if (levelFinished)
        {
            GameManager.instance.EnableWinScreen();
            GameSessionData data = new GameSessionData();
            data.mapName = mapName;
            data.timer = timer;
            data.TimeLevel = minutes + ":" + seconds;
            GameSession.Save(data);
        }
    }


    public void ResetObject()
    {
        currentMonsters = 0;
        currentPotions = 0;
        timer = 0;
        levelFinished = false;
        gameStart = false;
        UpdateMonsters();
        UpdatePotions();
        UpdateTimer();
        Time.timeScale = 0f;
        GameManager.instance.ReloadAllObjects();
    }

}
