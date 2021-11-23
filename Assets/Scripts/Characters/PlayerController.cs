using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{

    private bool moving = false;

    private bool powerMode = false;

    public bool IsPowered
    {
        get { return powerMode; }
    }

    [HideInInspector]
    public bool isDied = false;

    private Sprite PowerMode;
    private Sprite NormalMode;

    [Header("VFX Area")]
    private GameObject CatchVFX;

    [SerializeField]
    private GameObject RunVFX;

    [SerializeField]
    private GameObject PowerModeVFX;


    public override void Update()
    {
        if (animations)
            animations.Play("run", moving);

        if (RunVFX)
        {
            runFX.enabled = moving;
        }

        if (isDied) return;
        CheckDirection();
        MoveCharacter();
        CheckTargetDistance();
    }


    private void CheckDirection()
    {
        Vector2 newDir = Vector2.zero;

        newDir = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        direction = newDir.normalized;

        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        moving = direction != Vector2.zero;

    }

    ParticleSystem.EmissionModule runFX;
    private void MoveCharacter()
    {
        if (moving)
        {
            rb.position = Vector2.MoveTowards(
                rb.position,
                rb.position + direction.normalized,
                speed * Time.deltaTime
            );
        }


    }


    public void EnablePowerMode()
    {
        powerMode = true;
        if (PowerModeVFX)
            PowerModeVFX.SetActive(powerMode);
        var spRender = sprite.GetComponent<SpriteRenderer>();
        if (spRender != null)
        {
            if (PowerMode != null)
                spRender.sprite = PowerMode;
        }
        CancelInvoke(nameof(DisablePowerMode));
        Invoke(nameof(DisablePowerMode), 5f);
    }


    private void DisablePowerMode()
    {
        CancelInvoke(nameof(DisablePowerMode));
        powerMode = false;
        if (PowerModeVFX)
            PowerModeVFX.SetActive(powerMode);
        var spRender = sprite.GetComponent<SpriteRenderer>();
        if (spRender != null)
        {
            if (NormalMode != null)
                spRender.sprite = NormalMode;
        }
    }


    public override void Catch()
    {
        if (isDied) return;
        base.Catch();
        //GameOverScreen

        if (powerMode) return;
        isDied = true;
        moving = false;

        if (!CatchVFX) return;
        var vfx = Instantiate(CatchVFX, rb.position, Quaternion.identity);
        Destroy(vfx, 1.5f);

        GetComponent<Collider2D>().enabled = false;

        transform.position = startPosition;

        GameManager.instance.EnableLoseScreen();
    }

    public override void CheckTargetDistance()
    {
        if (!powerMode) return;
        var foundedTarget = Physics2D.OverlapCircle(rb.position, catchMinDistance, targetLayer);
        if (foundedTarget != null)
            foundedTarget.GetComponent<Character>().Catch();
    }


    public override void ResetCharacter()
    {
        GetData();

        base.ResetCharacter();
        isDied = false;
        CancelInvoke(nameof(DisablePowerMode));
        DisablePowerMode();

        GetComponent<Collider2D>().enabled = true;
        runFX = RunVFX.GetComponentInChildren<ParticleSystem>().emission;
        if (PowerModeVFX)
            PowerModeVFX.SetActive(false);
    }

    private void GetData()
    {
        var config = GameManager.instance.playerConfig;
        speed = config.speed;
        catchMinDistance = config.catchMinDistance;
        obstacle = config.obstacle;
        targetLayer = config.targetLayer;
        PowerMode = config.PowerMode;
        NormalMode = config.NormalMode;
        CatchVFX = config.CatchVFX;
    }

}
