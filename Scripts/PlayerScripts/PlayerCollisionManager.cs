using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    PlayerGetDamage Damage;
    PushAttackCalculator PushCalculator;
    PlayerData Data;
    PlayerSpawnProjectilles spawnerProjectilles;
    Health playerHealth;
    PlayerMovement2D Movement;

    GameManager gameManager;
    AudioManager audioManager;
    Mana playerMana;
    NarrativeManager narrativeManager;

    public bool isStandingOnEnemy = false;

    void Awake()
    {
        Damage = GetComponent<PlayerGetDamage>();
        Data = GetComponent<PlayerData>();
        spawnerProjectilles = GetComponent<PlayerSpawnProjectilles>();
        PushCalculator = GetComponent<PushAttackCalculator>();
        playerHealth = GetComponent<Health>();
        Movement = GetComponent<PlayerMovement2D>();
        gameManager = FindObjectOfType<GameManager>();
        playerMana = GetComponent<Mana>();
        audioManager = FindObjectOfType<AudioManager>();
        narrativeManager = FindObjectOfType<NarrativeManager>();
    }

    IEnumerator PlayVoiceAttackSoundRoutine()
    {
        audioManager.PlayVoiceAttackSound();
        yield return new WaitForSeconds(1f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Enemy") || (other.gameObject.tag == "FloatingEnemy"))
        {
            if (other.collider.GetType() == typeof(BoxCollider2D))
            {
                EnemyData enemyData;
                enemyData = other.gameObject.GetComponent<EnemyData>();
                CollectDataForPushAttack(enemyData);

                if (!spawnerProjectilles.isKickAttack)
                {
                    Damage.isPushedInAttack = true;
                }

                //playercontroller.IsBouncy(false);
            }
            else if (other.collider.GetType() == typeof(CapsuleCollider2D))  //player attacks  enemy
            {
                Data.SetPlayerAttackPoints(10);
                isStandingOnEnemy = true;
                EnemyGetDamage enemyGetDamage = other.gameObject.GetComponent<EnemyGetDamage>();
                enemyGetDamage.EnemyGetsDamageOnHeadAttack();
                StartCoroutine(PlayVoiceAttackSoundRoutine());

                //playercontroller.IsBouncy(true);
            }
        }

        if ((other.gameObject.tag == "Platform"))
        {
            if (other.gameObject != null)
            {
                ConveyerPushPower conveyerPush = other.gameObject.GetComponent<ConveyerPushPower>();
                if (conveyerPush != null)
                {
                    Movement.convyerDir = conveyerPush.convyerDirection;
                }
            }
        }

        if ((other.gameObject.tag == "FallingCube"))
        {
            if (other.gameObject != null)
            {
                CubeGravity cube = other.gameObject.GetComponent<CubeGravity>();
                cube.isCubeFalling = true;
            }
        }
    }

    void TriggerNextLevel(Collider2D other, int _gamelevel) //gameManager.gameLevel
    {
        if (other.gameObject.tag == "Finish")
        {
            gameManager.isLevelFinished = true;
            Movement.isFreezeMovementControl = true;

            if ((_gamelevel == 0))
            {
                StartCoroutine(gameManager.LoadNextLevelRoutine(4f));
            }

            if ((_gamelevel == 1))
            {
                StartCoroutine(gameManager.LoadNextLevelRoutine(4f));
            }

            if (_gamelevel == 2)
            {
                StartCoroutine(gameManager.LoadNextLevelRoutine(0.5f));
            }

            if (_gamelevel == 3)
            {
                StartCoroutine(gameManager.LoadNextLevelRoutine(4f));
            }

        }
    }

    void TriggerShowNavigationText(Collider2D other)
    {
        if (other.gameObject.tag == "Navigation_Move")
        {
            StartCoroutine(gameManager.ShowNavigationText("Press ARROWS or WASD to move", 0.5f, 6f));

            //  if (Movement.CheckPlayerHorizontalMovement())
            other.gameObject.SetActive(false);

        }

        if (other.gameObject.tag == "Navigation_Jump")
        {
            StartCoroutine(gameManager.ShowNavigationText("Press SPACE to jump", 0f, 6f));
            audioManager.PlayTextSound();
            //   if (Input.GetButtonUp("Jump"))
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Navigation_Spell")
        {
            StartCoroutine(gameManager.ShowNavigationText("Trick the enemy!\n \n Press [O] to cast your shadow", 0f, 8f));
            audioManager.PlayTextSound();
            other.gameObject.SetActive(false);
        }


        if (other.gameObject.tag == "Navigation_Kick")
        {
            StartCoroutine(gameManager.ShowNavigationText("Press [I] to fight", 0f, 4f));
            audioManager.PlayTextSound();
            other.gameObject.SetActive(false);
        }

        //if (other.gameObject.tag == "Navigation_Fire")
        //{
        //    StartCoroutine(gameManager.ShowNavigationText("You got new skill! \n \nPress [P] to cast a fireball", 0f, 10f));
        //    audioManager.PlayTextSound();
        //    other.gameObject.SetActive(false);
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TriggerShowNavigationText(other);
        TriggerNextLevel(other, gameManager.gameLevel);

        if (other.gameObject.tag == "PowerUp")
        {
            bool isPicked = false;

            if (!isPicked)
            {
                if (playerMana.GetManaPoints() < playerMana.maxManaPoints)
                {
                    isPicked = true;
                    playerMana.RecoverManaPoints(25);
                    float delay = 0.3f;
                    Animator powerUpAnimator = other.gameObject.GetComponent<Animator>();
                    powerUpAnimator.SetBool("isCollected", true);
                    audioManager.PlayPickUpSound();
                    Destroy(other.gameObject, delay);
                }

                else
                {
                        isPicked = true;
                        playerMana.RecoverManaPoints(0);
                        float delay = 0.3f;
                        Animator powerUpAnimator = other.gameObject.GetComponent<Animator>();
                        powerUpAnimator.SetBool("isCollected", true);
                        audioManager.PlayPickUpSound();
                        Destroy(other.gameObject, delay);
                }

            }

        }

        if (other.gameObject.tag == "Potion")
        {
            if (playerHealth.GetHealthPoints() < playerHealth.maxHealthPoints)
            {
                PotionBurst _burst = other.gameObject.GetComponent<PotionBurst>();
                _burst.PlayBurst();
                playerHealth.RecoverHealthPoints(25);
                audioManager.PlayPotionPickUpSound();
            }

            else
            {
                PotionBurst _burst = other.gameObject.GetComponent<PotionBurst>();
                _burst.PlayBurst();
                playerHealth.RecoverHealthPoints(0);
                audioManager.PlayPotionPickUpSound();
            }
        }

        if (other.gameObject.tag == "Coin")
        {
            audioManager.PlayCoinsCollisionSound();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Shooting_Skill")
        {
            audioManager.PlayPickUpSound();
            spawnerProjectilles.hasShootingSkill = true;
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("Wait, what is it?... Can I cast something powerful? ", 0.1f));
            StartCoroutine(narrativeManager.TurnMonologuePanelRoutine(false, 5f));
            Destroy(other.gameObject);
            StartCoroutine(gameManager.ShowNavigationText("You got new skill! \n \nPress [P] to cast a fireball", 0f, 10f));
            audioManager.PlayTextSound();
        }

        //if (other.gameObject.tag == "Water")
        //{ 
        //    audioManager.PlayWaterSplashSound(); 
        //}
    }


    void CollectDataForPushAttack(EnemyData enemyData)
    {
        PushCalculator.SetVectorDirection(enemyData.EnemyLookDirectionOnX);
        Data.SetPlayerDamagePoints(enemyData.enemyAttackPoints);     
        PushCalculator.SetPushStrength(enemyData.pushAttackStrength);
    }

}
