using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerSelectionManager : MonoBehaviour
{
    public TMP_Text CurrentTowerSelectionText;
    public Button BasicTowerButton;
    public Button AdvancedTowerButton;
    public Button RocketTowerButton;

    public enum TowerType
    {
        Basic,
        Advanced,
        Rocket
    }

    public static TowerType currentlySelectedTower;

    void Awake() 
    {
        BasicTowerButton.onClick.AddListener(() => { SelectTower(TowerType.Basic);});
        AdvancedTowerButton.onClick.AddListener(() => { SelectTower(TowerType.Advanced);});
        RocketTowerButton.onClick.AddListener(() => { SelectTower(TowerType.Rocket);});

        currentlySelectedTower = TowerType.Basic;
    }

    public void SelectTower(TowerType tower)
    {
        currentlySelectedTower = tower;

        switch(tower) 
        {
            case TowerType.Basic:
                CurrentTowerSelectionText.text = "Turret Selected: Basic";
                break;
            case TowerType.Advanced:
                CurrentTowerSelectionText.text = "Turret Selected: Advanced";
                break;
            case TowerType.Rocket:
                CurrentTowerSelectionText.text = "Turret Selected: Rocket";
                break;
            default:
                break;
        }
    }
}