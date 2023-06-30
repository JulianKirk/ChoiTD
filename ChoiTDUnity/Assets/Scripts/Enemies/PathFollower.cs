using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFollower : MonoBehaviour
{
    private Rigidbody2D rBody;

    private PathNode currentTarget;
    private int currentPathIndex;

    public float m_speed;

    public float distanceTraveled; //So the towers can target the one farthest along the path

    void Awake() 
    {
        rBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start() 
    {
        currentPathIndex = 0;
        currentTarget = MapGenerator.MapPath[0];

        transform.position = new Vector3(currentTarget.xPos, currentTarget.yPos, 0);

        distanceTraveled = 0;
        
        //Start the path
        Vector2 newVel = new Vector2(currentTarget.xPos - transform.position.x, currentTarget.yPos - transform.position.y);
        newVel.Normalize();
        rBody.velocity = newVel * m_speed;
    }

    void Update() 
    {
        FollowPath();

        distanceTraveled += m_speed * Time.deltaTime;
    }

    void FollowPath() 
    {
        if (MapGenerator.MapPath.Count != 0) 
        {
            if (Mathf.Abs(currentTarget.xPos - transform.position.x) < 0.15 && Mathf.Abs(currentTarget.yPos - transform.position.y) < 0.15)
            // if (((new Vector2(currentTarget.xPos, currentTarget.yPos)) - (Vector2)transform.position).magnitude < 0.25)
            {
                if(currentPathIndex != MapGenerator.MapPath.Count - 1) 
                {
                    currentPathIndex++;
                    currentTarget = MapGenerator.MapPath[currentPathIndex];

                    Vector2 newVel = new Vector2(currentTarget.xPos - transform.position.x, currentTarget.yPos - transform.position.y);
                    newVel.Normalize();
                    rBody.velocity = newVel * m_speed;
                } else 
                {
                    HealthManager.LoseHealth(5);

                    Destroy(gameObject);
                }
            }
        }
    }
}