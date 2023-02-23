using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{

    [Header("Mana")]
    [Range(0f, 300f)] public int maxManaPoints;

    GameManager gameManager;
    int _currentManaPoints; //we set it in GetDamage() class
    int _maxManaPoints;


    private void Awake()
    {
        SetManaPoints(maxManaPoints);
        SetMaxManaPoints(maxManaPoints);
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        GetAndSetManaPointsForCurrentLevel();
    }

    void GetAndSetManaPointsForCurrentLevel()
    {
        if (gameManager.gameLevel < 2)
        {
            SetManaPoints(maxManaPoints);
        }
        else
        {
            SetManaPoints(DataBetweenLevels.currentMana);
        }

      //  Debug.Log("MANA FOR THIS LEVEL" + GetManaPoints()); ;
    }


    public int GetManaPoints()
    {
        return _currentManaPoints;
    }

    //public int GetMaxManaPoints()
    //{
    //    return _maxManaPoints;
    //}

    public void SetManaPoints(int _mana)
    {
        _currentManaPoints = _mana;
      //  Debug.Log("Set Mana:" + _currentManaPoints);
    }

    public void SetMaxManaPoints(int _mana)
    {
        _maxManaPoints = _mana;
        //   Debug.Log("Set Max Mana:" + _maxManaPoints);
    }

    public void ReduceManaPoints(int _points)
    {
        _currentManaPoints -= _points;
        //  Debug.Log("Damage Mana:" + _currentManaPoints);
    }

    public void RecoverManaPoints(int _mana)
    {
        _currentManaPoints += _mana;
        //   Debug.Log("Mana After Recover:" + _currentManaPoints);
    }

}
