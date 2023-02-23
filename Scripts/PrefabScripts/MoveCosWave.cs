using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCosWave : MonoBehaviour
{
    float speed = 1.5f;
    float height = 0.8f;

    float initialPosX;
    float initialPosY;


    private void Awake()
    {
        initialPosX = transform.position.x;
        initialPosY = transform.position.y;
       // Debug.Log("Initial Pos x:" + initialPosX + " " + "Initial Pos y:" + initialPosY);
    }

    void Update()
    {
        MoveUpDown();
    }

    void MoveUpDown()
    {
        float newY = Mathf.Cos(Time.time * speed) * height;
        transform.position = new Vector3(initialPosX, (initialPosY + Mathf.Pow(newY, 2))); // + Mathf.Abs(newY)
      //  Debug.Log("Potion location:" + transform.position);
    }

}
