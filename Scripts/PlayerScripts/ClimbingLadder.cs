using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingLadder : MonoBehaviour
{
    float verticalMove = 0f;
    float climbSpeed = 5f;

    [SerializeField] private LayerMask whatIsLadder;
    [SerializeField] private Transform groundCheck;
    private CircleCollider2D circleCollider;
    Animator myAnimator;

    Rigidbody2D MyRigidBody;

    void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        verticalMove = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Climb();
    }

    void Climb()
    {
        if (IsLadder())
        {
            MyRigidBody.gravityScale = 0f;
            MyRigidBody.velocity = new Vector2(MyRigidBody.velocity.x, verticalMove * climbSpeed);
            myAnimator.SetBool("isClimbIdle", true);

            if (CheckPlayerVerticalMovement())
            {
                myAnimator.SetBool("isClimbing", true);
                myAnimator.SetBool("isClimbIdle", false);
            }
        }
        else if (!IsLadder())
        {
            MyRigidBody.gravityScale = 4f;
            myAnimator.SetBool("isClimbing", false);
            myAnimator.SetBool("isClimbIdle", false);

        }
        else
            return;
    }

    bool IsLadder()
    {
        float _groundedOverlapRadius = 0.15f;
        return Physics2D.OverlapCircle(groundCheck.position, _groundedOverlapRadius, whatIsLadder); //OverlapCircleAll()?
    }

    //bool IsLadder() //check for ladder
    //{
    //    RaycastHit2D Hitinfo = Physics2D.CircleCast(circleCollider.bounds.center, 0.5f, Vector2.down, 0.01f, whatIsLadder);
    //    return Hitinfo.collider != null;
    //}

    bool CheckPlayerVerticalMovement()
    {
        return Mathf.Abs(verticalMove) > Mathf.Epsilon;
    }

}
