using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOLDONE : MonoBehaviour
{
    CharPlayerController controller;
  //  LayerMaskStates layerMaskStates;
    GameManager gameManager;
    float horizontalMove = 0f;
 //   float verticalMove = 0f;
    float runSpeed = 40f;
  //  float climbSpeed = 10f;
    bool jump = false;
 //   bool climb = false;

     void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        controller = GetComponent<CharPlayerController>();
     //   layerMaskStates = FindObjectOfType<LayerMaskStates>();
    }

    void Update()
    {
        if (gameManager.isGameOver)
        { return; }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed ;
      //  verticalMove = Input.GetAxisRaw("Vertical") * climbSpeed;

        if (Input.GetButtonDown("Jump")) //Input.GetButtonDown("Jump") = Input.GetKeyDown(KeyCode.Space)
        { jump = true; }

       // if (layerMaskStates.IsClimbing())
       // {
       //     climb = true;
       //     Debug.Log("Climb"+ climb);
       //   //  myRigidBody.isKinematic = true;
       // }
       //else if (!layerMaskStates.IsClimbing())
       // {
       //     climb = false;
       //    //  myRigidBody.isKinematic = false;
       // }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
       // controller.Climb(verticalMove * Time.fixedDeltaTime, jump, climb);
        jump = false;
    }
}
