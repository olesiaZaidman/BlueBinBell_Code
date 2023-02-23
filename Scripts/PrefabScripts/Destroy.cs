using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
   public float lifetime = 2.0f;

    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
