using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [Range(0f, 300f)] public int maxHealthPoints;


    int _currentHealthPoints; //we set it in GetDamage() class
    int _maxHealthPoints;

    public void ReduceHealth(int _damage)
    {
        _currentHealthPoints -= _damage;
     //   Debug.Log("Damage HP:" + _currentHealthPoints);
    }

    public int GetHealthPoints()
    {
        return _currentHealthPoints;
    }

    //  public int GetMaxHealthPoints()
    //  {
    //     return maxHealthPoints;
    //  }

    public void SetHealthPoints(int _hp)
    {
        _currentHealthPoints = _hp;
      //  Debug.Log("Set HP:" + _currentHealthPoints);
    }

    public void SetMaxHealthPoints(int _hp)
    {
        _maxHealthPoints = _hp;
     //   Debug.Log("Set Max HP:" + _maxHealthPoints);
    }

    public void RecoverHealthPoints(int _hp)
    {
        _currentHealthPoints += _hp;
      //  Debug.Log("HP After Recover:" + _currentHealthPoints);
    }

}

