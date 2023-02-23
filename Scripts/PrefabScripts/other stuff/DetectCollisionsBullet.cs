using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionsBullet : MonoBehaviour
{
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
//we can make bool for special enum like bullet with differen rules like animation
    private void OnTriggerEnter2D(Collider2D other)
    {
        Animator enemyAnimator;
        float destroyDelay = 0.5f;
        float destroyProjectilleDelay = 0.35f;

        if (other.gameObject.tag == "Enemy")
        {
            //add bullet animator
            enemyAnimator = other.gameObject.GetComponent<Animator>();

            _animator.SetBool("isHitTarget", true);
            enemyAnimator.SetBool("isEnemyDead", true);
            Destroy(gameObject, destroyProjectilleDelay);
            Destroy(other.gameObject, destroyDelay);
        }
    }
}
