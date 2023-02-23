using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Animator animator;
    EnemyData Data;
    EnemyGetDamage enemyDamage;

    GameManager gameManager;
    [SerializeField] GameObject objectToFollow;
    GameObject player;
    PlayerSpawnProjectilles shootProjectilles;

    [SerializeField] GameObject healthBar;

    Vector2 _lookDirection;
    public bool hasAttacked = false;
    public bool isSleeping = true;


    void Awake()
    {
        enemyDamage = GetComponent<EnemyGetDamage>();
        rigidBody = GetComponent<Rigidbody2D>();
        Data = GetComponent<EnemyData>();
        animator = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManager>();

        player = GameObject.Find("Player");

        if (player != null)
        {
            
            shootProjectilles = player.GetComponent<PlayerSpawnProjectilles>();
        }
    }

    void Start()
    {
        hasAttacked = false;
        SleepMode();
    }

    void Update()
    {
        if (gameManager.isGameOver || enemyDamage.isFreezeEnemyMovement)
        {
            SleepMode();
            SetEnemyAnimation(isSleeping);
            return;
        }

        SetEnemyAnimation(isSleeping);

    }

    void FixedUpdate() //Update?
    {
        if (gameManager.isGameOver || enemyDamage.isFreezeEnemyMovement)
        {
            SleepMode();
            SetEnemyAnimation(isSleeping);
            return;
        }

        DetectTarget();
        AwakeWhenTargetInRangeOrSleepIfNot();

        if (!isSleeping && !hasAttacked) //hasAttacked gives extra mini pause betwwen attacks
        {
            MoveEnemyAI(objectToFollow);
        }
    }

    #region SLEEP_AWAKE

    void AwakeWhenTargetInRangeOrSleepIfNot()
    {
        if (IsInAttackRange(objectToFollow, Data.attackRangeDistance) && !enemyDamage.gotDamage && isSleeping && !enemyDamage.isFreezeEnemyMovement)
        { AwakeMode(); }

        else if (!IsInAttackRange(objectToFollow, Data.attackRangeDistance) && !isSleeping)
        { SleepMode(); }
    }
    public void SleepMode() //start position
    {
        isSleeping = true;
    }

    public void AwakeMode() //start position
    {
        isSleeping = false;
    }


    void SetEnemyAnimation(bool _isSleeping)
    {
        if (_isSleeping || hasAttacked)
        {
            animator.SetBool("isEnemyWalking", false);
        }
        else if (!_isSleeping)
        { animator.SetBool("isEnemyWalking", true); }

    }

    public IEnumerator FallAsleepAfterDelayRoutine(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        SleepMode();
        hasAttacked = false;
    }


    #endregion

    #region MOVEMENT

    void MoveEnemyAI(GameObject _target)
    {
        _lookDirection = _target.transform.position - rigidBody.transform.position;
        transform.Translate(_lookDirection.normalized * Data.speed * Time.deltaTime);
        FlipSprite(-_lookDirection.normalized.x);
    }

    public float GetLookDirectionValue()
    {
        return _lookDirection.normalized.x;
    }
    bool CheckPlayerHorizontalMovement(float _vectorValue)
    {
        return Mathf.Abs(_vectorValue) > Mathf.Epsilon;
    }


    #endregion

    #region DETECT_TARGET

    void DetectTarget()
    {
        if (shootProjectilles.GetShadowsCount() <= 0)
        {
            objectToFollow = player;
        }

        else if (objectToFollow != null)
        {
            objectToFollow = FindClosestShadow();
        }
    }

    public GameObject FindClosestShadow()
    {
        GameObject[] shadowInstances;
        shadowInstances = GameObject.FindGameObjectsWithTag("PlayerProjection");

        GameObject closest = null;
        float distance = Mathf.Infinity; //or within Collision circle 
        Vector3 position = transform.position;

        foreach (GameObject gameObject in shadowInstances)
        {
            Vector3 diff = gameObject.transform.position - position;

            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = gameObject;
                distance = curDistance;
            }
        }
        return closest;
    }


    bool IsInAttackRange(GameObject _target, float _distance) //objectToFollow
    {
        Vector2 _distancetoTarget = _target.transform.position - rigidBody.transform.position;
        return _distancetoTarget.magnitude < _distance;

        //  Vector2 _distancetoTarget = _target.transform.position - rigidBody.transform.position;
        // return _distancetoTarget.x < _distance && _distancetoTarget.x > (-_distance);
    }

    #endregion


    #region SHADDOW_COLLISION
    public void CollideWithShadow(Collider2D other) //IN OnTriggerEnter2D(Collider2D other  if (other.gameObject.tag == "PlayerProjection")
    {
        hasAttacked = true;
        float delay = 1f;
        StartCoroutine(FallAsleepAfterDelayRoutine(delay));
        shootProjectilles.ReduceShadowCount();
        Destroy(other.gameObject);
    //    Debug.Log("_shadowsCount: " + shootProjectilles.GetShadowsCount());
        DetectTarget();
    }
    #endregion


    #region Flip_Sprite
    //public bool IsEnemyMoving()
    //{ return CheckPlayerHorizontalMovement(GetLookDirectionValue()); }


    void FlipSprite(float direction)
    {
        if (CheckPlayerHorizontalMovement(GetLookDirectionValue()))
        {
            transform.localScale = new Vector2(Mathf.Sign(direction), 1f);
            healthBar.transform.localScale = new Vector2(0.01f * Mathf.Sign(direction), 0.01f);
        }
    }

    #endregion

}