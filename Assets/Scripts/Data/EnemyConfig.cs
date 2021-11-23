using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Data/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField]
    public EnemyData data;
}

[System.Serializable]
public class EnemyData
{
    [Header("General Settings")]
    public float speed = 3;

    public LayerMask obstacle;

    public LayerMask targetLayer;

    public float catchMinDistance = 1f;

    public float distanceToDestiny = 5f;

    [Header("VFX Area")]
    [SerializeField]
    public GameObject CatchVFX;
}
