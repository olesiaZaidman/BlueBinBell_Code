using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] AudioSource soundEffectsAudio;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource backgroundAmbient;

    [Header("UI")]
    public AudioClip clickButtonSound;

    public void PlayClickSound()
    {
        soundEffectsAudio.PlayOneShot(clickButtonSound, soundEffectsAudio.volume);
    }

}
