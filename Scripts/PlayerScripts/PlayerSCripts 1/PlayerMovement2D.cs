using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    PlayerData Data; //or make public & attach in the Ispector
    PlayerCollisionManager _onCollision;
    PlayerGetDamage Damage;

    DustEffect dustEffect;
    GameManager gameManager;
    AudioManager audioManager;
    PushAttackCalculator PushCalculator;

    #region COMPONENTS
    public Rigidbody2D MyRigidBody { get; private set; }
    Animator myAnimator;
    private CircleCollider2D circleCollider;
    #endregion

    #region STATE PARAMETERS
    bool isFacingRight = true;
    #endregion

    #region CHECK PARAMETERS
    [SerializeField] private Transform _groundCheckPoint;
    #endregion

    #region LAYERS & TAGS
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsLadder;
    [SerializeField] private LayerMask whatIsPlatform;
    [SerializeField] private LayerMask whatIsBouncy;
   // [SerializeField] GameObject _startPosition;
    #endregion

    //Run
    float horizontalMove;
    [SerializeField] AudioSource footstepsSound;

    //Climb
    float verticalMove = 0f;
    [SerializeField] AudioSource climbSound;

    //Jump
    bool isJumping;
    bool isJumpingOffLadder;

    //Timers 
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    //Push
    public bool isFreezeMovementControl = false;
    public float getAttackCoolDownTime = 0.5f;

    //PlatformConvyer
    public Vector2 convyerDir;



    public bool IsAttacking()
    { return Input.GetKeyDown(KeyCode.I); }

    void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
        Data = GetComponent<PlayerData>();
        audioManager = FindObjectOfType<AudioManager>();
        PushCalculator = GetComponent<PushAttackCalculator>();
        _onCollision = GetComponent<PlayerCollisionManager>();
        dustEffect = FindObjectOfType<DustEffect>();
        myAnimator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        Damage = GetComponent<PlayerGetDamage>();
    }



    void Start()
    {
        SetGravityScale(Data.gravityScale);
        isFacingRight = true;
        isJumpingOffLadder = false;
        isFreezeMovementControl = false;
    }

    void Update()
    {
        if (isFreezeMovementControl && gameManager.isLevelFinished)
        {
            StopMovement();
        }

        PlayOrPauseFootstepsAudio();
        RunInput();

        if (gameManager.isGameOver || isFreezeMovementControl || !gameManager.isGameReadyToStart)
        { return; }

        PlayOrPauseClimbingAudio();
        SetCaoyteTime();
        SetJumpBufferTime();
        JumpInput();
        JumpAnimation();
        IsJumpingOffLadder();
        ClimbInput();
        CheckIfToFlipSprite();
    }

    void FixedUpdate()
    {
        if (gameManager.gameLevel >= 1 && gameManager.gameLevel < 3)
        {
           PlayIntroMovement();
        } 

       
        if (gameManager.isGameOver || isFreezeMovementControl || !gameManager.isGameReadyToStart)
        { return; }

        RunPhysicsOnInput();
        ClimbPhysics();
        RecievePushPhysics(MyRigidBody);
        PushOnConveyer(MyRigidBody);

    }

    #region  Audio_Movement_Effects
    void PlayOrPauseFootstepsAudio()
    {

        if (CheckPlayerHorizontalMovement() && IsGrounded())
        {
            footstepsSound.enabled = true;
        }
        else
        { footstepsSound.enabled = false; }
    }

    void PlayOrPauseClimbingAudio()
    {
        if (CheckPlayerVerticalMovement() && IsLadder())
        {
            climbSound.enabled = true;
        }
        else
        { climbSound.enabled = false; }
    }
    #endregion

    #region  StartingAnimation
    void PlayIntroMovement()
    {
        if (IsGrounded() && !gameManager.isGameReadyToStart)
        {
            float timeForIntroMovement = 0.7f;
            //    Vector2 playerPoint = Vector2.MoveTowards(transform.position, _startPosition.transform.position, speed * Time.fixedDeltaTime);//  * Time.deltaTime         
            //   MyRigidBody.MovePosition(playerPoint);

            MyRigidBody.velocity = new Vector2(Data.moveSpeed, 0); 
            StartCoroutine(EndIntroMovementRoutine(timeForIntroMovement));
        }
    }


    private IEnumerator EndIntroMovementRoutine(float _time)
    {
        yield return new WaitForSeconds(_time);
        gameManager.isGameReadyToStart = true;
      //  myAnimator.SetBool("isRunning", false);
    }

    #endregion

    public void SetGravityScale(float _scale)
    {
        MyRigidBody.gravityScale = _scale;
    }

    #region  PlatformConvyer

    void PushOnConveyer(Rigidbody2D _myRb)
    {
        float convyerSpeedModifier = 500f;
        if (IsPlatform())
        {
            _myRb.AddForce(convyerDir * convyerSpeedModifier); //, ForceMode2D.Impulse

        }
    }

    #endregion

    #region JUMP_Timers

    void SetCaoyteTime()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    void SetJumpBufferTime()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }
    #endregion

    #region JUMP
    void JumpInput()
    {
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            dustEffect.CreateDustEffect();
            audioManager.PlayJumpSound();
            MyRigidBody.velocity = new Vector2(MyRigidBody.velocity.x, Data.jumpingPower);
            jumpBufferCounter = 0f;
            StartCoroutine(JumpCooldown());
        }
        if (Input.GetButtonUp("Jump") && MyRigidBody.velocity.y > 0f)
        {
            dustEffect.CreateDustEffect();
            // audioManager.PlayJumpSound();
            MyRigidBody.velocity = new Vector2(MyRigidBody.velocity.x, MyRigidBody.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
            SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
            StartCoroutine(JumpCooldown());
        }

        if (IsLadder() && jumpBufferCounter > 0f) // && !isJumping
        {
            isJumpingOffLadder = true;
            audioManager.PlayJumpSound();
            SetGravityScale(Data.gravityScale);
            MyRigidBody.velocity += new Vector2(1f * Data.moveSpeed, Data.jumpingPower * 1f);
            jumpBufferCounter = 0f;
            StartCoroutine(JumpingOffLadderRoutine());
        }

        if (_onCollision.isStandingOnEnemy && jumpBufferCounter > 0f)
        {
            MyRigidBody.velocity = new Vector2(MyRigidBody.velocity.x, Data.jumpingPower);
            jumpBufferCounter = 0f;
            StartCoroutine(JumpCooldown());
            _onCollision.isStandingOnEnemy = false;
        }

      //  if (IsBouncy())
        //{ audioManager.PlayJumpSound(); }
    }

    void JumpAnimation()
    {
        if (!IsGrounded())
        {
            myAnimator.SetBool("isJumping", true);
        }

        if (!_onCollision.isStandingOnEnemy && isJumping)
        {
            myAnimator.SetBool("isJumping", true);
        }

        if (!IsGrounded() && _onCollision.isStandingOnEnemy)
        {
            myAnimator.SetBool("isJumping", false);
        }

        else if (IsGrounded())
        {
            _onCollision.isStandingOnEnemy = false;
            myAnimator.SetBool("isJumping", false);

            //  myAnimator.SetBool("isClimbing", false); //we can add it just to be on safe side
            //  myAnimator.SetBool("isClimbIdle", false);//we can add it just to be on safe side
        }
    }

    void IsJumpingOffLadder()
    {
        if (IsGrounded() || !IsLadder())
        { isJumpingOffLadder = false; }
    }

    IEnumerator JumpingOffLadderRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        isJumpingOffLadder = false;
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }


    #endregion

    #region RUN

    void RunInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        PlayOrPauseRunningAnimation();
    }

    void PlayOrPauseRunningAnimation()
    {
        if (IsGrounded() && CheckPlayerHorizontalMovement() && !Damage.isPushedInAttack) //isGround && Mathf.Abs(horizontalMove) > 0f
        {
            myAnimator.SetBool("isRunning", true);
            return;
        }

        myAnimator.SetBool("isRunning", false);

        //else if (!CheckPlayerHorizontalMovement())
        //{
        //    myAnimator.SetBool("isRunning", false);
        //}
        //else if (Damage.isPushedInAttack)
        //{
        //    myAnimator.SetBool("isRunning", false);
        //}
        //else if (!IsGrounded())
        //{
        //    myAnimator.SetBool("isRunning", false);
        //}

       // if (gameManager.isGameOver || isFreezeMovement)
      //  { myAnimator.SetBool("isRunning", false); }
    }

    void RunPhysicsOnInput()
    {
        //float targetSpeed = horizontalMove * moveSpeed;
        //float speedDif = targetSpeed - myRigidBody.velocity.x;
        //float accelerRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _decceleration;
        //float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelerRate, velPower) * Mathf.Sign(speedDif);
        //myRigidBody.AddForce(movement * Vector2.right);

        MyRigidBody.velocity = new Vector2(horizontalMove * Data.moveSpeed, MyRigidBody.velocity.y);
    }

    void StopMovement()
    {
        MyRigidBody.velocity = new Vector2(0, MyRigidBody.velocity.y);
        Data.moveSpeed = 0f;
       // MyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    #endregion

    #region GET_DAMAGE_PUSH

    void RecievePushPhysics(Rigidbody2D _rigidBody)
    {
        if (Damage.isPushedInAttack)
        {
            PushCalculator.GetHorizontalDamagePushPhysics(_rigidBody);
            Damage.PlayerGetsDamage(Data.DamagePoints);
            StartCoroutine(GetAttackCoolDown());
        }
        else return;
    }

    private IEnumerator GetAttackCoolDown()
    {
        isFreezeMovementControl = true;
        yield return new WaitForSeconds(getAttackCoolDownTime);
        Damage.isPushedInAttack = false;
        isFreezeMovementControl = false;
    }
    #endregion

    #region CLIMB
    void ClimbInput()
    {
        verticalMove = Input.GetAxisRaw("Vertical");
    }
    void ClimbPhysics()
    {
        if (IsLadder() && !isJumpingOffLadder)
        {
            // SetGravityScale(Data.zeroGravity); 
            MyRigidBody.gravityScale = 0f;
            MyRigidBody.velocity = new Vector2(MyRigidBody.velocity.x, verticalMove * Data.climbSpeed);
            myAnimator.SetBool("isClimbIdle", true);

            if (CheckPlayerVerticalMovement())
            {
                myAnimator.SetBool("isClimbing", true);
                myAnimator.SetBool("isClimbIdle", false);
            }
        }
        else if (!IsLadder())
        {
            //  SetGravityScale(Data.gravityScale); 
            MyRigidBody.gravityScale = 4f;
            myAnimator.SetBool("isClimbing", false);
            myAnimator.SetBool("isClimbIdle", false);

        }
        else
            return;
    }
    #endregion

    #region LayerMaskDetection

    public bool IsGrounded()
    {
        float _groundedOverlapRadius = 0.2f;
        return Physics2D.OverlapCircle(_groundCheckPoint.position, _groundedOverlapRadius, whatIsGround); //OverlapCircleAll()?
    }

    bool IsPlatform()
    {
        float _groundedOverlapRadius = 0.2f;
        return Physics2D.OverlapCircle(_groundCheckPoint.position, _groundedOverlapRadius, whatIsPlatform); //OverlapCircleAll()?
    }

    bool IsLadder() //check for ladder
    {
        float _radius = 0.25f;
        float _distance = 0.01f;
        RaycastHit2D Hitinfo = Physics2D.CircleCast(circleCollider.bounds.center, _radius, Vector2.down, _distance, whatIsLadder);
        return Hitinfo.collider != null;
    }

    bool IsBouncy()
    {
        float _groundedOverlapRadius = 0.2f;
        return Physics2D.OverlapCircle(_groundCheckPoint.position, _groundedOverlapRadius, whatIsBouncy); //OverlapCircleAll()?
    }

    #endregion

    #region Move_Directions
    bool CheckPlayerVerticalMovement()
    {
        return Mathf.Abs(verticalMove) > Mathf.Epsilon;
    }

    public bool CheckPlayerHorizontalMovement()
    {
        float customEpsilon = 0.1f;
      //  Debug.Log(MyRigidBody.velocity.x);
        return Mathf.Abs(MyRigidBody.velocity.x) > customEpsilon;//we use 0.1 instead of Mathf.Epsilon -  a very small amount
                                                       //coz the MyRigidBody is moving slightly thanks to gravity what we see in Debugger
        //return Mathf.Abs(horizontalMove) > Mathf.Epsilon  //this way it works just fine ecept bugs in StopMovement function
    }

    public bool CheckPlayerHorizontalMovementIncline()
    {
        float customEpsilon = 0.1f;
        //  Debug.Log(MyRigidBody.velocity.x);
        return Mathf.Abs(horizontalMove) > customEpsilon;//we use 0.1 instead of Mathf.Epsilon -  a very small amount
                                                                 //coz the MyRigidBody is moving slightly thanks to gravity what we see in Debugge                                                                 //return Mathf.Abs(horizontalMove) > Mathf.Epsilon  //this way it works just fine ecept bugs in StopMovement function
    }

    #endregion

    #region FLIPSprite

    void CheckIfToFlipSprite()
    {
        if ((horizontalMove > 0 && !isFacingRight) || (horizontalMove < 0 && isFacingRight))
        {
            FlipSprite();
            dustEffect.CreateDustEffect();
        }
    }
    void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector3 _localScale = transform.localScale;
        _localScale.x *= -1;
        transform.localScale = _localScale;
    }

    public bool IsFacingRight()
    { return isFacingRight; }
    #endregion
}
