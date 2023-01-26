using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public UnitDisplay unitDisplay;
    public UnitsStates states;


    protected List<PathNode> pathNodeList = new List<PathNode>();

    protected int currentPathIndex;
    public float pathfindingTimer;

    [SerializeField] Image lifeBar;

    private PathNode targetNode;
    private Vector2 targetPos;

    private Vector3 moveDir = Vector3.zero;
    private float nextAttackTime;
    private bool isStop;
    private float dieTime = 1f;
    private bool isSpawnedCards;

    protected virtual void HandleMovement(float speed)
    {
        pathfindingTimer -= Time.deltaTime;
        if (pathNodeList != null)
        {

            targetNode = pathNodeList[currentPathIndex];
            if (targetNode.isOcuped == true)
            {
                targetPos = MoveToFreeNode(GameManager.Instance.ReturnBasePos());
            }
            else
            {
                targetPos = TransformVector2ToCenterCell(new Vector2(targetNode.x, targetNode.y));
            }

            float reachedTargetDistance = 1f;

            if (Vector3.Distance(transform.position, targetPos) > reachedTargetDistance)
            {
                Vector3 moveDir = (targetPos - new Vector2(transform.position.x, transform.position.y)).normalized;


                if (transform.position.x < targetPos.x)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    unitDisplay.unitAnimator.SetFloat("X", moveDir.x);
                }
                else if(transform.position.x > targetPos.x)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    unitDisplay.unitAnimator.SetFloat("X", moveDir.x);
                }

                if (transform.position.y < targetPos.y)
                {
                    unitDisplay.unitAnimator.SetFloat("Y", moveDir.y);
                }
                else if (transform.position.y > targetPos.y)
                {
                    unitDisplay.unitAnimator.SetFloat("Y", moveDir.y);
                }

                //if (targetPos.x - transform.position.x > 0.1f)
                //{
                //    moveDir = new Vector2(1, 0);
                //}
                //else if (targetPos.x - transform.position.x < 0.1f)
                //{
                //    moveDir = new Vector2(-1, 0);
                //}
                //else if (targetPos.y - transform.position.y > 0.1f)
                //{
                //    moveDir = new Vector2(0, 1);
                //}
                //else if (targetPos.y - transform.position.y < 0.1f)
                //{
                //    moveDir = new Vector2(0, -1);
                //}

                transform.position += moveDir * speed * Time.deltaTime;

                //enemyMain.CharacterAnims.PlayMoveAnim(moveDir);
            }
            else
            {
                currentPathIndex++;

                if (currentPathIndex >= pathNodeList.Count)
                {
                    pathNodeList[currentPathIndex - 1].SetIsOcuped(true);
                    pathNodeList[currentPathIndex - 1].myUnitType = unitDisplay.unitType;
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

    protected virtual void StopMoving()
    {
        if (pathNodeList != null && currentPathIndex <= pathNodeList.Count - 1)
        {
            pathNodeList[currentPathIndex].SetIsOcuped(true);
            pathNodeList[currentPathIndex].myUnitType = unitDisplay.unitType;
        }

        pathNodeList = null;
        moveDir = Vector3.zero;
        isStop = true;
    }

    protected List<PathNode> GetPathVectorList()
    {
        return pathNodeList;
    }

    protected virtual void MoveToTimer(Vector2 targetPosition)
    {
        if (pathfindingTimer <= 0f)
        {
            SetTargetPosition(targetPosition);
        }
    }

    protected void SetTargetPosition(Vector2 targetPosition)
    {
        if (!isStop)
        {
            currentPathIndex = 0;
            Vector2 startPos = GameManager.Instance.Grid.WorldPositionToCell(transform.position);
            pathNodeList = GameManager.Instance.PathFinding.FindPathEnemy((int)startPos.x, (int)startPos.y, (int)targetPosition.x, (int)targetPosition.y);
            pathfindingTimer = 0.5f;

            if (pathNodeList != null && pathNodeList.Count > 1)
            {
                pathNodeList.RemoveAt(0);
            }
        }
    }

    protected Vector2 TransformVector2ToCenterCell(Vector2 vector)
    {
        Vector2 pos = GameManager.Instance.Grid.GetCenterCell(vector);
        return pos;
    }

    protected virtual bool IsObjectiveInMyRadius(int radius, UnitsEnum unitType)
    {
        PathNode enemyNode;
        enemyNode = GameManager.Instance.Grid.GetPathNodeWhitEnemy(transform.position, radius, unitType);
        if (enemyNode != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void CheckDistanceToTarget(Vector2 objective)
    {
        Vector2 objetiveVector = TransformVector2ToCenterCell(objective);

        float d = HypotenuseLength(transform.position.x, objetiveVector.y);
        //Debug.Log(d + " ------------------------------------- d");

        //Debug.Log(Vector2.Distance(transform.position, objetiveVector));

        if (Vector2.Distance(transform.position, objetiveVector) <= unitDisplay.attackRange)
        {
            states = UnitsStates.attacking;
        }
        else if (isStop && d <= unitDisplay.attackRange)
        {
            states = UnitsStates.attacking;
        }
        else
        {
            states = UnitsStates.moving;
        }
    }

    protected virtual void Attack(Vector2 objective)
    {
        CheckDistanceToTarget(objective);
        if (Time.time > nextAttackTime)
        {
            unitDisplay.unitAnimator.SetTrigger("Attack");
            nextAttackTime = Time.time + unitDisplay.attackCD;
            //enemyMovement.StopMoving();

            // Play Sonido Ataque
            //SoundManager.Instance.PlayEnemyAttack();

            // Ataca el enemigo
            BaseStats.Instance.RecieveDamage(unitDisplay.attack);
            // Ataca la base 
            //RecieveDamage(BaseStats.Instance.baseAttack);

            //aimShootAnims.ShootTarget(Player.Instance.GetPosition(), () =>
            //{
            //    state = State.ChaseTarget;
            //});
        }
        //unitDisplay.unitAnimator.SetTrigger("Attack");
    }

    public void RecieveDamage(int damage)
    {
        unitDisplay.life -= damage;
        BarraVida();
        if (unitDisplay.life <= 0)
        {
            unitDisplay.unitAnimator.SetBool("isDead", true);
            dieTime += Time.time;
            Instantiate(unitDisplay.deadFeed, this.transform.position, Quaternion.identity);
            states = UnitsStates.dead;
        }
    }

    protected Vector2 MoveToFreeNode(Vector2 target)
    {
        PathNode node;
        node = GameManager.Instance.Grid.GetFreeNode(target, 1);
        Vector2 tg = TransformVector2ToCenterCell(new Vector2(node.x, node.y));
        return tg;
    }

    protected virtual void Die(AudioClip clip)
    {
        if (unitDisplay.unitType == UnitsEnum.enemy)
        {
            //int rnd = Random.Range(0, 100);
            //if (rnd > 2 && rnd <= 15 && !isSpawnedCards)
            //{
            //    GameManager.Instance.CardManager.AddRandomCard(1);
            //    isSpawnedCards = true;
            //}
            //if (rnd <= 2 && !isSpawnedCards)
            //{
            //    GameManager.Instance.CardManager.AddRandomCard(2);
            //    isSpawnedCards = true;
            //}
        }
        if (dieTime <= Time.time)
        {
            targetNode.SetIsOcuped(false);
            targetNode.myUnitType = UnitsEnum.Null;

            //pathNodeList[currentPathIndex - 1].SetIsOcuped(false);
            //pathNodeList[currentPathIndex - 1].myUnitType = UnitsEnum.Null;
            SoundManager.Instance.PlayAnyClip(clip);
            if (unitDisplay.dataEnemy.enemyName == "Grunt")
            {
                GameManager.Instance.UIManager.ActivateMaxChaosUI();
                this.gameObject.SetActive(false);
                Time.timeScale = 0f;
            }
            this.gameObject.SetActive(false);
        }
    }

    private void BarraVida()
    {
        float currentLife = lifeBar.fillAmount = (float)unitDisplay.life / (float)unitDisplay.maxLife;
        lifeBar.fillAmount = currentLife;
    }

    float HypotenuseLength(float sideALength, float sideBLength)
    {
        return Mathf.Sqrt(sideALength * sideALength - sideBLength * sideBLength);
    }
}
