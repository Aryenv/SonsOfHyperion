using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Unidad", menuName = "Assets/NuevaUnidad")]
public class UnitData : ScriptableObject
{
    public string       enemyName;
    public UnitsEnum    unitType;
    public int          life;
    public float        moveSpeed;
    public int          attack;
    public float        attackCd;
    [Range(1, 50)]
    public int    attackRadius;
    [Range(1, 100)]
    public int    visionRadius;
    public Sprite   enemySprite;
    public RuntimeAnimatorController animator;
    public GameObject deadFeed;
}
