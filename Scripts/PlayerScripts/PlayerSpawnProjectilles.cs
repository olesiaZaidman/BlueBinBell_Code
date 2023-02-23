using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnProjectilles : MonoBehaviour
{
    Animator myAnimator;
    PlayerMovement2D Movement;
    PlayerGetDamage Damage;
    GameManager gameManager;
    AudioManager audioManager;
    ScoreManager scoreManager;
    Mana playerMana;

    PlayerData Data;

    [Header("Shadow")]
    [SerializeField] GameObject projectileShadowPrefab;
    [SerializeField] GameObject shadowFXPrefab;

    [SerializeField] Transform _positionShadowProjection;
    [SerializeField] Transform _positionShadowFX;

    [Header("Bullets")]
    [SerializeField] GameObject projectileBulletPrefab;
    [SerializeField] Transform _positionBullet;

    [Header("Kick")]
    [SerializeField] GameObject projectileKickPrefab;
    [SerializeField] Transform _positionKick;
    public bool isKickAttack = false;
    public bool hasShootingSkill = false;


    int _shadowsCount = 0;
    bool hasShoot = false;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        audioManager = FindObjectOfType<AudioManager>();
        Data = FindObjectOfType<PlayerData>();
        myAnimator = GetComponent<Animator>();
        Movement = GetComponent<PlayerMovement2D>();
        Damage = GetComponent<PlayerGetDamage>();
        playerMana = GetComponent<Mana>();
    }

    void Update()
    {
        if (gameManager.isGameOver)
        { return; }

        SpawnShadowProjection();
        FireBullets();
        KickAttack(0.2f, 0.8f, 2f);
    }

    #region SPAWN
    GameObject SpawnProjectile(GameObject _projectilePrefab, Transform _position)
    {
        return Instantiate(_projectilePrefab, _position.position, _projectilePrefab.transform.rotation);
    }
    #endregion

    #region SHADOW_COUNT

    public int GetShadowsCount()
    { return _shadowsCount; }

    public int ReduceShadowCount()
    {
        if (_shadowsCount > 0)
        { _shadowsCount--; }
        else _shadowsCount = 0;
        return _shadowsCount;
    }

    int IncrementShadowCount()
    {
        return _shadowsCount++;
    }
    #endregion

    #region SHADOW_SPAWN

    void SpawnShadowProjection()
    {
        float lifetime = 5f;

        if (Input.GetKeyDown(KeyCode.O) && playerMana.GetManaPoints() > 0) //scoreManager.SpellScorePoints > 0
        {
            playerMana.ReduceManaPoints(20); // scoreManager.DecreaseSpellScore();

            audioManager.PlaySpellSound();
            myAnimator.SetBool("IsShadow", true);
            GameObject shadow = SpawnProjectile(projectileShadowPrefab, _positionShadowProjection);
            FlipSprite(shadow, Movement.IsFacingRight());
            GameObject shadowFX = SpawnProjectile(shadowFXPrefab, _positionShadowFX);
            StartCoroutine(StopShadowProjectionRoutine());
            IncrementShadowCount();
            shadow.name = "PlayerProjection" + _shadowsCount;
            Destroy(shadowFX, lifetime);
            StartCoroutine(DestroyShadowInstanceRoutine(shadow));
          //  Debug.Log("_shadowsCount: " + _shadowsCount);
        }
        else
            return;
    }

    IEnumerator DestroyShadowInstanceRoutine(GameObject _gameObject)
    {
        yield return new WaitForSeconds(2f);
        Destroy(_gameObject);
        ReduceShadowCount();
      //  Debug.Log("_shadowsCount: " + _shadowsCount);
    }

    IEnumerator StopShadowProjectionRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        myAnimator.SetBool("IsShadow", false);
    }

    #endregion

    #region BULLETS

    void FireBullets()
    {
        float timeIntervalFire = 0.5f;
        float timeIntervalStopAnim = 1.2f;

        if (hasShootingSkill && Input.GetKeyDown(KeyCode.P) && !hasShoot && playerMana.GetManaPoints() > 0) //scoreManager.SpellScorePoints > 0
        {
            Data.SetPlayerAttackPoints(30);
            playerMana.ReduceManaPoints(20);  //  scoreManager.DecreaseSpellScore();
            Movement.isFreezeMovementControl = true;
            hasShoot = true;
            myAnimator.SetBool("IsFiring", true);
            StartCoroutine(StartFireRoutine(timeIntervalFire));
            StartCoroutine(StopFireRoutine(timeIntervalStopAnim));
        }
        else
            return;
    }

    IEnumerator StartFireRoutine(float _timeInterval)
    {
        float lifetime = 2f;
        yield return new WaitForSeconds(_timeInterval);
        audioManager.PlayShootSound();
        // Debug.Log("Bullet!");
        GameObject bullet = SpawnProjectile(projectileBulletPrefab, _positionBullet);
        FlipSpriteProjectileInstance(bullet);
        Destroy(bullet, lifetime);
    }
    IEnumerator StopFireRoutine(float _timeInterval)
    {

        yield return new WaitForSeconds(_timeInterval); //like Exit time
        myAnimator.SetBool("IsFiring", false);
        hasShoot = false;
        Movement.isFreezeMovementControl = false;
        // Debug.Log("IsFiring, false");
    }
    #endregion

    #region KICK
    void KickAttack(float _timeIntervalSpawnFX, float _timeIntervalStopAnim, float _endFunction)
    {
        if (Input.GetKeyDown(KeyCode.I) && !isKickAttack)
        {
            Data.SetPlayerAttackPoints(10);
            audioManager.PlayVoiceAttackSound();
            audioManager.PlayKickSound();
            Damage.isPushedInAttack = false;
            isKickAttack = true;
            myAnimator.SetBool("isKickAttack", true);
            StartCoroutine(StartKickAttackRoutine(_timeIntervalSpawnFX));
            StartCoroutine(StopKickAttackRoutine(_timeIntervalStopAnim));
            StartCoroutine(AttackCoolDownRoutine(_endFunction));
        }
        else
            return;
    }

    IEnumerator StartKickAttackRoutine(float _timeInterval)
    {
        yield return new WaitForSeconds(_timeInterval);
        GameObject kickEffect = SpawnProjectile(projectileKickPrefab, _positionKick);
        kickEffect.GetComponent<FlippyKick>().FlipImageKick(Mathf.Sign(transform.localScale.x));
    }
    IEnumerator StopKickAttackRoutine(float _timeInterval)
    {
        yield return new WaitForSeconds(_timeInterval); //like Exit time
        myAnimator.SetBool("isKickAttack", false);
        isKickAttack = false;
    }

    IEnumerator AttackCoolDownRoutine(float _timeInterval)
    {
        yield return new WaitForSeconds(_timeInterval);
    }
    #endregion


    #region Flip

    void FlipSprite(GameObject _instance, bool _isFacingRight)
    {
        if (_isFacingRight)
        {
            _instance.transform.localScale = new Vector2(1f, 1f);
        }

        else
            _instance.transform.localScale = new Vector2(-1f, 1f);
    }

    void FlipSpriteProjectileInstance(GameObject _projectileInstance)
    {
        _projectileInstance.GetComponent<BulletMoveForward>().FlipProjectile(Mathf.Sign(transform.localScale.x));
    }
    #endregion

}
