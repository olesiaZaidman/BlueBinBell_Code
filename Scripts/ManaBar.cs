using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] Mana mana;
    Slider slider;
    GameManager gameManager;


    void Awake()
    {
        slider = GetComponent<Slider>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        SetMaxManaBar(mana.maxManaPoints);
        SetManaBar(mana.GetManaPoints());
    }

    void Update()
    {
        SetManaBar(mana.GetManaPoints()); 
    }

    public void SetMaxManaBar(int _mana)
    {
        if (gameManager.gameLevel < 2)
        {
            slider.maxValue = _mana;
            slider.value = _mana;
        }

        else
            slider.maxValue = _mana;
    }

    public void SetManaBar(int _mana)
    {
        slider.value = _mana;
    }
}
