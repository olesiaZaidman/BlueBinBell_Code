using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    GameObject player;
    float curSpeed;

    float acceleration = 0.05f;
    float minSpeed = 20.0f;
    float midSpeed = 50.0f;
    float maxSpeed = 70.0f;


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        curSpeed = Random.Range(minSpeed, midSpeed);
    }
     void Update()
    {
        player = GameObject.Find("Player");
        MagnetToPlayer();
    }
   
    public void MagnetToPlayer()
    {
        Vector2 playerPoint = Vector2.MoveTowards(transform.position, player.transform.position, curSpeed * Time.deltaTime);//           
        _rigidBody.MovePosition(playerPoint);
        curSpeed += acceleration;
        if (curSpeed > maxSpeed)
        { curSpeed = maxSpeed; }
    }

}
