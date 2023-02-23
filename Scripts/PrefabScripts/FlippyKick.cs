using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyKick : MonoBehaviour
{

    public void FlipImageKick(float _direction)
    {
        transform.localScale = new Vector2((-1) * _direction, 1f);
    }
}

