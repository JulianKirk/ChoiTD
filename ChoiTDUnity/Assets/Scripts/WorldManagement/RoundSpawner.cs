using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RoundSpawner : MonoBehaviour
{
    public GameData GameInfo = new GameData(50, 15
        ,new List<Round>() 
        {
            new Round(5, 0, 0),
            new Round(7, 0, 0),
            new Round(7, 3, 0),
            new Round(10, 2, 2),
            new Round(10, 5, 0),
            new Round(10, 5, 2),
            new Round(12, 5, 2),
            new Round(12, 5, 5),
            new Round(15, 10, 2),
            new Round(15, 10, 4),
            new Round(15, 10, 6),
            new Round(15, 10, 8),
            new Round(15, 10, 10),
            new Round(15, 12, 5),
            new Round(15, 12, 8),
            new Round(15, 15, 0),
            new Round(5, 15, 10),
            new Round(10, 10, 10),
            new Round(15, 15, 0),
            new Round(15, 0, 15),
            new Round(20, 20, 20)
        }
    
    );


    public TMP_Text RoundText;
    public int currentRoundNum = 0;

    private float spawnInterval = 0.25f;
    private int intervalIncreaseCounter = 0;

    public GameObject BasicEnemy;
    public GameObject TankyEnemy;
    public GameObject FastEnemy;

    public Transform EnemyContainer;
    public GameObject roundNotFinishedMessage;

    void Start() 
    {
        HealthManager.GameHealth = GameInfo.StartingLives;  
        HealthManager.UpdateHealthText();  

        CoinManager.TotalCoins = GameInfo.StartingCoins;
        CoinManager.UpdateCoinText();
    }

    public void StartNewRound() 
    {
        if (EnemyContainer.childCount != 0) 
        {
            StartCoroutine("DisplayRoundNotFinishedMessage");

            return;
        }

        currentRoundNum++;

        if (currentRoundNum > GameInfo.rounds.Count) 
        {
            HealthManager.GameWon = true;

            HealthManager.GameHealth = 1;
            CoinManager.TotalCoins = 0;
            TowerPlacementManager.TowerLocations.Clear();

            SceneManager.LoadScene("GameOverState");
        }

        intervalIncreaseCounter++;

        if (intervalIncreaseCounter >= 9) 
        {
            spawnInterval -= 0.05f;
            intervalIncreaseCounter = 0;
        }

        RoundText.text = "Current Round: " + currentRoundNum.ToString();

        Round currentRound = GameInfo.rounds[currentRoundNum - 1];

        StartCoroutine(SpawnRoundWithIntervals(currentRound.BasicEnemies, currentRound.TankyEnemies, currentRound.FastEnemies, spawnInterval));
    }

    IEnumerator SpawnRoundWithIntervals(int basicNum, int tankNum, int fastNum, float interval) 
    {
        Vector3 spawnPosition = new Vector3(MapGenerator.MapPath[0].xPos, MapGenerator.MapPath[0].yPos, 0);  
    
        for (int b = 0; b < basicNum; b++) 
        {
            Instantiate(BasicEnemy, spawnPosition, Quaternion.identity, EnemyContainer);
            yield return new WaitForSeconds(interval);
        }

        for (int a = 0; a < tankNum; a++) 
        {
            Instantiate(TankyEnemy, spawnPosition, Quaternion.identity, EnemyContainer);
            yield return new WaitForSeconds(interval);
        }

        for (int r = 0; r < fastNum; r++) 
        {
            Instantiate(FastEnemy, spawnPosition, Quaternion.identity, EnemyContainer);
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator DisplayRoundNotFinishedMessage() 
    {
        roundNotFinishedMessage.SetActive(true);

        yield return new WaitForSeconds(1f);

        roundNotFinishedMessage.SetActive(false);
    }
}
