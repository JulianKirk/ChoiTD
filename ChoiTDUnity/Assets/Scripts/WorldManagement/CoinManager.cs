using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static int TotalCoins;
    public static TMP_Text coinText;

    void Awake() 
    {
        coinText = GameObject.Find("CoinsIndicator").GetComponent<TMP_Text>();
    }

    public static void AddCoins(int amt) //Just add negative amounts to remove coins 
    {
        TotalCoins += amt;

        UpdateCoinText();
    }

    public static void UpdateCoinText() 
    {
        coinText.text = "Coins: " + TotalCoins.ToString();
    }
}