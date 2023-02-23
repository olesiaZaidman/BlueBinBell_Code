using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskStates : MonoBehaviour
{
    // BoxCollider2D myFeetCollider;
    //  CapsuleCollider2D myCollider;
    BoxCollider2D myHeadCollider;
    CircleCollider2D myFeetCollider;

    public bool isStanding = false;
    public bool isStandingOnEnemy = false;
    public bool isEnemy = false;
    public bool isClimbing = false;
    void Awake()
    {
        //  myCollider = GetComponent<CapsuleCollider2D>();
        myHeadCollider = GetComponent<BoxCollider2D>();
        myFeetCollider = GetComponent<CircleCollider2D>();
    }

    public bool IsClimbing()
    {
        isClimbing = true;
        isStanding = false;
        isEnemy = false;
        //  Debug.Log("Touching the ladder");
        return myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }


    public bool IsStanding()
    {
        //   Debug.Log("Touching the ground");
        isStanding = true;
        isEnemy = false;
        isClimbing = false;
        return myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

    }

    //public bool IsConveyer()
    //{
    //    return myFeetCollider.IsTouchingLayers(LayerMask.GetMask("SlidingPlatform"));
    //}


    public bool IsEnemy()
    {
        isStanding = false;
        isEnemy = true;
        isClimbing = false;
        return myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"));  
    }


    bool IsWater() //IsWaterHazards
    {
        return myHeadCollider.IsTouchingLayers(LayerMask.GetMask("Water"));//LayerMask.GetMask("Water", "Hazards"));
    }

}
