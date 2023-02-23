using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGravity : MonoBehaviour
{
    public bool isCubeFalling = false;
    public bool isDoggoFree= false;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
       // rb.gravityScale = 0;
    }

    void Update()
    {
        FallIfNeeded();
    }

    void FallIfNeeded()
    {
        float _delay = 1f;
        float _gravityScale = 4f;

        if (isCubeFalling)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = _gravityScale;
            Destroy(gameObject, _delay);
        }

        if (isDoggoFree)
        {
            Destroy(gameObject);
        }
    }
}
