using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private UnitDisplay enemyDisplay;
    private List<PathNode> pathVectorList = new List<PathNode>();
    private PathNode targetNode;
    private Vector2 targetVector;

    private int currentPathIndex;

    public float speed;

    public bool inRange;
    public bool isSotp;
    private void Start()
    {
        SetReferences();
        SetDestination();
    }

    private void SetReferences()
    {
        enemyDisplay = GetComponent<UnitDisplay>();
    }

    private void Update()
    {
        HandleMovement();
    }

    public void     SetDestination()
    {
        targetNode      = GameManager.Instance.Grid.GetFreeNode(GameManager.Instance.ReturnBasePos(), 2);
        targetVector    = new Vector2(targetNode.x, targetNode.y);
        SetTargetPosition(targetVector);
    }

    public void SetDestination(PathNode node)
    {
        targetVector = new Vector2(node.x, node.y);
        SetTargetPosition(targetVector);
    }

    public void     HandleMovement()
    {
        if (pathVectorList != null)
        {
            // Busca el nuevo nodo
            PathNode targetPosition = pathVectorList[currentPathIndex];
            Vector2 targetPos = TranformNodeToCell(new Vector2(targetPosition.x, targetPosition.y));

            if (targetPosition.isOcuped)
            {
                SetDestination();
            }
            else
            {
                targetPosition.isOcuped = true;
            }

            // Comprueba que la distancia al nodo es superior a 1
            if (Vector2.Distance(transform.position, targetPos) >= 1f)
            {
                Vector3 moveDir = (targetPos - new Vector2(transform.position.x, transform.position.y)).normalized;

                // Se mueve
                float distanceBefore = Vector2.Distance(GetPosition(), targetPos);
                transform.position += moveDir * speed * Time.deltaTime;
                isSotp = false;
            }
            else
            {
                // Se ejecuta cuando la distancia al nodo es inferior a 1
                targetPosition.isOcuped = false;
                currentPathIndex++;
                if (GetDistance() <= 1)
                {
                    inRange = true;
                    StopMoving();
                }
                else
                {
                    inRange = false;
                }
                if (pathVectorList != null && currentPathIndex <= pathVectorList.Count && pathVectorList[currentPathIndex].isOcuped)
                {
                    StopMoving();
                }
                if (!inRange)
                {
                    SetDestination();
                }
                // Se ejecuta cuando ha llegado al nodo final
                if (pathVectorList != null && currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    IEnumerator Attack()
    {
        while (enemyDisplay.life > 0)
        {
            // Ataca el enemigo
            BaseStats.Instance.RecieveDamage(enemyDisplay.attack);
            // Ataca la base 
            //enemyDisplay.RecieveDamage(BaseStats.Instance.baseAttack);

            if (enemyDisplay.life <= 0)
            {
                //enemyDisplay.Die();
                yield break;
            }
            yield return new WaitForSeconds(enemyDisplay.attackCD);
        }
    }

    private void     SetTargetPosition(Vector2 targetPos)
    {
        currentPathIndex = 0;
        Vector2 startPos = GameManager.Instance.Grid.WorldPositionToCell(transform.position);
        pathVectorList = GameManager.Instance.PathFinding.FindPathEnemy((int)startPos.x,(int)startPos.y, (int)targetPos.x, (int)targetPos.y);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
    private int GetDistance()
    {
        int Distance = 999;

        PathNode targetNode = GameManager.Instance.Grid.GetFreeNode(GameManager.Instance.ReturnBasePos(), 2);
        Vector2  targetPos = new Vector2(targetNode.x, targetNode.y);
        Vector2  startPos = GameManager.Instance.Grid.WorldPositionToCell(transform.position);

        if (GameManager.Instance.PathFinding.FindPath((int)startPos.x, (int)startPos.y, (int)targetPos.x, (int)targetPos.y) != null)
        {
            Distance = GameManager.Instance.PathFinding.FindPath((int)startPos.x, (int)startPos.y, (int)targetPos.x, (int)targetPos.y).Count;
        }

        return Distance;
    }
    private Vector2 TranformNodeToCell(Vector2 node)
    {
        Vector2 pos = GameManager.Instance.Grid.GetCenterCell(node);
        return  pos;
    }
    public void     StopMoving()
    {
        pathVectorList = null;
        isSotp = true;
        if (inRange)
        {
            StartCoroutine(Attack());
        }
    }
    public Vector2  GetPosition()
    {
        Vector2 startPos = GameManager.Instance.Grid.WorldPositionToCell(transform.position);
        return startPos;
    }
}
