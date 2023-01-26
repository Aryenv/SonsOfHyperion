using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAI : Unit
{
    //private EnemyPathfindingMovement    enemyMovement;
    public Vector2                      targetPosition;
    public string                       tipoEnemigo;
    float rndSpedd;

    private void Awake()
    {
        unitDisplay     = GetComponent<UnitDisplay>();
        //enemyMovement   = GetComponent<EnemyPathfindingMovement>();
        states         = UnitsStates.moving;      
    }

    private void Start()
    {
        PathNode baseNode = GameManager.Instance.Grid.GetFreeNode(GameManager.Instance.ReturnBasePos(), 1);
        targetPosition = new Vector2(baseNode.x, baseNode.y);

        rndSpedd = Random.Range(30, unitDisplay.moveSpeed);
    }

    private void Update()
    {
        switch (states)
        {
            case UnitsStates.moving:
                // Se mueve hacia la base
                MoveToTimer(GameManager.Instance.ReturnBasePos());
                //enemyMovement.MoveToTimer(freeVectorPosBase);
                //// Comprobar distancia a la base
                CheckDistanceToTarget(GameManager.Instance.ReturnBasePos());

                // Busca objetivo cercano
                //FindTarget();

                HandleMovement(rndSpedd);
                break;

            case UnitsStates.chasing:
                // Se mueve hacia el objetivo marcado
                //MoveToTimer(enemyMovement.ObjectiveVector());

                //CheckDistanceToObjective(enemyMovement.ObjectiveVector());
                break;

            case UnitsStates.attacking:
                StopMoving();
                Attack(targetPosition);
                break;

            case UnitsStates.dead:
                // Hacer la muerte                
                Die(SoundManager.Instance.enemyDie);
                break;

            case UnitsStates.stop:
                break;

            default:
                break;
        }
    }

    //private void CheckDistanceToObjective(Vector2 objective)
    //{
    //    if (Vector2.Distance(transform.position, enemyMovement.TransformVector2ToCell(objective)) > unitDisplay.visionRadius)
    //    {
    //        myState = UnitsStates.moving;
    //    }

    //    if (Vector2.Distance(transform.position, enemyMovement.TransformVector2ToCell(objective)) <= unitDisplay.attackRange)
    //    {
    //        if (Time.time > unitDisplay.attackCD)
    //        {
    //            enemyMovement.StopMoving();
    //            myState = UnitsStates.attacking;

    //            //aimShootAnims.ShootTarget(Player.Instance.GetPosition(), () =>
    //            //{
    //            //    state = State.ChaseTarget;
    //            //});

    //            nextAttackTime = Time.time + unitDisplay.attackCD;
    //        }
    //        else
    //        {
    //            myState = UnitsStates.chasing;
    //        }
    //    }
    //}

    // Busca a un objetivo de tipo aliado en su rango
    //private void FindTarget()
    //{
    //    // Compruebo si hay objetivos en mi radio
    //    if (enemyMovement.IsObjectiveInMyRadius(unitDisplay.visionRadius, UnitsEnum.ally))
    //    {
    //        // Objetivo en mi rango
    //        states = UnitsStates.chasing;
    //    }
    //}
}
