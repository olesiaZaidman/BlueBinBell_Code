using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipShadow : MonoBehaviour
{
    PlayerController playerController;
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void FlipSprite(GameObject _instance)
    {
        if (playerController.IsMovingLeft())
        {
            _instance.transform.localScale = new Vector2(-1f, 1f);
        }

        if (playerController.IsMovingRight())
        {
            _instance.transform.localScale = new Vector2(1f, 1f);
        }
    }
}
