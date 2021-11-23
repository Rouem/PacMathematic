using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Character
{
    public Character target;

    private PlayerController player;

    private bool invertDir;

    private float distanceToDestiny = 5f;

    private bool isDied = false;

    private GameObject CatchVFX;

    private Vector3 targetPos;

    private Vector2 lastDir;


    public override void Update()
    {
        if (animations)
            animations.Play("run", !player.isDied);

        if (player.isDied || isDied) return;


        if (player.IsPowered)
        {
            if (speed > 0)
                speed *= -1;
        }
        else
        {
            if (speed < 0)
                speed *= -1;
        }

        FollowCharacter();

        if (lastDir.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        CheckTargetDistance();

    }


    void FollowCharacter()
    {

        if (!Physics2D.Linecast(rb.position, target.rb.position, obstacle))
        {
            lastDir = (target.rb.position - rb.position).normalized;
            targetPos = target.targetPosition;
        }

        if (targetIsNear)
        {
            ChangeDirection();
            targetPos = rb.position + lastDir;
        }

        RunToPosition(targetPos);
    }


    float time = 0;

    void ChangeDirection()
    {
        if (
            Physics2D.Linecast(rb.position, rb.position + lastDir, obstacle) ||
            Physics2D.Linecast(rb.position, rb.position + lastDir, gameObject.layer
        ))
            if (time > 2)
            {
                time = 0;
                if (invertDir)
                {
                    invertDir = false;
                    lastDir = new Vector2(
                        Random.Range(-1f, 1f),
                        Random.Range(-1f, 1f)
                    ).normalized;
                }
                else
                {
                    invertDir = true;
                    lastDir *= -1;
                }
            }
            else
                time += Time.deltaTime;
    }


    public override void ResetCharacter()
    {
        GetData();
        base.ResetCharacter();
        if (speed < 0)
            speed *= -1;
        player = target.GetComponent<PlayerController>();
        targetPos = target.transform.position;
        if (!rb)
            rb = GetComponent<Rigidbody2D>();
        lastDir = ((Vector2)targetPos - rb.position).normalized;
        isDied = false;
    }


    public override void Catch()
    {
        if (isDied) return;
        base.Catch();
        isDied = true;
        if (!CatchVFX) return;
        var vfx = Instantiate(CatchVFX, rb.position, Quaternion.identity);
        Destroy(vfx, 1.5f);
        GameManager.instance.currentLevel.defeatMonster();
    }


    private void RunToPosition(Vector2 destiny)
    {
        rb.position = Vector2.MoveTowards(
            rb.position,
            destiny,
            (player.IsPowered ? speed * 1.8f : speed) * Time.deltaTime
        );
    }


    private bool targetIsNear
    {
        get { return ((Vector2)targetPos - rb.position).magnitude <= distanceToDestiny; }
    }


    private void GetData()
    {
        var config = GameManager.instance.enemyData;
        speed = config.speed;
        catchMinDistance = config.catchMinDistance;
        obstacle = config.obstacle;
        targetLayer = config.targetLayer;
        distanceToDestiny = config.distanceToDestiny;
        CatchVFX = config.CatchVFX;
    }


}
