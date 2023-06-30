using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonFunctions : MonoBehaviour
{
    public void OpenMainMenu() 
    {
        HealthManager.GameHealth = 1;
        CoinManager.TotalCoins = 0;
        TowerPlacementManager.TowerLocations.Clear();
        MapGenerator.MapPath.Clear();

        Destroy(GameObject.Find("Music"));

        SceneManager.LoadScene(0);
    }

    public void PlayGame() 
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}