using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetDamage : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _enemyRb;
    CapsuleCollider2D headCollider;
    BoxCollider2D bodyCollider;
    ItemDrop itemDrop;
    EnemyData Data;
    EnemyAttack enemyAttack;

    Health enemyHealth;
    GameObject player;
    PlayerData playerData;
    PlayerGetDamage playerDamage;
    GameManager gameManager;
    //audioManager.PlayMonsterHurtSound
    PushAttackCalculator pushCalculator;
    AudioManager audioManager;

    public bool gotDamage = false;  //hasBeenDamaged
    public bool isPushedInAttackEnemy = false;
    public bool isFreezeEnemyMovement = false;
    public bool isDead = false;
    [SerializeField] ParticleSystem dashHeadAttackParticle;
    bool isWaterSplash = false;

    SpriteRenderer spriteRenderer;


    void Awake()
    {
        player = GameObject.Find("Player");
        Data = GetComponent<EnemyData>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<Health>();
         enemyAttack = GetComponent<EnemyAttack>();
        itemDrop = GetComponent<ItemDrop>();
        headCollider = GetComponent<CapsuleCollider2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        _enemyRb = GetComponent<Rigidbody2D>();
        pushCalculator = GetComponent<PushAttackCalculator>();
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();

        animator = GetComponent<Animator>();

        enemyHealth.SetHealthPoints(enemyHealth.maxHealthPoints);
        //  enemyHealth.SetMaxHealthPoints(Data.maxHealthPoints);
     //   Debug.Log("Enemy max health: " + enemyHealth.maxHealthPoints + " " + "Enemy cur health: " + enemyHealth.GetHealthPoints());

        if (player != null)
        {
            playerData = player.GetComponent<PlayerData>();
            playerDamage = player.GetComponent<PlayerGetDamage>();
        }
    }

    void Start()
    {
        isFreezeEnemyMovement = false;
        gotDamage = false;
        isPushedInAttackEnemy = false;
        isWaterSplash = false;
    }


    void Update()
    {
        if (IsDead() || IsHazards())
        {
            playerDamage.isInvincible = true;
            DestroyEnemy();
        }
        if (IsWater())
        {
            DieInWater();
        }
    }

    void FixedUpdate()
    {
        //  if (isFreezeEnemyMovement)
        //   { return; }

        EnemyRecievePushPhysics(_enemyRb);
    }

    #region DAMAGE
    public void EnemyGetsDamage(int _damage)
    {
        if (!gotDamage)
        {
           // CameraShake cameraShakeEffect = FindObjectOfType<CameraShake>();
         //   cameraShakeEffect.Shake(1f,5f);
            audioManager.PlayMonsterHurtSound();
            playerDamage.isInvincible = true;
            gotDamage = true;
            float hurtAnimInterval = 1f; // 0.7f;
            float getDamageCoolDownTime = 1.3f;
            PlayEnemyHurtAnimation();
            enemyHealth.ReduceHealth(_damage);
            StartCoroutine(StopEnemyHurtAnimRoutine(hurtAnimInterval));
        //    Debug.Log("Enemy health after damage: " + enemyHealth.GetHealthPoints());
            StartCoroutine(GetDamageCoolDown(getDamageCoolDownTime));
        }
    }

    public IEnumerator GetDamageCoolDown(float _getDamageCoolDownTime)
    {
        yield return new WaitForSeconds(_getDamageCoolDownTime);
        gotDamage = false;
        playerDamage.isInvincible = false;
    }
    #endregion


    #region Head_Damage
    public void EnemyGetsDamageOnHeadAttack() //Add BOUNCY?
    {
        dashHeadAttackParticle.Play();
        EnemyGetsDamage(playerData.AttackPoints);
    }

    #endregion

    #region Shadow_Freeze
    public void EnemyGetsShadowFreeze() //Add BOUNCY?
    {
        audioManager.PlayFrostSpellSound();
       // Color freezeColor;         //sprite color 0033FF
        spriteRenderer.color = Color.blue;
        isFreezeEnemyMovement = true;
      //  animator.SetBool("isEnemyWalking", false);
        StartCoroutine(ShadowFreezeCoolDownRoutine(3f));
    }

    public IEnumerator ShadowFreezeCoolDownRoutine(float _getDamageCoolDownTime)
    {
        yield return new WaitForSeconds(_getDamageCoolDownTime);
        isFreezeEnemyMovement = false;
        //animator.SetBool("isEnemyWalking", true);        //  enemyAttack.AwakeMode();
        spriteRenderer.color = Color.white;
    }



    #endregion

    #region Push_Damage

    void EnemyRecievePushPhysics(Rigidbody2D _rigidBody)
    {
        float getPushedCoolDownTime = 0.3f;

        if (isPushedInAttackEnemy) // is set in OnTriggerEnter2D
        {
          //  Debug.Log("EnemyRecievePushPhysics!");
            //{Work with push phiscis for enemy- maybe gravity off? for now it is floating}:
            pushCalculator.GetHorizontalDamagePushPhysics(_rigidBody);  //or pushCalculator.GetVerticalDamagePushPhysics(_rigidBody);
            EnemyGetsDamage(playerData.AttackPoints);
            StartCoroutine(GetPushedCoolDown(getPushedCoolDownTime));
        }
        else return;
    }

    private IEnumerator GetPushedCoolDown(float _getPuchedCoolDownTime)
    {
        isFreezeEnemyMovement = true;
        yield return new WaitForSeconds(_getPuchedCoolDownTime);
        isPushedInAttackEnemy = false;
        isFreezeEnemyMovement = false;
    }
    #endregion

    #region Hurt_Animation

    void PlayEnemyHurtAnimation()
    {
        animator.SetBool("isEnemyHurt", true);
    }

    IEnumerator StopEnemyHurtAnimRoutine(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        animator.SetBool("isEnemyHurt", false);
    }

    #endregion

    #region Die
    void DestroyEnemy()
    {
        bool isDroped = false;
        float destroyDelay = 1f;

        if (!isDead) //IsGameOverInWater() ||
        {
            isDead = true;
            audioManager.PlayMonsterDeadSound();
            transform.localScale = new Vector2(1f, -1f);
            headCollider.isTrigger = true;
            animator.SetBool("isEnemyDead", true);

            if (!isDroped)
            {
                itemDrop.DropItems();
                isDroped = true;
            }

            Destroy(gameObject, destroyDelay);
            playerDamage.isInvincible = false;

            if (gameManager.gameLevel == 3)
            {
                gameManager.FreeDoggo();
            }
        }
    }

    bool IsDead()
    {
        return enemyHealth.GetHealthPoints() <= 0;
    }

    void DieInWater()
    {
        if (!isWaterSplash)
        {
            isWaterSplash = true;
            audioManager.PlayWaterSplashSound();
        }

        float destroyDelay = 1.3f;
        transform.localScale = new Vector2(1f, -1f);
        animator.SetBool("isEnemyDead", true);
        Destroy(gameObject, destroyDelay);
    }


    //bool IsGameOverInWaterHazrds()
    //{ return headCollider.IsTouchingLayers(LayerMask.GetMask("Water")) || bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")); }

    public bool IsHazards()
    {
        return bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazard"));
    }

    public bool IsWater()
    {
        return headCollider.IsTouchingLayers(LayerMask.GetMask("Water"));
    }
     
    #endregion

}
