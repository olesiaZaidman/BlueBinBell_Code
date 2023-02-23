using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : MonoBehaviour
{

	[Space(5)]
	[Header("Gravity")]
	[HideInInspector] public float gravityStrength; //Downwards force (gravity) needed for the desired jumpHeight and jumpTimeToApex.
	[HideInInspector] public float gravityScale = 4f; //Strength of the player's gravity as a multiplier of gravity
	[HideInInspector] public float zeroGravity = 0f;
	[HideInInspector]  public float fallGravityMult = 2f; //Multiplier to the player's gravityScale when falling.
	[HideInInspector]  public float fastFallGravityMult = 20f; //Larger multiplier to the player's gravityScale when they are falling and a downwards input is pressed.

	[Space(5)]

	[Header("Run")]
	[Range(0f, 15f)] public float moveSpeed;

	[Space(5)]
	[Header("Jump")]
	[Range(0f, 50f)] public float jumpingPower;

	[Space(5)]
	[Header("Climb")]
	[Range(0f, 10f)] public float climbSpeed;

	[Space(5)]
	[Header("Damage")] //damage that we recieve
	int _damagePoints;

	public int DamagePoints  
	{
		get
		{
			return _damagePoints;
		}
	}

	public void SetPlayerDamagePoints(int _value)
	{ _damagePoints = _value; }


	[Space(5)]
	[Header("Attack")] // how we attack
	[Range(0f, 100f)] public int attackPoints;
	[Range(0f, 40f)] public int pushAttackStrength; //20
	public int AttackPoints
	{
		get
		{
			return attackPoints;
		}
	}

	public void SetPlayerAttackPoints(int _value)
	{ attackPoints = _value; }

	public float LookDirectionOnX
	{
		get
		{
			return transform.localScale.x; //1 or -1
		}
	}



	void Start()
	{
		SetPlayerAttackPoints(attackPoints);
	}


}
