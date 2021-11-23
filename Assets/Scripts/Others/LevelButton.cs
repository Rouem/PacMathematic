using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public LevelManager level;

    public void SelectThisLevel(){
        GameManager.instance.currentLevel = level;
        level.gameObject.SetActive(true);
    }
}
