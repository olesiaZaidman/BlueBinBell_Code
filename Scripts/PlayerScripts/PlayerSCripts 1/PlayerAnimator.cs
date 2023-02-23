using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
   // PlayerMovement2D movement;
   // private Animator myAnimator;
   //// private SpriteRenderer spriteRend;

   // DustEffect dustEffect;

   // public bool startedJumping { private get; set; }
   // public bool justLanded { private get; set; }
   // public float currentVelY;

   // [Header("Particle FX")]
   // [SerializeField] private GameObject jumpFX;
   // [SerializeField] private GameObject landFX;
   // private ParticleSystem _jumpParticle;
   // private ParticleSystem _landParticle;

   // void Awake()
   // {
   //     movement = GetComponent<PlayerMovement2D>();
   //     dustEffect = FindObjectOfType<DustEffect>();
   //     //spriteRend = GetComponentInChildren<SpriteRenderer>(); && //anim = spriteRend.GetComponent<Animator>();
   //     myAnimator = GetComponent<Animator>();
   //     _jumpParticle = jumpFX.GetComponent<ParticleSystem>();
   //     _landParticle = landFX.GetComponent<ParticleSystem>();
   // }
   // private void CheckAnimationState()
   // {
   //     if (startedJumping)
   //     {
   //         myAnimator.SetTrigger("Jump");
   //         GameObject obj = Instantiate(jumpFX, transform.position - (Vector3.up * transform.localScale.y / 2), Quaternion.Euler(-90, 0, 0));
   //         Destroy(obj, 1);
   //         startedJumping = false;
   //         return;
   //     }

   //     if (justLanded)
   //     {
   //         myAnimator.SetTrigger("Land");
   //         GameObject obj = Instantiate(landFX, transform.position - (Vector3.up * transform.localScale.y / 1.5f), Quaternion.Euler(-90, 0, 0));
   //         Destroy(obj, 1);
   //         justLanded = false;
   //         return;
   //     }

   //     myAnimator.SetFloat("Vel Y", movement.MyRigidBody.velocity.y);
   // }
}
