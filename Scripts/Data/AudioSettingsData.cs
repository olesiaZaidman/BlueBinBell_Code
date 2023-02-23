using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingsData : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] AudioSource soundEffectsAudio;
    [SerializeField] AudioSource backgroundMusic;


    private void Update()
    {
        //if clicked settings???it can be in conflic with sound bar at start
        SaveVolumeSettingsData();
    }

    public void SaveVolumeSettingsData()
    {
        DataBetweenLevels.volumeLevelMusic = backgroundMusic.volume;
        DataBetweenLevels.volumeLevelEffects = soundEffectsAudio.volume;
    }

    //public float GetVolumeMusicSettings()
    //{
    //    float volumeMusic = DataBetweenLevels.volumeLevelMusic;
    //    Debug.Log("Music volume: " + volumeMusic);
    //    return volumeMusic;
    //}

    //public float GetVolumeEffectsSettings()
    //{
    //    float volumeEffects = DataBetweenLevels.volumeLevelEffects;
    //    Debug.Log("Effects volume: " + volumeEffects);
    //    return volumeEffects;
    //}


    //public float GetVolumeMusicSettings()
    //{
    //    Debug.Log("Music volume: " + volumeMusic);
    //    return volumeMusic;
    //}

    //public float GetVolumeEffectsSettings()
    //{
    //    Debug.Log("Effects volume: " + volumeEffects);
    //    return volumeEffects;
    //}
}
