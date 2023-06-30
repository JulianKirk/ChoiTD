using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    private void Start() 
    {
        Camera.main.orthographicSize = (MapGenerator.MapHeight) / 2f;

        transform.position = new Vector3(MapGenerator.MapWidth/2 - 0.5f, MapGenerator.MapHeight/2 - 0.5f, transform.position.z);
    }
}
