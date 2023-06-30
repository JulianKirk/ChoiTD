using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TowerPlacementManager : MonoBehaviour
{
    MapGenerator mapGenerator;

    public GameObject notPlaceableMessage;
    public GameObject notBuyableMessage;

    public GameObject BasicTower;
    public GameObject AdvancedTower;
    public GameObject RocketTower;

    public int BasicPrice = 10;
    public int AdvancedPrice = 20;
    public int RocketPrice = 50;

    public static HashSet<Vector3> TowerLocations = new HashSet<Vector3>(); //Stores the locations of towers

    void Start() 
    {
        mapGenerator = gameObject.GetComponent<MapGenerator>();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))  
        {
            Vector3 locationClicked = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

            if (!(locationClicked.x > (MapGenerator.MapWidth - 0.5f) || locationClicked.x < -0.5f
                || locationClicked.y > (MapGenerator.MapWidth - 0.5f) || locationClicked.y < -0.5f)) //Nothing happens if not clicking on the screen
            {
                //Because the actual tilemap is offset by 0.5
                //Check if clicking on a path
                if (mapGenerator.tilemap.GetTile(new Vector3Int((int)(locationClicked.x + 0.5), (int)(locationClicked.y + 0.5))) == mapGenerator.PathTile)
                {
                    StartCoroutine("DisplayNotPlaceableMessage");
                }
                else 
                {
                    Vector3 realSpawnLocation = new Vector3((int)(locationClicked.x + 0.5), (int)(locationClicked.y + 0.5), 0);

                    if (!TowerLocations.Contains(realSpawnLocation)) //Cannot spawn towers on top of each other
                    {
                        switch(TowerSelectionManager.currentlySelectedTower) 
                        {
                            case TowerSelectionManager.TowerType.Basic:
                                if (!(CoinManager.TotalCoins < BasicPrice)) 
                                {
                                    CoinManager.AddCoins(-BasicPrice);
                                    Instantiate(BasicTower, realSpawnLocation, Quaternion.identity);
                                    TowerLocations.Add(realSpawnLocation);
                                }
                                else
                                {
                                    StartCoroutine("DisplayNotEnoughMoneyMessage");
                                }
                                break;
                            case TowerSelectionManager.TowerType.Advanced:
                                if (!(CoinManager.TotalCoins < AdvancedPrice)) 
                                {
                                    CoinManager.AddCoins(-AdvancedPrice);
                                    Instantiate(AdvancedTower, realSpawnLocation, Quaternion.identity);
                                    TowerLocations.Add(realSpawnLocation);
                                }
                                else
                                {
                                    StartCoroutine("DisplayNotEnoughMoneyMessage");
                                }
                                break;
                            case TowerSelectionManager.TowerType.Rocket:
                                if (!(CoinManager.TotalCoins < RocketPrice)) 
                                {
                                    CoinManager.AddCoins(-RocketPrice);
                                    Instantiate(RocketTower, realSpawnLocation, Quaternion.identity);
                                    TowerLocations.Add(realSpawnLocation);
                                }
                                else
                                {
                                    StartCoroutine("DisplayNotEnoughMoneyMessage");
                                }
                                break;
                            default:
                                break;
                        }
                    } 
                    else 
                    {
                        StartCoroutine("DisplayNotPlaceableMessage");
                    }
                }
            }

            
        }
    }

    IEnumerator DisplayNotPlaceableMessage() 
    {
        notBuyableMessage.SetActive(false);
        notPlaceableMessage.SetActive(true);

        yield return new WaitForSeconds (1f);

        notPlaceableMessage.SetActive(false);
    }

    IEnumerator DisplayNotEnoughMoneyMessage()
    {
        notPlaceableMessage.SetActive(false);
        notBuyableMessage.SetActive(true);

        yield return new WaitForSeconds (1f);

        notBuyableMessage.SetActive(false);
    }
}