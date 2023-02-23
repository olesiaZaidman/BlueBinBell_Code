using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator myAnimator; //We use Different Player  Ainmator for this CODE
    private void Awake()
    {
      //  gameManager = FindObjectOfType<GameManager>();
        myAnimator = GetComponent<Animator>();
    }

    void AnimatonStateManager()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
        Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            myAnimator.SetBool("isRunning", false); //isClimbing? IsIdle?

        //when buttons are released we set walking to false

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            myAnimator.SetBool("isClimbing", true);
            // transform.Translate(0, 0, translation);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            myAnimator.SetBool("isRunning", true);
            //   transform.Rotate(0, rotation, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            myAnimator.SetTrigger("Jumping");
        //else if (Input.GetKeyDown(KeyCode.P))
        //    myAnimator.SetTrigger("Punching");
        //else if (Input.GetKeyDown(KeyCode.K))
        //    myAnimator.SetTrigger("Kicking");
    }
}
