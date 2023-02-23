using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if ((other.gameObject.tag == "Enemy"))
    //    {
    //        animator.SetBool("isHitTarget", true);
    //        Debug.Log("Hit Enemy");
    //        Destroy(gameObject, 0.3f);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Enemy"))
        {
            animator.SetBool("isHitTarget", true);
            Destroy(gameObject, 0.3f);
        }
    }
}
