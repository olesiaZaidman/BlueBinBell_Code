using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    EnemyData enemyData;
    PushAttackCalculator PushCalculator;
    EnemyAttack enemyAttack;
    EnemyGetDamage enemyDamage;

    GameObject player;
    PlayerData playerData;
    PlayerMovement2D playerMovement;
    void Awake()
    {
        enemyData = GetComponent<EnemyData>();
        player = GameObject.Find("Player");
        PushCalculator = GetComponent<PushAttackCalculator>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyDamage = GetComponent<EnemyGetDamage>();

        if (player != null)
        {
            playerData = player.GetComponent<PlayerData>();
            playerMovement = player.GetComponent<PlayerMovement2D>();
        }
    }

    void OnCollisionEnter2D(Collision2D other)  //enemy attacks player
    {
        if (other.gameObject.tag == "Player")
        {
            PushAttackCalculator playerPushCalculator = other.gameObject.GetComponent<PushAttackCalculator>();

            if (!playerMovement.IsAttacking())
            {
              //  Debug.Log("Enemy has attacled");
                enemyAttack.hasAttacked = true; //hasAttacked gives extra mini pause between attacks
                float delayBeforeNextAttack = 2f;
                StartCoroutine(enemyAttack.FallAsleepAfterDelayRoutine(delayBeforeNextAttack));
            }
        }
        else return;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerProjection")
        {
            enemyDamage.EnemyGetsShadowFreeze();
            enemyAttack.CollideWithShadow(other);
        }

        if (other.gameObject.tag == "PlayerFX") //enemy gets damage from attack
        {
            enemyDamage.EnemyGetsDamage(playerData.AttackPoints); //(_damageBullets); // int _damageBullets = 20;
          //  Debug.Log(other.gameObject.tag + " Bullet damage!");
        }

        if (other.gameObject.tag == "PlayerKick") //enemy gets damage from attack
        {
          //  Debug.Log("PlayerKick: enemyDamage.hasBeenDamaged");
            CollectDataForPushAttack();
            enemyDamage.isPushedInAttackEnemy = true; //Activates EnemyRecievePushPhysics() in EnemyGetDamage
         //   Debug.Log(other.gameObject.tag + " Kick AAA!");
        }
        else return;
    }


    void CollectDataForPushAttack()
    {
        PushCalculator.SetVectorDirection(playerData.LookDirectionOnX);
        enemyData.SetEnemyDamagePoints(playerData.AttackPoints); //(_damageKick); int _damageKick = 10;
        //do we need this? SetEnemyDamagePoints
        PushCalculator.SetPushStrength(playerData.pushAttackStrength);
    }

}
