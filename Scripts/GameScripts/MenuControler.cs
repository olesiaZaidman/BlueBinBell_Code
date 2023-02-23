using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    [SerializeField] GameObject userUICanvas;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject volumeSettingsCanvas;

    AudioManager audioManager;
    bool isMenuOpen = false;

    void Start()
    {
        isMenuOpen = false;
        audioManager = FindObjectOfType<AudioManager>();// GameObject.Find("AudioManager");
        userUICanvas.SetActive(true);
        menuCanvas.SetActive(false);
        volumeSettingsCanvas.SetActive(false);
    }

    private void Update()
    {
        OpenMenuOnInput();
    }


    #region User_UI
    public void OnClickOpenMenu() 
    {
        if (!isMenuOpen)
        {
            audioManager.PlayClickSound();
            menuCanvas.SetActive(true);
            isMenuOpen = true;
        }
        else if (isMenuOpen)
        {
            audioManager.PlayClickSound();
            menuCanvas.SetActive(false);
            isMenuOpen = false;
        }
    }


    void OpenMenuOnInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenuOpen)
            {
                menuCanvas.SetActive(true);
                isMenuOpen = true;
            }
            else if (isMenuOpen)
            {
                menuCanvas.SetActive(false);
                isMenuOpen = false;
            }
        }
    }
    #endregion

    #region Menu
    public void OnClickResume() //Resume
    {
        audioManager.PlayClickSound();
        menuCanvas.SetActive(false);
        volumeSettingsCanvas.SetActive(false);
    }

    public void OnClickVolumeSettings() //Settings
    {
        audioManager.PlayClickSound();
        menuCanvas.SetActive(false);
        volumeSettingsCanvas.SetActive(true);
    }

    public void OnClickExitGame()
    {
        audioManager.PlayClickSound();
        SceneManager.LoadScene(0);
        //  Application.Quit();
    }

    #endregion

    #region Volume
    public void OnClickBackMenu()
    {
        audioManager.PlayClickSound();
        menuCanvas.SetActive(true);
        volumeSettingsCanvas.SetActive(false);
    }
    #endregion

}
