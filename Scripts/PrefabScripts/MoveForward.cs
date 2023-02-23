using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    bool isFacingRight = true;
    float speed = 10.0f;

    void Update() 
    {
        MoveBullet();
    }

    public void FlipProjectileSprite(float _direction)
    {
        isFacingRight = _direction > 0;

        //if (_direction > 0)
        //{
        //    isFacingRight = true;
        //}

        //else if (_direction < 0)

        //{
        //    isFacingRight = false;
        //}
    }

    void MoveBullet()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime * (isFacingRight ? 1 : -1));

        //if (isFacingRight)
        //{
        //    transform.Translate(Vector3.right * speed * Time.deltaTime);
        //}
        //else
        //    transform.Translate(-Vector3.right * speed * Time.deltaTime);
    }
}
