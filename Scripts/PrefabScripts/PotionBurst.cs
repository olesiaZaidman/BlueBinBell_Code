using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBurst : MonoBehaviour
{
    SpriteRenderer parentSpriteRenderer;
  //  SpriteRenderer childFXSpriteRenderer;
    [SerializeField] GameObject _FX;


    private void Start()
    {
        parentSpriteRenderer = GetComponentInParent<SpriteRenderer>();
       // childFXSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
       
        parentSpriteRenderer.enabled = true;
      //  childFXSpriteRenderer.enabled = true;
        _FX.SetActive(false);
    }

    public void PlayBurst()
    {
        float delay = 0.5f;
       // childFXSpriteRenderer.enabled = false;
        parentSpriteRenderer.enabled = false;
        _FX.SetActive(true);

        Destroy(gameObject, delay);
    }
}
