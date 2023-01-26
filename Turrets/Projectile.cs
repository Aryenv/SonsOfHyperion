using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float               speed;

    EnemyAI             enemigLife;
    Transform           enemy;
    Vector2             target;

    private void Start()
    {
        speed           = 200f;
        enemy           = GameObject.FindObjectOfType<UnitAI>().transform;
        target          = new Vector2(enemy.position.x, enemy.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        destroyOnPosition();
        CheckEnemy();
        MoveBullet();
              
    }

    private void CheckEnemy()
    {
        if (!enemy)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<UnitAI>() != null)
        {          
            DestroyProjectile();
        }
    }

    void MoveBullet()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void destroyOnPosition()
    {
        if (transform.position.x >= target.x && transform.position.y >= target.y)
        {
            DestroyProjectile();
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
