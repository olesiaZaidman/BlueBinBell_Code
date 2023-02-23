using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    Health playerHealth;
    Mana playerMana;
    Animator playerAnimator;
    ScoreManager scoreManager;
    AudioManager audioManager;
    NarrativeManager narrativeManager;
    MoveDoggo doggoEvents;

    private GameObject player;
    private GameObject doggo;
    [SerializeField] GameObject exitLevelCollider;
    private GameObject enemyBoss;

    //EnemyBoss

    public bool isGameOver = false;
    public bool isLevelFinished = false;
    public bool isGameReadyToStart = false;

    public bool isFinalDoggoReunionEvent = false;
    //  bool isPaused = false;

    [Header("Level")]
    public int gameLevel;

    [Header("Text UI")]
    [SerializeField] TextMeshProUGUI navigationText;

    [Header("Drops FX")]
    [SerializeField] ParticleSystem _caveDropsParticle;

    [Header("HealthBar")]
    [SerializeField] GameObject _health25;
    [SerializeField] GameObject _health50;
    [SerializeField] GameObject _health75;
    [SerializeField] GameObject _health100;


    void Awake()
    {
        
        isLevelFinished = false;
        isGameOver = false;

        narrativeManager = GetComponent<NarrativeManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        audioManager = FindObjectOfType<AudioManager>();

        player = GameObject.Find("Player");
        doggo = GameObject.Find("Doggo");
        //   enemyBoss = GameObject.FindGameObjectWithTag("EnemyBoss");


        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>();
            playerHealth = player.GetComponent<Health>();
            playerMana = player.GetComponent<Mana>();
        }

        if (doggo != null)
        {
            doggoEvents = doggo.GetComponent<MoveDoggo>();
        }

        ChooseStartConditionForlevelIntro(gameLevel);
        PlaySoundEffectsIfNeddedForlevel(gameLevel, 0.1f);
    }

    void Start()
    {
        PlayNarrativeEvent(gameLevel);
        TurnExitColliderOffForFinalLevel(gameLevel);
    }

    #region NAVIGATION_TEXT

    public IEnumerator ShowNavigationText(string _text, float delay, float _delayBeforeDelete)
    {
        yield return new WaitForSeconds(delay);
        navigationText.SetText(_text);
        StartCoroutine(DeleteNavigationTextRoutine(_delayBeforeDelete));
    }

    public IEnumerator DeleteNavigationTextRoutine(float _delayBeforeDelete)
    {
        yield return new WaitForSeconds(_delayBeforeDelete);
        navigationText.SetText(" ");
    }

    #endregion

    void Update()
    {
        DataBetweenLevels.UpdateSoundData();

        PlaySoundEffectsIfNeddedForlevel(gameLevel, 8f);

        if (IsGameOverOnHealth()) //IsGameOverInWater() ||
        {
            audioManager.StopBackgroundMusic();

            if (!isGameOver)
            {
                audioManager.PlayGameOverSound();
            }

            playerAnimator.SetBool("isDying", true);
            isGameOver = true;
            //ADD SCREEN SHAKE
            StartCoroutine(GameOverRoutine());
        }
    }


    #region HEALTH_UI
    //void SetHealthUI(int _health)
    //{
    //    if (_health > 75)
    //    {
    //        _health25.SetActive(true);
    //        _health50.SetActive(true);
    //        _health75.SetActive(true);
    //        _health100.SetActive(true);
    //    }

    //    if (_health <= 75 && _health > 50)
    //    {
    //        _health25.SetActive(true);
    //        _health50.SetActive(true);
    //        _health75.SetActive(true);
    //        _health100.SetActive(false);
    //    }


    //    if (_health <= 50 && _health > 25)
    //    {
    //        _health25.SetActive(true);
    //        _health50.SetActive(true);
    //        _health75.SetActive(false);
    //        _health100.SetActive(false);
    //    }

    //    if (_health <= 25 && _health > 0)
    //    {
    //        _health25.SetActive(true);
    //        _health50.SetActive(false);
    //        _health75.SetActive(false);
    //        _health100.SetActive(false);
    //    }

    //    if (_health <= 0)
    //    {
    //        _health25.SetActive(false);
    //        _health50.SetActive(false);
    //        _health75.SetActive(false);
    //        _health100.SetActive(false);
    //    }

    //}

    #endregion


    #region Narrative_Events

    void PlayNarrativeEvent(int _gamelevel)
    {
        if ((_gamelevel == 0))
        {
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("Come on, Goddo! Hurry up! It's getting dark...", 2f));
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("We will be home soon...", 5f));
            StartCoroutine(narrativeManager.TurnMonologuePanelRoutine(false, 7f));
        }

        if ((_gamelevel == 1))
        {
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("Goddo! Where are you?...", 1f));
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("It's cold here.. Poor Goddo..", 4f));
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("I'll find you, and save you!", 7f));
            StartCoroutine(narrativeManager.TurnMonologuePanelRoutine(false, 10f));
        }

        if ((_gamelevel == 3))
        {
            StartCoroutine(narrativeManager.SetMonologueTextRoutine("Goddo probably fell down here too... ", 1f));
        //    StartCoroutine(narrativeManager.TurnMonologuePanelRoutine(false, 3f));
        }
    }
    #endregion

    #region Game_Events

    void ChooseStartConditionForlevelIntro(int _gamelevel)
    {

        if ((_gamelevel == 0) || (_gamelevel == 3))
        {
            isGameReadyToStart = true;
        }

        if ((_gamelevel == 1) || (_gamelevel == 2))
        {
            //  isGameReadyToStart = true;
            isGameReadyToStart = false;
        }
    }

    void PlaySoundEffectsIfNeddedForlevel(int _gamelevel, float timeDelay)
    {
        if ((_gamelevel == 0)) //Start
        {
            // float _delay = 0.1f;
            audioManager.PlayBirdsIntroScene(timeDelay);
        }


        //if ((gameLevel == 0)) //Update
        //{
        //    float _secondDelay = 8f;
        //    audioManager.PlayBirdsIntroScene(_secondDelay);
        //}

        if (_gamelevel >= 1)
        {
            _caveDropsParticle.Play();
        }
    }
    void TurnExitColliderOffForFinalLevel(int _gamelevel)
    {
        if (_gamelevel == 3)
        {
            exitLevelCollider.SetActive(false);
        }
    }

    public void FreeDoggo() //on trigger after death of boss enemy
    {
        //isFinalDoggoReunionEvent = true;
        doggoEvents.FindClosestCubeAndDestroyIt();
        doggoEvents.TriggerFinalReunionStoryThreeLevel();
        exitLevelCollider.SetActive(true);
    }

    #endregion


    #region Game_Over
    public bool IsGameOverOnHealth()
    {
        return playerHealth.GetHealthPoints() <= 0;
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(2f);
        playerHealth.SetHealthPoints(100);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    #endregion

    #region Load_Level
    public IEnumerator LoadNextLevelRoutine(float _delay)
    {
        SaveDataBetweenLevels();
        yield return new WaitForSeconds(_delay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        //if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) //we don't use it because final scene is credit's Scene
        //{
        //    nextSceneIndex = 0;
        //}
        SceneManager.LoadScene(nextSceneIndex);       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    void SaveDataBetweenLevels()
    {
        DataBetweenLevels.currentHealth = playerHealth.GetHealthPoints();
        DataBetweenLevels.currentMana = playerMana.GetManaPoints();
        DataBetweenLevels.coinScore = scoreManager.GetScorePoints();
    }


    #endregion

}
