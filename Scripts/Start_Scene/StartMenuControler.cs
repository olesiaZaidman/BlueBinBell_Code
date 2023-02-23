using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuControler : MonoBehaviour
{
    [SerializeField] GameObject startMenuCanvas;
    [SerializeField] GameObject volumeSettingsCanvas;
    [SerializeField] GameObject creditsCanvas;


    StartAudioManager audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<StartAudioManager>(); 
        startMenuCanvas.SetActive(true);
        volumeSettingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }

    public void OnClickStartGame()
    {
        audioManager.PlayClickSound();
        //maibe wait 0.3f sec?
        PlayerPrefs.SetFloat("VolumeMusic", DataBetweenLevels.volumeLevelMusic);
        PlayerPrefs.SetFloat("VolumeEffects", DataBetweenLevels.volumeLevelEffects);
        SceneManager.LoadScene(1);
    }

    public void OnClickVolumeSettings()
    {
        audioManager.PlayClickSound();
        startMenuCanvas.SetActive(false);
        volumeSettingsCanvas.SetActive(true);
    }

    public void OnClickCredits()
    {
        audioManager.PlayClickSound();
        startMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }


    public void OnClickBackStartMenu()
    {
        audioManager.PlayClickSound();
        startMenuCanvas.SetActive(true);
        volumeSettingsCanvas.SetActive(false);
    }

    public void OnClickBackFinalMenu() //!
    {
        audioManager.PlayClickSound();
        startMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    public void OnClickQuitGame()
    {
        audioManager.PlayClickSound();
        Application.Quit();
    }

    public void OnClickExitGame()
    {
        audioManager.PlayClickSound();
        SceneManager.LoadScene(0);
        //  Application.Quit();
    }
}
