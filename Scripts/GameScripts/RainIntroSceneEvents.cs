using System.Collections;
using UnityEngine;


public class RainIntroSceneEvents : MonoBehaviour
{
    private GameObject player;
    SceneTriggers sceneTriggers;

    [Header("Level")]
    [SerializeField] ParticleSystem rainFx;
    AudioManager audioManager;
    MoveDoggo moveDoggo;
    [SerializeField] GameObject _darkSky;
    NarrativeManager narrativeManager;

    void Awake()
    {
        _darkSky.SetActive(false);
        player = GameObject.Find("Player");
        sceneTriggers = player.GetComponent<SceneTriggers>();

        audioManager = FindObjectOfType<AudioManager>();
        moveDoggo = FindObjectOfType<MoveDoggo>();
        narrativeManager = FindObjectOfType<NarrativeManager>();
    }

    void Update()
    {
        PlayRainSceneOnTrigger();
    }

    void PlayRainSceneOnTrigger()
    {
        if (sceneTriggers.isRainSceneTriggered)
        {
            float delay = 1.5f;
            sceneTriggers.isRainSceneTriggered = false;
            rainFx.Play();
            _darkSky.SetActive(true);
            audioManager.PlayRainIntroScene();
            StartCoroutine(DoggoRunningAwayRoutine(delay));
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("Oh, no.. Hey!!", 2f));
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("Wait!! Goddo!", 4f));
            StartCoroutine(narrativeManager.TurnMonologuePanelRoutine(false, 7f));
        }
    }


    public IEnumerator DoggoRunningAwayRoutine(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        moveDoggo.TriggerRunAwayStoryIntroLevel();
    }

}
