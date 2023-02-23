using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerPush : MonoBehaviour
{
    public float convyerSpeed = 10000f;
    [SerializeField] Rigidbody2D rb;
    private void OnCollisionEnter2D(Collision2D other)
    {
            if ((other.gameObject.tag != "Player"))
            //for Player we have different code that sits on Player Movement Run
            {
            //NullReferenceExeption? Rigidbody2D
             rb = other.gameObject.GetComponent<Rigidbody2D>();
                rb.AddForce(Vector2.right * convyerSpeed);
            }
    }

}


