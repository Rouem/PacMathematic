using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer render;
    private Collider2D col;

    [SerializeField]
    private GameObject VFX;

    PlayerController player;

    private bool itemCatched = false;

    private void OnEnable()
    {
        GameManager.ReloadObjects += ResetObject;
    }

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        ResetObject();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<PlayerController>() || itemCatched) return;
        itemCatched = true;
        player = other.GetComponent<PlayerController>();
        Time.timeScale = 0f;
        GameManager.instance.EnableMathPuzzle();
        MathPuzzle.correctAwnserResult = CatchItem;
        MathPuzzle.wrongAwnserResult = FailItemCatch;        
    }

    void CatchItem(){
        itemCatched = false;
        player.EnablePowerMode();
        render.enabled = false;
        col.enabled = false;
        VFX?.SetActive(false);
        GameManager.instance.currentLevel.catchPotion();
    }

    void FailItemCatch(){
        player.transform.position = transform.position;
        itemCatched = false;
    }

    void ResetObject()
    {
        if (!render)
            render = GetComponent<SpriteRenderer>();
        if (!col)
            col = GetComponent<Collider2D>();
        render.enabled = true;
        col.enabled = true;
        VFX?.SetActive(true);
    }


    void ClearAllEvents()
    {
        GameManager.ReloadObjects -= ResetObject;
    }


    private void OnDestroy()
    {
        ClearAllEvents();
    }


    private void OnDisable()
    {
        ClearAllEvents();
    }
}
