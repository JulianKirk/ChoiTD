using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public static int GameHealth;
    public static TMP_Text HealthText;

    public static bool GameWon;
    public static int lossRound;

    void Awake() {
        HealthText = GameObject.Find("LivesIndicator").GetComponent<TMP_Text>();
    }

    void Update() 
    {
        if (GameHealth <= 0) 
        {
            LoseGame();
        }    
    }

    public static void LoseHealth(int num) 
    {
        GameHealth -= num;
        UpdateHealthText();
    }

    public static void UpdateHealthText() 
    {
        HealthText.text = "Lives: " + GameHealth.ToString();
    }

    void LoseGame() 
    {
        GameHealth = 0;
        CoinManager.TotalCoins = 0;
        TowerPlacementManager.TowerLocations.Clear();
        
        GameWon = false;

        lossRound = gameObject.GetComponent<RoundSpawner>().currentRoundNum;

        //Load into the next scene
        SceneManager.LoadScene("GameOverState");
    }

}