using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoggo : MonoBehaviour
{
    bool isDoggoRunning = false;

    GameManager gameManager;
    AudioManager audioManager;

    [SerializeField] GameObject objectToFollow;
    GameObject player;

    Vector2 _lookDirection;
    Rigidbody2D rigidBody;
    Animator animator;
    PlayerData playerData;

    bool isIntroAnimation = false;
    bool isFinalReunion = false;
    bool isSpriteFacingRight = true;
    bool isTimeToMeetPlayer = false;
    bool isPlayerNear = false;

    //Movement
    float speed; // 6f
    float maxSpeed = 15f;
    float horizontalMove;

    void Awake()
    {
        isSpriteFacingRight = true;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        player = GameObject.Find("Player");
        playerData = FindObjectOfType<PlayerData>();
        speed = playerData.moveSpeed;
    }

    void Update()
    {
        MovementLogicAccordingToLevel(gameManager.gameLevel);
        PlayMovementAnimation();
        CheckIfToFlipSprite();
    }

    void FixedUpdate()
    {
        MovementPhysicsLogicAccordingToLevel(gameManager.gameLevel);
    }


    #region TriggerCollision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DogDisapearPortal")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "DoggoMoveStopper")
        {
         //   Debug.Log("STOP TIME");
            Destroy(other.gameObject);
            isTimeToMeetPlayer = true;
            isFinalReunion = false;
            isDoggoRunning = false;
        }
    }
    #endregion

    #region Movement_Logic_On_Level

    void MovementLogicAccordingToLevel(int _gameLevel) //Update
    {
        if (_gameLevel == 0)
        {
            if (!isIntroAnimation)
            {
                RunAlongWithPlayerRunningInput();
            }

            if (isIntroAnimation)
            {
                MoveDoggoAI(objectToFollow);
            }
        }

        if (_gameLevel == 3)
        {
            if (isFinalReunion)
            {
                MoveDoggoAI(objectToFollow);
            }

            if (isTimeToMeetPlayer)
            {
                DetectPlayerAndWalkAlong();
            }
        }
    }

    void MovementPhysicsLogicAccordingToLevel(int _gameLevel) //FixedUpdate
    {
        if (gameManager.gameLevel == 0)
        {
            if (!isIntroAnimation)
            {
                RunPhysics();
            }
        }

        if (gameManager.gameLevel == 3)
        {
            if (isTimeToMeetPlayer)
            {
                RunPhysics();
            }
        }
    }

    #endregion

    #region Player_Detector
    bool IsTargetInRange(GameObject _target, float _distance) //objectToFollow
    {
        Vector2 _distancetoTarget = _target.transform.position - rigidBody.transform.position;
        return _distancetoTarget.magnitude < _distance;
    }

    bool IsTargetOutOfRange(GameObject _target, float _distance) //objectToFollow
    {
        Vector2 _distancetoTarget = _target.transform.position - rigidBody.transform.position;
        return _distancetoTarget.magnitude > _distance;
    }

    void DetectPlayerAndWalkAlong()
    {
        float maxDistanceToPlayer = 2f;
        float minDistanceToPlayer = 0.2f;

        //float maxDistanceToChasePlayer = 6f;
        //float minDistanceToChasePlayer = maxDistanceToPlayer;
        //bool hasMetPlayer = false;

        if (IsTargetInRange(player, maxDistanceToPlayer) && IsTargetOutOfRange(player, minDistanceToPlayer))
        {
           // hasMetPlayer = true;
            speed = playerData.moveSpeed * 1.2f;
           // Debug.Log("PLayer is in range of doogo."); 
            RunAlongWithPlayerRunningInput();
            isDoggoRunning = false;
            isPlayerNear = true;  //isPlayerNear controls animation type
        }

        //else if ((!hasMetPlayer) && (IsTargetInRange(player, maxDistanceToChasePlayer) && IsTargetOutOfRange(player, minDistanceToChasePlayer)) )   //  DashTowardsPlayer
        //{
        //    speed = playerData.moveSpeed * 2f;
        //    Vector2 vectorToPlayer = player.transform.position - rigidBody.transform.position; ;
        //    rigidBody.velocity = new Vector2(vectorToPlayer.x * speed*Time.deltaTime*100, rigidBody.velocity.y);            
        //    isDoggoRunning = true; //isPlayerNear controls animation type
        //    hasMetPlayer = true;
        //}

        else
        {
            speed = 0;
            isPlayerNear = false;
          //  isDoggoRunning = false;
        }
    }

    #endregion

    #region Movement
    void RunAlongWithPlayerRunningInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
    }

    void RunPhysics()
    {
        rigidBody.velocity = new Vector2(horizontalMove * speed, rigidBody.velocity.y);
    }

    void MoveDoggoAI(GameObject _target)
    {
        _lookDirection = _target.transform.position - rigidBody.transform.position;
        transform.Translate(_lookDirection.normalized * speed * Time.deltaTime);
    }

    bool CheckHorizontalMovement()
    {
        return Mathf.Abs(horizontalMove) > Mathf.Epsilon;
    }

    #endregion

    #region Animation
    void PlayMovementAnimation()
    {
        if (CheckHorizontalMovement())
        {
            if (gameManager.gameLevel == 0)
            {
                if (!isDoggoRunning)
                {
                    animator.SetBool("IsWalking", true);
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsSleeping", false);
                }

                else if (!isDoggoRunning)
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsIdle", true);
                    animator.SetBool("IsSleeping", false);
                }
            }


            if (gameManager.gameLevel == 3)
            {
                if (!isDoggoRunning && isPlayerNear)
                {
                    animator.SetBool("IsWalking", true);
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsSleeping", false);
                }

                else if (!isDoggoRunning && !isPlayerNear)
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsIdle", true);
                    animator.SetBool("IsSleeping", false);
                }
            }


            else if (isDoggoRunning)
            {
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsSleeping", false);
            }
        }

        else if (!CheckHorizontalMovement())
        {
            if (!isDoggoRunning)
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsIdle", true);
                animator.SetBool("IsSleeping", false);

                if (!isFinalReunion && gameManager.gameLevel == 3 && !isTimeToMeetPlayer)
                {
                    animator.SetBool("IsSleeping", true);
                }
            }

            else if (isDoggoRunning)
            {
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsSleeping", false);
            }
        }
    }

    #endregion

    #region Narrative_Events
    public void TriggerRunAwayStoryIntroLevel()
    {
        speed = maxSpeed;
        audioManager.PlayDoggoBarkSound();
        isIntroAnimation = true;
        isDoggoRunning = true;
        audioManager.PlayDoggoBarkSound();
    }

    public void TriggerFinalReunionStoryThreeLevel()
    {
        speed = maxSpeed; //5
        audioManager.PlayDoggoBarkSound();
        isDoggoRunning = true;
        audioManager.PlayDoggoBarkSound();
        isFinalReunion = true;
    }

    public void FindClosestCubeAndDestroyIt()
    {
        GameObject[] cubes;
        cubes = GameObject.FindGameObjectsWithTag("DoggoStopperCube");

        foreach (GameObject cube in cubes)
        {
            CubeGravity cubeGravity = cube.GetComponent<CubeGravity>();
            cubeGravity.isDoggoFree = true;
        }

        if (cubes == null) //NullReferenceException: Object reference not set to an instance of an object
                           //   MoveDoggo.FinClosestCubeAndDropIt()(at Assets / Scripts / MoveDoggo.cs:207)
                           //GameManager.FreeDoggoIfNeeded(System.Int32 _gamelevel)(at Assets / Scripts / GameManager.cs:236)
                           //GameManager.Update()(at Assets / Scripts / GameManager.cs:104)


        { return; }
    }

    #endregion


    #region FLIP_Sprite

    void CheckIfToFlipSprite()
    {
        if (!isIntroAnimation || !isFinalReunion)
        {
            if ((horizontalMove > 0 && !isSpriteFacingRight) || (horizontalMove < 0 && isSpriteFacingRight))
            {
                FlipSprite();
            }
        }

        if (isIntroAnimation || isFinalReunion)
        {
            Vector3 _localScale = transform.localScale;
            _localScale.x = 1;
            transform.localScale = _localScale;
        }
    }

    void FlipSprite()
    {
        isSpriteFacingRight = !isSpriteFacingRight;
        Vector3 _localScale = transform.localScale;
        _localScale.x *= -1;
        transform.localScale = _localScale;
    }


    public bool IsFacingRight()
    { return isSpriteFacingRight; }

    #endregion


}

