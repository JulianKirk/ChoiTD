using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int StartingCoins;

    public int StartingLives;

    public List<Round> rounds = new List<Round>();

    public GameData(int lives, int coins, List<Round> rnds) 
    {
        StartingCoins = coins;
        StartingLives = lives;
        rounds = rnds;
    }
}