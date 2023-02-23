using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    EnemyAttack Attack;


    [Space(5)]
    [Header("Movement")]
    [Range(0f, 10f)] public float speed;
    [Range(0f, 100f)] public float attackRangeDistance;

    [Space(5)]
    [Header("Damage")] //damage that we recieve
    [Range(0f, 100f)] public int enemyDamagePoints;   //  public int EnemyDamagePoints { get; private set; }
    int _damagePoints;
    public int DamagePoints
    {
        get
        {
            return _damagePoints;
        }
    }

    public void SetEnemyDamagePoints(int _value)
    { _damagePoints = _value; }


    [Space(5)]
    [Header("Attack")] // how we attack
    [Range(0f, 100f)] public int enemyAttackPoints;
    [Range(0f, 40f)] public int pushAttackStrength; //20
    int _attackPoints;
    public int AttackPoints
    {
        get
        {
            return _attackPoints;
        }
    }

    public void SetEnemyAttackPoints(int _value)
    { _attackPoints = _value; }


    public float EnemyLookDirectionOnX
    {
        get
        {
            return Attack.GetLookDirectionValue();
        }
    }

    private void Awake()
    {
        Attack = GetComponent<EnemyAttack>();
    }
    void Start()
    {       
        SetEnemyAttackPoints(enemyAttackPoints);
    }
}
