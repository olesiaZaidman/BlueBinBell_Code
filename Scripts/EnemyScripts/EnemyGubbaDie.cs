using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGubbaDie : MonoBehaviour
{
    #region Die
    CapsuleCollider2D headCollider;
    Health enemyHealth;
    bool isDead = false;
    ItemDrop itemDrop;
    void Update()
    {
        if (IsDead())
        {
            //  playerDamage.isInvincible = true;
            DestroyEnemy();
        }
        if (IsGameOverInWater())
        {
            DieInWater();
        }
    }



    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
        headCollider = GetComponent<CapsuleCollider2D>();
        itemDrop = GetComponent<ItemDrop>();
    }
    void DestroyEnemy()
    {
        bool isDroped = false;
        float destroyDelay = 1f;

        if (!isDead)
        {
            isDead = true;
            headCollider.isTrigger = true;
            transform.localScale = new Vector2(1f, -1f);
            //animator.SetBool("isEnemyDead", true);

            if (!isDroped)
            {
                itemDrop.DropItems();
                isDroped = true;
            }

            Destroy(gameObject, destroyDelay);
            //  playerDamage.isInvincible = false;
        }


    }

    bool IsDead()
    {
        return enemyHealth.GetHealthPoints() <= 0;
    }

    void DieInWater()
    {
        float destroyDelay = 1f;
        transform.localScale = new Vector2(1f, -1f);
        //  animator.SetBool("isEnemyDead", true);
        Destroy(gameObject, destroyDelay);
    }

    bool IsGameOverInWater()
    { return headCollider.IsTouchingLayers(LayerMask.GetMask("Water")); }

    #endregion
}
