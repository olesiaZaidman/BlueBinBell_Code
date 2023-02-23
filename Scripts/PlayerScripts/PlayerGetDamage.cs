using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGetDamage : MonoBehaviour
{
    Health playerHealth;
    Animator myAnimator;
    AudioManager audioManager;
    PlayerData Data;
    GameManager gameManager;

    [SerializeField] GameObject bloodCanvas;

    PlayerMovement2D Movement;
    [SerializeField] private LayerMask whatIsHazards;
    [SerializeField] private LayerMask whatIsWater;
    [SerializeField] private Transform groundCheck;

    //PlayWaterSplashSound

    public bool isPushedInAttack = false;
    public bool isInvincible = false;

    int healthPointsAtLevel;
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        Movement = GetComponent<PlayerMovement2D>();
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        Data = GetComponent<PlayerData>();

        GetAndSetHealthPointsForCurrentLevel();
        bloodCanvas.SetActive(false);
    }

    void Start()
    {
        isPushedInAttack = false;
    }

    void Update()
    {
        DieOnHazards();
    }

    void GetAndSetHealthPointsForCurrentLevel()
    {
        if (gameManager.gameLevel < 2)
        {
            playerHealth = GetComponent<Health>();
            playerHealth.SetHealthPoints(playerHealth.maxHealthPoints);
        }

        else
        {
            playerHealth = GetComponent<Health>();
            playerHealth.SetHealthPoints(DataBetweenLevels.currentHealth);
        }
    }



    public void PlayerGetsDamage(int _damage)
    {
        if (!isInvincible)
        {
            // bloodCanvas.SetActive(true);
            PlayHurtAnimation();
            playerHealth.ReduceHealth(_damage);
            audioManager.PlayHurtSound();
            //  Debug.Log("Player Health after damage: " + playerHealth.GetHealthPoints());
            StartCoroutine(StopHurtPlayerAnimationRoutine(Movement.getAttackCoolDownTime));
        }

    }


    public void PlayHurtAnimation()
    {
        myAnimator.SetBool("isHurt", true);
    }

    IEnumerator StopHurtPlayerAnimationRoutine(float _time)
    {
        yield return new WaitForSeconds(_time);
        myAnimator.SetBool("isHurt", false);
        bloodCanvas.SetActive(false);
    }

    void DieOnHazards()
    {
        if (IsHazards() || IsWater())
        {
            playerHealth.SetHealthPoints(0);
            bloodCanvas.SetActive(true);
            //   Debug.Log("DieOnHazards-Player Health after damage: " + playerHealth.GetHealthPoints());
        }
    }

    public bool IsHazards()
    {
        float _groundedOverlapRadius = 0.2f;
        return Physics2D.OverlapCircle(groundCheck.position, _groundedOverlapRadius, whatIsHazards); //OverlapCircleAll()?
    }

    public bool IsWater()
    {
        float _groundedOverlapRadius = 0.2f;
        return Physics2D.OverlapCircle(groundCheck.position, _groundedOverlapRadius, whatIsWater); //OverlapCircleAll()?
    }

}





