using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

    public float despawnTimer = 1f; //Can change for rockets and such

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.layer == 6) //6 is the enemy layer
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            
            Destroy(gameObject);
        }
    }

    void Update() 
    {
        despawnTimer -= Time.deltaTime;

        if (despawnTimer <= 0) 
        {
            Destroy(gameObject);
        }
    }
}