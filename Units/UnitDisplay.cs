using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDisplay : MonoBehaviour
{
    public UnitData         dataEnemy;
    public UnitsEnum        unitType;
    public UnitsStates      mystate;

    public int              life;
    public int              maxLife;
    public float            moveSpeed;
    public int              attack;
    public float            attackCD;
    public int              attackRange;
    public int              visionRadius;
    public SpriteRenderer   enemyImage;
    public Animator         unitAnimator;
    public GameObject       deadFeed;

    // Start is called before the first frame update
    void Start()
    {
        SetEnemy();
    }

    public void SetEnemy()
    {
        unitType            = dataEnemy.unitType;
        mystate             = UnitsStates.idle;
        life                = dataEnemy.life;
        maxLife             = life;
        moveSpeed           = dataEnemy.moveSpeed;
        attack              = dataEnemy.attack;
        attackCD            = dataEnemy.attackCd;
        attackRange         = dataEnemy.attackRadius;
        visionRadius        = dataEnemy.visionRadius;

        enemyImage.sprite   = dataEnemy.enemySprite;
        unitAnimator = GetComponent<Animator>();
        unitAnimator.runtimeAnimatorController = dataEnemy.animator;

        deadFeed = dataEnemy.deadFeed;
    }
}
