
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfindingMovement : MonoBehaviour
{
    private UnitAI unitAi;

    private List<PathNode> pathNodeList = new List<PathNode>();

    private int currentPathIndex;
    private float pathfindingTimer;

    Vector2 objectiveVector;

    private Vector3 moveDir;
    private Vector3 lastMoveDir;

    public PathNode targetNode;
    public Vector2 targetPos;

    private void Awake()
    {
        unitAi = GetComponent<UnitAI>();
    }

    public void HandleMovement()
    {
        pathfindingTimer -= Time.deltaTime;
        Debug.Log("Antes del IF");
        if (pathNodeList != null)
        {
            targetNode = pathNodeList[currentPathIndex];
            targetPos = TransformVector2ToCell(new Vector2(targetNode.x, targetNode.y));
            //targetNode.SetIsOcuped(true);

            //if (targetNode.isOcuped)
            //{
            //    MoveToFreeNode(unitAi.targetPosition);
            //}

            float reachedTargetDistance = 1f;
            if (Vector3.Distance(transform.position, targetPos) > reachedTargetDistance)
            {
                if (Mathf.Abs(targetPos.x - transform.position.x) > 0.1f)
                {
                    moveDir = new Vector2(1, 0);
                }
                else if (Mathf.Abs(targetPos.y - transform.position.y) > 0.1f)
                {
                    moveDir = new Vector2(0, 1);
                }
                //moveDir = (targetPos - new Vector2(transform.position.x, transform.position.y)).normalized;
                transform.position += moveDir * unitAi.unitDisplay.moveSpeed * Time.deltaTime;

                lastMoveDir = moveDir;
                //enemyMain.CharacterAnims.PlayMoveAnim(moveDir);
            }
            else
            {
                Debug.Log("currentIndex");
                currentPathIndex++;
                //pathNodeList[currentPathIndex - 1].SetIsOcuped(false);

                if (currentPathIndex >= pathNodeList.Count)
                {
                    Debug.Log("me paro " + currentPathIndex + " " + pathNodeList.Count);
                    pathNodeList[currentPathIndex - 1].SetIsOcuped(true);
                    StopMoving();
                    //enemyMain.CharacterAnims.PlayIdleAnim();
                }
            }
        }
        else
        {
            //enemyMain.CharacterAnims.PlayIdleAnim();
        }
    }

    public void StopMoving()
    {
        pathNodeList = null;
        moveDir = Vector3.zero;
    }

    public List<PathNode> GetPathVectorList()
    {
        return pathNodeList;
    }

    public void MoveTo(Vector2 targetPosition)
    {
        SetTargetPosition(targetPosition);
    }

    public void MoveToTimer(Vector2 targetPosition)
    {
        if (pathfindingTimer <= 0f)
        {
            SetTargetPosition(targetPosition);
        }
    }

    public void MoveToFreeNode(Vector2 targetPos)
    {
        PathNode node = GameManager.Instance.Grid.GetFreeNode(targetPos, 1);
        if (node == null)
        {
            node = GameManager.Instance.Grid.GetFreeNode(targetPos, 2);
        }
        if (node != null && !node.isOcuped)
        {
            Vector2 targetVector = new Vector2(node.x, node.y);
            SetTargetPosition(targetVector);
        }
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        Debug.Log("Seteo Target");
        currentPathIndex    = 0;
        Vector2 startPos    = GetMyPosition();
        pathNodeList        = GameManager.Instance.PathFinding.FindPathEnemy((int)startPos.x, (int)startPos.y, (int)targetPosition.x, (int)targetPosition.y);
        pathfindingTimer    = 0.1f;

        if (pathNodeList != null && pathNodeList.Count > 1)
        {
            pathNodeList.RemoveAt(0);
        }
    }

    public Vector3 GetLastMoveDir()
    {
        return lastMoveDir;
    }      

    public Vector2 GetMyPosition()
    {
        Vector2 startPos = GameManager.Instance.Grid.WorldPositionToCell(transform.position);
        return startPos;
    }

    public Vector2 TransformVector2ToCell(Vector2 vector)
    {
        Vector2 pos = GameManager.Instance.Grid.GetCenterCell(vector);
        return pos;
    }

    public bool IsObjectiveInMyRadius(int radius, UnitsEnum unitType)
    {
        PathNode enemyNode;
        enemyNode = GameManager.Instance.Grid.GetPathNodeWhitEnemy(GetMyPosition(), radius, unitType);
        if (enemyNode != null)
        {
            objectiveVector = TransformVector2ToCell(new Vector2(enemyNode.x, enemyNode.y));
            return true;
        }
        else
        {
            objectiveVector = Vector2.zero;
            return false;
        }
    }

    public Vector2 ObjectiveVector()
    { 
        return objectiveVector;
    }
}
