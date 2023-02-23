using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] AudioSource footstepsSound;
    PlayerMovement2D Movement;

    void Awake()
    {
        Movement = GetComponent<PlayerMovement2D>();
        footstepsSound.enabled = false; //or true coz we are runnig at start
    }



    void PlayFootstepsAudio()
    {
        if (Movement.CheckPlayerHorizontalMovement() && Movement.IsGrounded())
        { footstepsSound.enabled = true; }
          else
         { footstepsSound.enabled = false; }

    }

    void Update()
    {
        PlayFootstepsAudio();
    }
}
