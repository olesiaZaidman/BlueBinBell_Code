using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlayerSprite : MonoBehaviour
{
    DustEffect dustEffect;
    PlayerController playerController;
    GameManager gameManager;
  

   public bool isFacingRight = true;

    void Awake()
    {
        dustEffect = FindObjectOfType<DustEffect>();
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        FlipSprite();
    }
    public void FlipSprite()
    {
        if (gameManager.isGameOver)
        { return; }

        if (playerController.IsMovingLeft())
        {
            transform.localScale = new Vector2(-1f, 1f);
        }

        if (playerController.IsMovingRight())
        {
            transform.localScale = new Vector2(1f, 1f);
        }

        bool changingDirections = (transform.localScale.x > 0 && !isFacingRight) || (transform.localScale.x < 0 && isFacingRight);

        if (changingDirections)
        {
            dustEffect.CreateDustEffect();
            isFacingRight = !isFacingRight;
        }
    }
}
