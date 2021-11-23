using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Data/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField]
    public PlayerData data;
}

[System.Serializable]
public class PlayerData
{
    [Header("General Settings")]
    public float speed = 10;

    public LayerMask obstacle;

    public LayerMask targetLayer;

    public float catchMinDistance = 1f;

    [Header("Sprites")]
    [SerializeField]
    public Sprite PowerMode;

    [SerializeField]
    public Sprite NormalMode;

    [Header("VFX Area")]
    [SerializeField]
    public GameObject CatchVFX;
}
