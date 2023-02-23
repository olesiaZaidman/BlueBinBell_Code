using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [Header("Coin")]
    [SerializeField] GameObject _coinPrefab;
    float spawnRange = 2.3f;
    public int minNumberofCoins = 1;
    public int maxNumberofCoins = 15;
    //AudioManager audioManager;
    ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
     //   audioManager = FindObjectOfType<AudioManager>();
    }

    public void DropItems()
    {
        int numberOfCoins = Random.Range(minNumberofCoins, maxNumberofCoins);
        int index = 0;
      //  Debug.Log("numberOfCoins" + numberOfCoins);
        scoreManager.SaveAmountOfCoinsDroped(numberOfCoins);
        scoreManager.IncreaseCoinScore();
     //   audioManager.PlayPickUpCoinsSound();

        for (int i = 0; i < numberOfCoins; i++) //we create in one moment as many enemies as = enemyWaveIndex
        {
            GameObject coin = Instantiate(_coinPrefab, GenerateSpawnPosition(), _coinPrefab.transform.rotation);
            coin.name = "Coin" + index;
            //   Debug.Log("numberOfCoins"+numberOfCoins);
            index++;         
        }
    }

    private Vector2 GenerateSpawnPosition()
    {
        float xSpawnPos = Random.Range(-spawnRange, spawnRange);
        float ySpawnPos = Random.Range(-spawnRange, spawnRange);
        Vector2 randomPosition = new Vector2(transform.position.x+xSpawnPos, transform.position.y+ ySpawnPos);

        return randomPosition;
    }
}
