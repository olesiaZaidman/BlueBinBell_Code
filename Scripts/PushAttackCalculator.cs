using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAttackCalculator : MonoBehaviour
{
    float _valueDirection;
    int _pushStrength;
  

    int PushStrength
    {
        get
        {
            return _pushStrength;
        }
    }

    public void GetHorizontalDamagePushPhysics(Rigidbody2D _rigidBody)
    {
       // if (IsAttacking())
       // {
         //   IsAttacking() = false;
            float _directionValue = GetVectorDirection();
            Vector2 push = new Vector2(PushStrength * _directionValue, 0);
       //     Debug.Log("Vector push. X: " + push.x + "Y: " + push.y);
            _rigidBody.AddForce(push, ForceMode2D.Impulse);
      //  }
    }

    public void GetVerticalDamagePushPhysics(Rigidbody2D _rigidBody)
    {
       // float _amplifier = 1.2f;
       // float _decreaser = 0.5f;

       //{Work with push phiscis for enemy- maybe gravity off? for now it is floating}

        Vector2 push = new Vector2(0, PushStrength);
        Debug.Log("Vector push. X: " + push.x + "Y: " + push.y);
        _rigidBody.AddForce(push, ForceMode2D.Impulse);
    }

    public void SetPushStrength(int _value)
    {
        _pushStrength = _value;
    }

    public void SetVectorDirection(float _value)
    { _valueDirection = _value;        
    }

    public float GetVectorDirection()
    { return _valueDirection; }


}
