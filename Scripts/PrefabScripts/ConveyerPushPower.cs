using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerPushPower : MonoBehaviour
{
    public float convyerSpeed = 5000f;
    [SerializeField] Rigidbody2D rb;
    public Vector2 convyerDirection;

    private void Awake()
    {
        convyerDirection = CreateVectorDirection();
    }

    public Vector2 CreateVectorDirection()
    {
        Vector2 _convyerDirection;
        if (Random.Range(0, 2) == 0)
        {
            _convyerDirection = Vector2.right;
        }
        else
            _convyerDirection = Vector2.left;

        return _convyerDirection;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Enemy"))
        //for Player we have different code that sits on Player Movement Run
        {
            //NullReferenceExeption? Rigidbody2D
            rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(convyerDirection * convyerSpeed);
        }
    }

}
