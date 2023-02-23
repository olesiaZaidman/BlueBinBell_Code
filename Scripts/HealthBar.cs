using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Health health;
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        gameManager = FindObjectOfType<GameManager>();
    }

     void Start()
    {
        SetMaxHealthBar(health.maxHealthPoints);
        SetHealthBar(health.GetHealthPoints());
    }

     void Update()
    {
        SetHealthBar(health.GetHealthPoints());
    }

    public void SetMaxHealthBar(int health)
    {
        if (gameManager.gameLevel < 2)
        {
            slider.maxValue = health;
            slider.value = health;
        }

        else 
            slider.maxValue = health;
    }

    public void SetHealthBar(int health)
    {
        slider.value = health;
    }
}
