using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GubbaEnemyMovement : MonoBehaviour
{
    //Move and Flip Enemy Sprite

    Rigidbody2D rb;
    EnemyData enemyData;
    EnemyAttack enemyAttack;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyData = GetComponent<EnemyData>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    //the simplest code that moves right(moveSpeed = 1f;) or left (moveSpeed = -1f;)
    private void Update()
    {
      //  if (enemyAttack.isSleeping)
      //  { GubbaWaitingMovement(); }
       
    }

    void GubbaWaitingMovement()
    {
        rb.velocity = new Vector2(enemyData.speed, 0f);
    }

     void OnTriggerExit2D(Collider2D other)
     {
        enemyData.speed = -enemyData.speed;
        FlipEnemySprite();
     }

    //FlipEnemySprite(moveSpeed);
    //void FlipEnemySprite(float _facingDirection)
    //{
    //    transform.localScale = new Vector2(_facingDirection, 1f);   
    //}

    void FlipEnemySprite()
    {
        transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), 1f);
    }

}
