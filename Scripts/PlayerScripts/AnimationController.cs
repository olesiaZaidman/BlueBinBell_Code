using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator myAnimator;
    GameManager gameManager;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }


    void Update()
    {
        PlayAnimationIfConditionTrue("isDying", gameManager.isGameOver);
    }
  

    public void PlayAnimation(string _animName, bool _isPlaying)
    {
        if (_isPlaying)
        {
            myAnimator.SetBool(_animName, true);
            //  Debug.Log(_animName + " is playing");
        }
        else if (!_isPlaying)
        {
            myAnimator.SetBool(_animName, false);
        }
    }

    void PlayAnimationIfConditionFalse(string _animName, bool _isTouchingLayerMask1)
    {
        if (_isTouchingLayerMask1)
        {
            myAnimator.SetBool(_animName, false);
            //  Debug.Log(_animName + " not playing");
        }
        else if (!_isTouchingLayerMask1)
        {
            myAnimator.SetBool(_animName, true);
            //  Debug.Log(_animName + " is playing");
        }
    }


    void PlayAnimationIfConditionTrue(string _animName, bool _isTouchingLayerMask1)
    {
        if (_isTouchingLayerMask1)
        {
            myAnimator.SetBool(_animName, true);
            //  Debug.Log(_animName + " is playing");
        }
        else if (!_isTouchingLayerMask1)
        {
            myAnimator.SetBool(_animName, false);
        }
    }

    void PlayAnimationIfYESTouchingLayerMasks(string _animName, bool _isTouchingLayerMask1)
    {
        if (_isTouchingLayerMask1)
        {
            myAnimator.SetBool(_animName, true);
            //  Debug.Log(_animName + " is playing");
        }
        else if (!_isTouchingLayerMask1)
        {
            myAnimator.SetBool(_animName, false);
        }
    }


    void PlayAnimationIfYESTouchingLayerMasks(string _animName, bool _isTouchingLayerMask1, bool _isTouchingLayerMask2)
    {
        if (_isTouchingLayerMask1 || _isTouchingLayerMask2)
        {
            myAnimator.SetBool(_animName, true);
            // Debug.Log(_animName + " is playing");
        }
        else if (!_isTouchingLayerMask1 || _isTouchingLayerMask2)
        {
            myAnimator.SetBool(_animName, false);
        }
    }

    void PlayAnimationIfNOTTouchingLayerMasks(string _animName, bool _isTouchingLayerMask1, bool _isTouchingLayerMask2)
    {
        if (_isTouchingLayerMask1 || _isTouchingLayerMask2)
        {
            myAnimator.SetBool(_animName, false);
            //  Debug.Log(_animName + " not playing");
        }
        else if (!_isTouchingLayerMask1 || _isTouchingLayerMask2)
        {
            myAnimator.SetBool(_animName, true);
            // Debug.Log(_animName + " is playing");
        }
    }


    void PlayAnimationIfNOTTouchingLayerMasks(string _animName, bool _isTouchingLayerMask1)
    {
        if (_isTouchingLayerMask1)
        {
            myAnimator.SetBool(_animName, false);
            //  Debug.Log(_animName + " not playing");
        }
        else if (!_isTouchingLayerMask1)
        {
            myAnimator.SetBool(_animName, true);
            //  Debug.Log(_animName + " is playing");
        }
    }

}
