using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public int ObstacleChance; //In percentage

    public Tilemap tilemap;

    public Tile PathTile;
    public Tile GroundTile;

    public static List<PathNode> MapPath;

    public static int MapHeight;
    public static int MapWidth;


    // Start is called before the first frame update
    void Awake()
    {
        GenerateMap(20, 20);
    }

    void GenerateMap(int mapWidth, int mapHeight) 
    {
        PathNode[,] nodeGrid = new PathNode[mapWidth, mapHeight];

        MapHeight = mapHeight;
        MapWidth = mapWidth;

        for (int y = 0; y < mapHeight; y++) 
        {
             for (int x = 0; x < mapWidth; x++) 
            {
                bool walkable = Random.Range(0, 100) > ObstacleChance;

                nodeGrid[x, y] = new PathNode(x, y, walkable);
            }
        }

        int mapIterationCount = 0;
        while (MapPath == null)
        {
            PathNode startNode = nodeGrid[0, Random.Range(0, mapHeight)];

            while (!startNode.walkable) 
            {
                startNode = nodeGrid[0, Random.Range(0, mapHeight)];
            }

            PathNode endNode = nodeGrid[mapWidth - 1, Random.Range(0, mapHeight)];

            while (!endNode.walkable) 
            {
                startNode = nodeGrid[0, Random.Range(0, mapHeight)];
            }

            MapPath = AstarPathfinding.instance.GeneratePath(startNode, endNode, nodeGrid);

            mapIterationCount++;

            if (mapIterationCount > 25) //Resets impossible maps
            {
                GenerateMap(mapWidth, mapHeight);
                return;
            }
        }

        for (int y = 0; y < mapHeight; y++) 
        {
             for (int x = 0; x < mapWidth; x++) 
            {
                if (MapPath.Contains(nodeGrid[x, y])) 
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), PathTile);
                }
                else 
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), GroundTile);
                }
            }
        }
    }
}
