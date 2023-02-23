using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueEvents : MonoBehaviour
{
    private GameObject player;
    SceneTriggers sceneTriggers;

    GameManager gameManager;
    NarrativeManager narrativeManager;

    void Awake()
    {
        player = GameObject.Find("Player");
        sceneTriggers = player.GetComponent<SceneTriggers>();

        gameManager = FindObjectOfType<GameManager>();
        narrativeManager = FindObjectOfType<NarrativeManager>();
    }

    void Update()
    {
        PlayMonologueOnTrigger(gameManager.gameLevel);
    }

    void PlayMonologueOnTrigger(int _gameLevel)
    {
        if (sceneTriggers.isMonologueTriggered && _gameLevel == 0)
        {
            sceneTriggers.isMonologueTriggered = false;
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("Goddo, where are you?", 1f));
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("...Probably he run away deep into the cave...", 3f));
            StartCoroutine(narrativeManager.TurnMonologuePanelRoutine(false, 6f));
        }

        if (sceneTriggers.isMonologueTriggered && _gameLevel == 3)
        {
            sceneTriggers.isMonologueTriggered = false;
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("Goddo, finally! Here you are!", 0.1f));
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("...Let's get out from this place..", 2f));
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("We are going home!", 4f));
            StartCoroutine(narrativeManager.TurnMonologuePanelRoutine(false, 15f));
        }

        if (sceneTriggers.isFindingDoggoTriggered && _gameLevel == 3)
        {
            sceneTriggers.isFindingDoggoTriggered = false;
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("Goddo, I found you! Hey, it's me!..", 0.1f));
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("Is it you who got my Goddo!? I'll beat you up!", 3.5f));
            StartCoroutine(narrativeManager.TurnMonologuePanelRoutine(false, 7f));
        }


        if (sceneTriggers.isFinallyMeetingDoggoEvent && _gameLevel == 3)
        {
            sceneTriggers.isFinallyMeetingDoggoEvent = false;
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("Goddo, I missed you!", 0.1f));
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("...Let's get out from here..", 3f));
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("We are going home!", 8f));
            StartCoroutine(narrativeManager.TurnMonologuePanelRoutine(false, 15f));
        }


}

  
}
