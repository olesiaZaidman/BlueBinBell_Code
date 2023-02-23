using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    GameManager gameManager;

    [Header("AudioSource")]

    [SerializeField] AudioSource soundEffectsAudio;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource backgroundAmbient;

    [Header("SoundEffects")]
    public AudioClip rainSound;
    public AudioClip thunderSound;
    public AudioClip birdSound;

    [Header("Doggo")]
    public AudioClip doggoBarkSound;

    [Header("Music")]
    public AudioClip caveMusic;
    public AudioClip bossFightMusic;
    public AudioClip scaryMusic;
    public AudioClip winnigMusic;

    [Header("UI")]
    public AudioClip clickButtonSound;
    public AudioClip textNavigationApperanceSound;

    [Header("GamePlay")]
    //  public AudioClip introSound;
    public AudioClip gameOverSound;
    public AudioClip winLevelSound;
    public AudioClip waterSplashSound;
    public AudioClip[] waterDropSound;

    [Header("Hero Movement")]
    public AudioClip jumpSound;
    public AudioClip runningSound;
    // public AudioClip landingSound; ?? do we really need it?


    [Header("Hero Fight")]
    public AudioClip voiceHurtSound; //beingpushed
    public AudioClip voiceAttackSound;
    public AudioClip shootSound;
    public AudioClip kickSound;
    public AudioClip spellSound;
    public AudioClip frostSound;

    [Header("Effects")]
    public AudioClip pickUpSound;
    public AudioClip coinsSound;
    public AudioClip potionPickUPSound;
    public AudioClip coinsCollisionSound;


    [Header("Monsters")]
    public AudioClip monsterHurtSound;
    public AudioClip monsterDeadSound;


    bool isWaterDroped = false;
    bool isBirdChirpped = false;
    bool isTimeToChangeMusic = false;

    void Awake()
    {
        backgroundMusic.volume = PlayerPrefs.GetFloat("VolumeMusic", DataBetweenLevels.volumeLevelMusic);
        backgroundAmbient.volume = PlayerPrefs.GetFloat("VolumeMusic", DataBetweenLevels.volumeLevelMusic);
        soundEffectsAudio.volume = PlayerPrefs.GetFloat("VolumeEffects", DataBetweenLevels.volumeLevelEffects);
    }
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        PlayBackgroundMusicForLevelAtStart(gameManager.gameLevel);
    }

    void Update()
    {
        float delay = Random.Range(2f, 10f);
        if (gameManager.gameLevel >= 1)
        { StartCoroutine(PlayWaterDropRoutine(delay)); }

        PlayBackgroundCaveFightMusicForLevelOnUpdate(gameManager.gameLevel);

    }

    #region Musci_Soundtrack

    void PlayBackgroundMusicForLevelAtStart(int _gameLevel)
    {

        if (_gameLevel == 2) //(_gameLevel == 1) ||
        {
            backgroundMusic.clip = caveMusic;
            backgroundMusic.Play();
        }


        if (_gameLevel == 3)
        {
            EnemyGetDamage enemy = FindObjectOfType<EnemyGetDamage>();
            if (!enemy.isDead)
            {
                backgroundMusic.clip = scaryMusic;
                backgroundMusic.Play();
            }
        }
    }

    void PlayBackgroundCaveFightMusicForLevelOnUpdate(int _gameLevel)
    {
        if (_gameLevel == 3)
        {
            EnemyGetDamage enemy = FindObjectOfType<EnemyGetDamage>();
            SceneTriggers sceneTriggers = FindObjectOfType<SceneTriggers>();

            if (!enemy.isDead && sceneTriggers.isBossFight)
            {
                sceneTriggers.isBossFight = false;
                backgroundMusic.clip = bossFightMusic;
                backgroundMusic.Play();
            }


            if (enemy.isDead && !isTimeToChangeMusic)
            {
                isTimeToChangeMusic = true;
                backgroundMusic.clip = winnigMusic;
                backgroundMusic.Play();
            }
        }
    }

#endregion

    #region UI
    public void PlayClickSound()
    {
        soundEffectsAudio.PlayOneShot(clickButtonSound, soundEffectsAudio.volume);
    }

    public void PlayTextSound()
    {
        //float _volume = 0.5f;
        soundEffectsAudio.PlayOneShot(textNavigationApperanceSound, soundEffectsAudio.volume);
    }
    #endregion

    #region SoundAmbientEffects

    public void PlayRainIntroScene()
    {
        soundEffectsAudio.PlayOneShot(thunderSound, soundEffectsAudio.volume);
        // backgroundAmbient.Play();
        backgroundAmbient.PlayDelayed(1f);
    }

    public void PlayBirdsIntroScene(float _delay)
    {      
        StartCoroutine(PlayBirdsChirpingRoutine(_delay));
       // soundEffectsAudio.PlayOneShot(birdSound, soundEffectsAudio.volume);
    }

    IEnumerator PlayBirdsChirpingRoutine(float _delay)
    { 
        if (!isBirdChirpped)
        {          
            isBirdChirpped = true;
            yield return new WaitForSeconds(_delay);
            soundEffectsAudio.PlayOneShot(birdSound, soundEffectsAudio.volume);
            StartCoroutine(PlayBirdsDoubleChirpingRoutine());
        }
    }



    IEnumerator PlayBirdsDoubleChirpingRoutine()
    {
        yield return new WaitForSeconds(5f);
        soundEffectsAudio.PlayOneShot(birdSound, soundEffectsAudio.volume);
        isBirdChirpped = false;
    }

    IEnumerator PlayWaterDropRoutine(float _delay)
    {
        if (!isWaterDroped)
        {
            isWaterDroped = true;

            int i = Random.Range(0, waterDropSound.Length);
            soundEffectsAudio.PlayOneShot(waterDropSound[i], soundEffectsAudio.volume);      //or backgroundMusic.volume   
            yield return new WaitForSeconds(_delay);
            StartCoroutine(WaterFropRoutineRecharge(_delay));
        }
    }
    IEnumerator WaterFropRoutineRecharge(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        isWaterDroped = false;
    }

    #endregion

    #region Doggo
    public void PlayDoggoBarkSound()
    {
        soundEffectsAudio.PlayOneShot(doggoBarkSound, soundEffectsAudio.volume);
        StartCoroutine(PlayDoggoDoubleBark());
    }

    IEnumerator PlayDoggoDoubleBark()
    {
        yield return new WaitForSeconds(0.3f);
        soundEffectsAudio.PlayOneShot(doggoBarkSound, soundEffectsAudio.volume);
    }
    #endregion

    #region GamePlay
    public void PlayWaterSplashSound()
    {
        // float _volume = 0.6f;
        soundEffectsAudio.PlayOneShot(waterSplashSound, soundEffectsAudio.volume);
    }


    public void PlayGameOverSound()
    {
        //  float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(gameOverSound, soundEffectsAudio.volume);
    }

    public void PlayWinLevelSound()
    {
        // float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(winLevelSound, soundEffectsAudio.volume);
    }


    //private IEnumerator PlayIntroSoundRoutine(float _startIntroDelay)
    //{
    //    yield return new WaitForSeconds(_startIntroDelay);
    //    // PlayIntroSound();
    //    // backgroundSoundtrack.PlayDelayed(1.7f);
    //    backgroundMusic.Play();
    //}



    //public void PlayIntroSound()
    //{
    //  //  float _volume = 0.5f;
    //    soundEffectsAudio.PlayOneShot(introSound, soundEffectsAudio.volume);
    //}


    #endregion

    #region Player_Movement
    public void PlayRunningSound()
    {
        float _delay = 0.1f;
        StartCoroutine(PlayRunningSoundRoutine(_delay));
    }

    private IEnumerator PlayRunningSoundRoutine(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        //  float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(runningSound, soundEffectsAudio.volume * 0.5f);
    }


    public void PlayJumpSound()
    {
        //   float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(jumpSound, soundEffectsAudio.volume);
    }

    #endregion

    #region Player_Fight


    public void PlayVoiceAttackSound()
    {
        //float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(voiceAttackSound, soundEffectsAudio.volume);
    }

    //public void PlayAmbientCaveMusic()
    //{
    //    backgroundMusic.clip = caveMusic;
    //}

    public void PlayHurtSound()
    {
        // float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(voiceHurtSound, soundEffectsAudio.volume);
    }

    public void PlaySpellSound()
    {
        // float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(spellSound, soundEffectsAudio.volume);
    }


    public void PlayFrostSpellSound()
    {
        soundEffectsAudio.PlayOneShot(frostSound, soundEffectsAudio.volume);
    }

    public void PlayShootSound()
    {
        // float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(shootSound, soundEffectsAudio.volume);
    }

    public void PlayKickSound()
    {
        // float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(kickSound, soundEffectsAudio.volume);
    }



    #endregion

    #region Effects


    public void PlayPickUpSound()
    {
        //  float _volume = 0.5f;
        soundEffectsAudio.PlayOneShot(pickUpSound, soundEffectsAudio.volume);
    }

    public void PlayPotionPickUpSound()
    {
        // float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(potionPickUPSound, soundEffectsAudio.volume);
    }


    public void PlayPickUpCoinsSound()
    {
        //   float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(coinsSound, soundEffectsAudio.volume);
    }

    public void PlayCoinsCollisionSound()
    {
        //  float _volume = 0.8f;
        soundEffectsAudio.PlayOneShot(coinsCollisionSound, soundEffectsAudio.volume);
    }
    #endregion

    #region Music
    public void StopBackgroundMusic()
    { backgroundMusic.Stop(); }


    //public void PlayBackgroundMusic()
    //{
    //    float _volume = 0.8f;

    //   // gameEffectsAudio.PlayOneShot(backgroundMusic, _volume);
    //}

    //private IEnumerator PlaySoundTrackRoutine(float _startIntroDelay)
    //{
    //    yield return new WaitForSeconds(_startIntroDelay);
    //    PlayIntroSound();
    //}



    #endregion

    #region Monsters

    public void PlayMonsterHurtSound()
    {
        //  float _volume = 0.5f;
        soundEffectsAudio.PlayOneShot(monsterHurtSound, soundEffectsAudio.volume);
    }

    public void PlayMonsterDeadSound()
    {
        //  float _volume = 0.5f;
        soundEffectsAudio.PlayOneShot(monsterDeadSound, soundEffectsAudio.volume);
    }

    #endregion

}
