using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    GameManager gameManager;
    AudioManager audioManager;

    [Header("Text UI")]
    [SerializeField] TextMeshProUGUI coinsCountText;
    [SerializeField] TextMeshProUGUI spellCountText;

    [Header("Coin Points")]
    int finalCoinPoints;
    int scoreCoins;
    int newCoins = 0;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        GetAndSetScorePointsForCurrentLevel();
        SetScoreText(coinsCountText, finalCoinPoints, finalCoinPoints); //coins
    }

    
    public void SetScoreText(TextMeshProUGUI _text, int _points, int _finalPoints)
    {
        _text.SetText(_points.ToString());

        if (!gameManager.isGameOver)
        {
            _finalPoints = _points;
        }
    }


    public void SaveAmountOfCoinsDroped(int _value)  //in ItemDrop class
    {
        newCoins = _value;
    }

    public void IncreaseCoinScore()
    {
        scoreCoins += newCoins;
        audioManager.PlayPickUpSound();
        SetScoreText(coinsCountText, scoreCoins, finalCoinPoints); //coins
        Debug.Log("Score Coins: " + scoreCoins);
    }

    void GetAndSetScorePointsForCurrentLevel()
    {
        if (gameManager.gameLevel < 2)
        {
            SetScorePoints(0);
        }
        else
        {
            SetScorePoints(DataBetweenLevels.coinScore);
        }
    }

    public int GetScorePoints()
    {
        Debug.Log("Score: " + scoreCoins);
        return scoreCoins;
    }

    void SetScorePoints(int _points)
    {
        scoreCoins = _points;
        finalCoinPoints = _points;
    }






    //[Header("Spell Points")]
    //int finalSpellPoints = 0;
    //int scoreSpells = 0;

    //public int SpellScorePoints
    //{
    //    get
    //    {
    //        return scoreSpells;
    //    }
    //}

    //Start
    //{
    //  SetScoreText(spellCountText, finalSpellPoints, finalSpellPoints); //spells
    //}

    //public void IncreaseSpellScore()
    //{
    //    scoreSpells++;
    //    // gameEffectsAudio.PlayOneShot(plusOneSound, 1.0f);
    //    Debug.Log("Score Spells: " + scoreSpells);
    //    SetScoreText(spellCountText, scoreSpells, finalSpellPoints);
    //}

    //public void DecreaseSpellScore()
    //{
    //    if (scoreSpells > 0)
    //    {
    //        scoreSpells--;
    //        // gameEffectsAudio.PlayOneShot(plusOneSound, 1.0f);
    //        Debug.Log("Score Spells: " + scoreSpells);
    //        SetScoreText(spellCountText, scoreSpells, finalSpellPoints);
    //    }

    //}


}
