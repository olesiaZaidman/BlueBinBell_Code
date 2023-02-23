using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position, 1f);
            //Playe an AudioClip at a given Position in world space
            //PlayClipAtPoint(AudioClip clip, Vector3 position, float volume)
            Destroy(gameObject);
        }
    }
}
