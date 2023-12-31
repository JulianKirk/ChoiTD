using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float m_health;
    public int coinValue;
    
    public void TakeDamage(float damage) 
    {
        m_health -= damage;

        if (m_health <= 0) 
        {
            CoinManager.AddCoins(coinValue);
            Destroy(gameObject);
        }
    }
}
