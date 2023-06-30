using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public TMP_Text winText;

    void Awake() 
    {
        if (HealthManager.GameWon) 
        {
            winText.text = "You Won! Good Job :)";
        } 
        else 
        {
            winText.text = "You Lost! On Round " + HealthManager.lossRound + " :(";
        }
    }
}