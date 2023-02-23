using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTriggers : MonoBehaviour //sits on Player
{
    public bool isRainSceneTriggered = false;
    public bool isMonologueTriggered = false;
    public bool isFindingDoggoTriggered = false;
    public bool isFinallyMeetingDoggoEvent = false;
    public bool isBossFight = false;

    private void Awake()
    {
        isRainSceneTriggered = false;
        isMonologueTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "RainTrigger")
        {
            isRainSceneTriggered = true;          
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Words")
        {
            isMonologueTriggered = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "FinallyMeetingDoggoEvent")
        {
            isFinallyMeetingDoggoEvent = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "FindingDoggoTriggered")
        {
            isFindingDoggoTriggered = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "BossFightEvent")
        {
            isBossFight = true;
            Destroy(other.gameObject);
        }

    }
}
