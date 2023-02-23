using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*NEXT TASKS:
    - make player climb; 
    -make player spawn Shadow
    -make player Spawn and shoot bullets 
    - make player fight*/

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] float jumpForce = 150f;
 //   [SerializeField] float gravityValue = 2;
    //   [SerializeField] bool isOnGround = false;
    [SerializeField] bool isJumpingOffLadder = false;

    Rigidbody2D myRigidBody;

    DustEffect dustEffect;
    GameManager gameManager;
    LayerMaskStates layerMaskStates;
    AnimationController animationController;

  //  [SerializeField] PhysicsMaterial2D _bouncyMaterial;
  //  [SerializeField] PhysicsMaterial2D _slidyMaterial;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
   //     myFeetCollider = GetComponent<BoxCollider2D>();

        gameManager = FindObjectOfType<GameManager>();
        layerMaskStates = FindObjectOfType<LayerMaskStates>();
        animationController = FindObjectOfType<AnimationController>();
        dustEffect = FindObjectOfType<DustEffect>();
   //     SetPhysicsMaterial(myFeetCollider, _slidyMaterial);
        //   myRigidBody.gravityScale = gravityValue;
    }

    //void SetPhysicsMaterial(Collider2D _collider,float _friction, float _bouncy)
    //{
    //    _collider.sharedMaterial.friction = _friction;
    //    _collider.sharedMaterial.bounciness = _bouncy;
    //}

    void SetPhysicsMaterial(Collider2D _collider, PhysicsMaterial2D _material)
    {
        _collider.sharedMaterial = _material;
    }

    void Update()
    {
        if (gameManager.isGameOver)
        { return; }
        Run();
        Jump();
        Climb();

        if (layerMaskStates.IsStanding() || !layerMaskStates.IsClimbing())
        {
            isJumpingOffLadder = false;
        }
        //  SwitchPhysicsMaterial();
        //   Climb();
    }

    //private void FixedUpdate()
    //{
    //    controller.Move(horizontalMove * Time.fixedDeltaTime, false);
    //}

    //void HitEnemyHead()
    //{
    //    if (layerMaskStates.IsStandingOnEnemy())
    //    {
    //        SetPhysicsMaterial(myFeetCollider, _bouncyMaterial);
    //      //  dustEffect.CreateDustEffect();
    //        //isEnemyHit = true;
    //        //enemy.health -= 20;
    //        Debug.Log("Hit Over Enemy Head");
    //    }
    //}

    void SwitchPhysicsMaterial()
    {
        //if (layerMaskStates.IsStanding())
        //{ 
        //    SetPhysicsMaterial(myFeetCollider, _slidyMaterial);
        //}
        // if (layerMaskStates.IsStandingOnEnemy())
        //{ 
        //    SetPhysicsMaterial(myFeetCollider, _bouncyMaterial); 
        //}
        //else
        //    SetPhysicsMaterial(myFeetCollider, _slidyMaterial);


        //bool _condition;

        //switch (_condition)
        //{
        //    case : /// same as … if( age == 15)
        //   
        //        break;

        //    case layerMaskStates.IsStanding():
        //        SetPhysicsMaterial(myFeetCollider, _slidyMaterial);
        //        break;

        //    default:
        //        SetPhysicsMaterial(myFeetCollider, _slidyMaterial);
        //        break; //default state
        //}


    }


    void Run()
    {
        if (gameManager.isGameOver)
        { return; }

     float horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;
     horizontalInput *= Time.deltaTime;
     transform.Translate(horizontalInput, 0, 0); //we move left/right on x-axis

       // myRigidBody.velocity = new Vector2(horizontalInput, 0);


    }

    public bool IsMovingRight()
    { return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow); }

    public bool IsMovingLeft()
    { return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow); }

    public bool IsStoppedMovingRight()
    { return Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow); }

    public bool IsStoppedMovingLeft()
    { return Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow); }



    void Jump()
    {
        if (gameManager.isGameOver)
        { return; }

        if (Input.GetKeyDown(KeyCode.Space) && layerMaskStates.IsStanding()) // isOnGround || 
        {
            myRigidBody.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            //m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            dustEffect.CreateDustEffect();
            //  isOnGround = false;
        }

        else  if (Input.GetKeyDown(KeyCode.Space) && (layerMaskStates.IsEnemy() || layerMaskStates.isStandingOnEnemy)) // isOnGround || 
        {

            myRigidBody.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            dustEffect.CreateDustEffect();
            layerMaskStates.isStandingOnEnemy = false;
            //  isOnGround = false;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && layerMaskStates.IsClimbing()) // isOnGround || 
        {
            isJumpingOffLadder = true;
            Vector2 jumpLadder = new Vector2(1f * moveSpeed, jumpForce * 1f);
            myRigidBody.isKinematic = false;
            myRigidBody.AddForce(jumpLadder, ForceMode2D.Impulse);
            dustEffect.CreateDustEffect();
            StartCoroutine(JumpingOffLadderRoutine());
        }

        else
            return;
    }

    IEnumerator JumpingOffLadderRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        isJumpingOffLadder = false;
    }




    void Climb()
    {
        if (gameManager.isGameOver)
        { return; }

        if (layerMaskStates.IsClimbing() && !isJumpingOffLadder)
        {
            //      float verticalInput = Input.GetAxis("Vertical") * moveSpeed; //we just fly away
            //     verticalInput *= Time.deltaTime;
            //  transform.Translate(0, verticalInput, 0); //we move left/right on x-axis}

            float verticalInput = Input.GetAxis("Vertical") * climbSpeed;
            myRigidBody.velocity = new Vector2(0, verticalInput);
            myRigidBody.isKinematic = true;
          //  Debug.Log(myRigidBody.velocity.y);
        }
        else
            myRigidBody.isKinematic = false;
    }

}
