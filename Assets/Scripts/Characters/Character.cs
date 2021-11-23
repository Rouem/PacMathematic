using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;

    [HideInInspector]
    public Vector2 direction;

    [HideInInspector]
    public float speed = 10;

    [HideInInspector]
    public LayerMask obstacle;

    [HideInInspector]
    public LayerMask targetLayer;

    [HideInInspector]
    public float catchMinDistance = 1f;

    public Transform sprite;

    public Vector3 targetPosition
    {
        get { return rb.position + direction * speed * 0.5f; }
    }

    [HideInInspector]
    public Vector3 startPosition = new Vector3(100,100,100);

    [HideInInspector]
    public AnimationsController animations;

    private void OnEnable() {
        GameManager.ReloadObjects += ResetCharacter;
    }

    public virtual void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animations = sprite.GetComponent<AnimationsController>();
        ResetCharacter();
    }

    public virtual void Update() { }

    public virtual void Catch(){ 
        sprite.gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
    }

    public virtual void CheckTargetDistance(){
        var foundedTarget = Physics2D.OverlapCircle(rb.position,catchMinDistance,targetLayer);
        if(foundedTarget != null)
            foundedTarget.GetComponent<Character>().Catch();
    }

    public virtual void ResetCharacter(){ 
        if(startPosition != new Vector3(100,100,100))
            transform.position = startPosition;
        sprite.gameObject.SetActive(true);
        GetComponent<Collider2D>().enabled = true;
    }

    void ClearAllEvents(){
        GameManager.ReloadObjects -= ResetCharacter;
    }


    private void OnDestroy() {
        ClearAllEvents();
    }
    

    private void OnDisable() {
        ClearAllEvents();
    }



}
