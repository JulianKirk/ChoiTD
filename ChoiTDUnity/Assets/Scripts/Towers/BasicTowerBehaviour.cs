using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTowerBehaviour : MonoBehaviour
{
    public float range;
    public float projectileSpeed;

    public float timeBetweenShots;
    private float remainingTimeToShot;

    public GameObject Projectile;
    public Transform ProjectileSpawnTransform;

    private Collider2D[] nearbyTargets;

    private Collider2D currentTarget;

    public LayerMask EnemyMask;

    void Start() 
    {
        remainingTimeToShot = timeBetweenShots;
    }

    void Update() 
    {
        if (Physics2D.OverlapCircle(transform.position, range, EnemyMask)) 
        {
            nearbyTargets = Physics2D.OverlapCircleAll(transform.position, range, EnemyMask);

            float highestDistance = 0;
            Collider2D newTarget = nearbyTargets[0];

            foreach (Collider2D target in nearbyTargets) 
            {
                float targetDistanceTraveled = target.gameObject.GetComponent<PathFollower>().distanceTraveled;

                if (targetDistanceTraveled > highestDistance) 
                {
                    highestDistance = targetDistanceTraveled;
                    newTarget = target;
                }
            }

            currentTarget = newTarget;

            Swivel();

            if (remainingTimeToShot <= 0) 
            {
                Shoot();
                remainingTimeToShot = timeBetweenShots;
            }
        }

        remainingTimeToShot -= Time.deltaTime;
    }

    void Shoot() 
    {
        GameObject newProjectile = Instantiate(Projectile, ProjectileSpawnTransform.position, ProjectileSpawnTransform.rotation);
        newProjectile.GetComponent<Rigidbody2D>().velocity = ProjectileSpawnTransform.up * projectileSpeed;
    }

    void Swivel() 
    {
        //Because transform.LookAt() doesn't work in 2D
        Quaternion newRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position, transform.TransformDirection(Vector3.back));

        transform.rotation = new Quaternion(0, 0, newRotation.z, newRotation.w);
    }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.white;
    //     Gizmos.DrawWireSphere(transform.position, range);
    // }

    void OnMouseOver() {
        transform.GetChild(0).gameObject.SetActive(true);    
    }

    void OnMouseExit() {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}