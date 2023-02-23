using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBetweenLevels 
{
    [Header("Volume")]
    public static float volumeLevelMusic;
    public static float volumeLevelEffects;

    [Header("Points")]
    public static int currentHealth;
    public static int currentMana;
    public static int coinScore;
  

   public static void UpdateSoundData() // we call it in gamemanager's Update
    {
        PlayerPrefs.SetFloat("VolumeMusic", volumeLevelMusic);
        PlayerPrefs.SetFloat("VolumeEffects", volumeLevelEffects);
    }

    //PlayerPrefs.GET Returns the value corresponding to key in the preference file if it exists.
    //If it doesn't exist, PlayerPrefs.GetInt will return defaultValue.

    //public static float GetMusicVolume()
    //{
    //    float volumeMusic = PlayerPrefs.GetFloat("VolumeMusic", volumeLevelMusic);
    //    return volumeMusic;
    //}

    //public static float GetEffectsVolume()
    //{
    //    float volumeEffects = PlayerPrefs.GetFloat("VolumeEffects", volumeLevelEffects);
    //    return volumeEffects;
    //}


    //public static int GetHP()
    //{
    //    int hp = PlayerPrefs.GetInt("HealthPoints", currentHealth);
    //    return hp;
    //}

    //int GetNumber() // we return int
    //{
    //    int myNumber = PlayerPrefs.GetInt("MyNumber", 0);
    //    //the number that is stored in PlayerPrefs
    //    return myNumber;
    //}
}
