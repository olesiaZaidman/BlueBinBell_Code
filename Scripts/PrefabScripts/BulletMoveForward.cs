using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveForward : MonoBehaviour
{
    bool isFacingRight = true;
    [Range(0f, 20f)] public float speed;

    void Update()
    {
        MoveBullet();
    }

    public void FlipProjectile(float _direction)
    {
        isFacingRight = _direction > 0;
    }

    void MoveBullet()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime * (isFacingRight ? 1 : -1));
    }
}
