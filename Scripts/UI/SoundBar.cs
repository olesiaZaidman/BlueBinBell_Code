using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBar : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource backgroundAmbient;
    public AudioSource soundEffects;

    public Slider volumeLevelMusic;
    public Slider volumeLevelEffects;

    //[SerializeField] Image soundFillImage;

     void Awake()
    { //if(SceneIndex>0)
       // SetVolumeLevel();
    }
    void Update()
    {
        TweakVolumeLevel();
    }


    void TweakVolumeLevel()
    {
       // backgroundMusic.volume = soundFillImage.fillAmount;
      //  backgroundAmbient.volume = soundFillImage.fillAmount;

        backgroundMusic.volume = volumeLevelMusic.value;
        backgroundAmbient.volume = volumeLevelMusic.value;
        soundEffects.volume = volumeLevelEffects.value;
    }

    //void SetVolumeLevel()
    //{
    //    // volumeLevelMusic.value = DataBetweenLevels.volumeLevelMusic;
    //    //  volumeLevelEffects.value = DataBetweenLevels.volumeLevelEffects;

    //    // volumeLevelMusic.value = DataBetweenLevels.GetMusicVolume();
    //    //  volumeLevelEffects.value = DataBetweenLevels.GetEffectsVolume();

    //    volumeLevelMusic.value = PlayerPrefs.GetFloat("VolumeMusic", DataBetweenLevels.volumeLevelMusic);
    //    volumeLevelEffects.value = PlayerPrefs.GetFloat("VolumeEffects", DataBetweenLevels.volumeLevelEffects);

    //    Debug.Log("Music:" +volumeLevelMusic.value);
    //    Debug.Log("Effects:" + volumeLevelMusic.value);
    //}



}
