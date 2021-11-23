using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public LevelManager currentLevel;

    private void Awake() {
        if(GameManager.instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    [SerializeField]
    private PlayerConfig PlayerConfiguration; 

    public PlayerData playerConfig {
        get { return PlayerConfiguration.data;}
    }

    [SerializeField]
    private EnemyConfig EnemyConfiguration; 

    public EnemyData enemyData {
        get { return EnemyConfiguration.data;}
    }

    [SerializeField]
    private LevelManager[] levels;


    [Header("UI section")]
    [SerializeField]
    private GameObject titleScreen;

    [SerializeField]
    private GameObject splashScreen, endScreen, winScreen, loseScreen, levelSelectionScreen, gameUI;

    public MathPuzzle mathPuzzle;

    public static event System.Action ReloadObjects;

    public void EnableMathPuzzle(){
        mathPuzzle.gameObject.SetActive(true);
    }

    public void ReloadAllObjects(){
        ReloadObjects?.Invoke();
    }

    private void Start() {
        splashScreen.SetActive(true);
        mathPuzzle.gameObject.SetActive(false);
        Invoke(nameof(QuitSplashScreen),1f);
    }

    void QuitSplashScreen(){
        splashScreen.SetActive(false);
        TitleScreen();
    }

    public void TitleScreen(){
        titleScreen.SetActive(true);
        levelSelectionScreen.SetActive(false);
        endScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        gameUI.SetActive(false);
        DisableLevels();
    }

    public void EnableLevelScreen(){
        titleScreen.SetActive(false);
        levelSelectionScreen.SetActive(true);
        endScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        gameUI.SetActive(false);
    }

    public void EnableWinScreen(){
        titleScreen.SetActive(false);
        levelSelectionScreen.SetActive(false);
        endScreen.SetActive(true);
        winScreen.SetActive(true);
        loseScreen.SetActive(false);
        gameUI.SetActive(false);
    }

    public void EnableLoseScreen(){
        titleScreen.SetActive(false);
        levelSelectionScreen.SetActive(false);
        endScreen.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(true);
        gameUI.SetActive(false);
    }

    public void EnableGameUI(){
        titleScreen.SetActive(false);
        levelSelectionScreen.SetActive(false);
        endScreen.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        gameUI.SetActive(true);
    }

    public void DisableLevels(){
        foreach(var level in levels){
            level.gameObject.SetActive(false);
        }
    }


    public void PlayAgain(){
        ReloadAllObjects();
    }


}
