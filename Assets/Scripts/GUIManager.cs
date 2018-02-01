using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    public static GUIManager Instance;

    public Text playerBalanceText;

    public Transform BuildingButtonsHolder;
    public Transform UpgradeButtonsHolder;

    public FloorContainer floorPrefab;
    public UpgradeContainer upgradePrefab;
    //public Button BuildingButton;
    //public UpgradeUIButton UpgradeButton;
    private List<Button> buildingButtons;

    private List<FloorContainer> floorContainers;
    private List<UpgradeContainer> upgradeContainers;

    public GameObject upgradeStoreObject;
    public GameObject newUpgradeAlert;
    public GameObject rewardAmountText;
    public Transform canvas;

    //private List<UpgradeUIButton> upgradeButtons;

    void Start ()
    {
        Instance = this;
        buildingButtons = new List<Button>();
        floorContainers = new List<FloorContainer>();
        upgradeContainers = new List<UpgradeContainer>();
        //upgradeButtons = new List<UpgradeUIButton>();
    }

    public void UpdatePlayerBalance(float balance)
    {
        playerBalanceText.text = Mathf.Floor(balance).ToString("N0")+"$";
    }

    public void RevealBuilding(Building building, Action action)
    {
        /*Button newBuildingButton = Instantiate(BuildingButton, BuildingButtonsHolder);
        newBuildingButton.GetComponentInChildren<Text>().text = building.Name +"\nCost: " + building.TotalPrice + "\nAmount: " + building.CurrentAmount;
        newBuildingButton.onClick.AddListener(delegate { action(); });
        buildingButtons.Add(newBuildingButton);*/

        FloorContainer newFloor = Instantiate(floorPrefab, BuildingButtonsHolder);
        newFloor.Init(building.Name, building.TotalPrice, building.CurrentAmount, building.Icon, action, building);
        newFloor.transform.SetAsFirstSibling();
        floorContainers.Add(newFloor);
    }

    public void RevealUpgrade(Upgrade upgrade, Action action)
    {
        /*UpgradeUIButton newUpgradeButton = Instantiate(UpgradeButton, UpgradeButtonsHolder);
        newUpgradeButton.UpgradeData = upgrade;
        newUpgradeButton.GetComponentInChildren<Text>().text = upgrade.Name + "\nCost: " + upgrade.Price;
        newUpgradeButton.onClick.AddListener(delegate { action(); });
        upgradeButtons.Add(newUpgradeButton);*/

        UpgradeContainer newUpgrade = Instantiate(upgradePrefab, UpgradeButtonsHolder);
        newUpgrade.Init(upgrade.Name, upgrade.Price, upgrade.Description, upgrade.Icon, action, upgrade);
        upgradeContainers.Add(newUpgrade);
        newUpgradeAlert.SetActive(true);
    }

    public void UpdateBuildingButtonText(Building building)
    {
        int buildingButtonIndex = building.Id % 100 - 1;
        Debug.Log("Updating button text for index: " + buildingButtonIndex);
        //buildingButtons[buildingButtonIndex].GetComponentInChildren<Text>().text = building.Name + "\nCost: " + building.TotalPrice + "\nAmount: " + building.CurrentAmount;
        floorContainers[buildingButtonIndex].UpdateLables(building.TotalPrice, building.CurrentAmount);
    }

    public void RemoveUpgrade(Upgrade upgrade)
    {
        int upgradeButtonId = upgrade.Id;
        foreach (UpgradeContainer button in upgradeContainers)
        {
            if(button.upgrade.Id == upgradeButtonId)
            {
                Destroy(button.gameObject);
                break;
            }
        }
    }

    public void CloseUpgradeStore()
    {
        upgradeStoreObject.SetActive(false);
        newUpgradeAlert.SetActive(false);

    }

    public void OpenUpgradeStore()
    {
        upgradeStoreObject.SetActive(true);
        newUpgradeAlert.SetActive(false);

    }

    public void RevealAchievement(Achievement achiv)
    {
        Debug.Log("Achievement unlocked: "+ achiv.Name);
    }

    public void ShowSpinAmountWon(float addedAmount, Vector2 position)
    {
        GameObject rewardText = Instantiate(rewardAmountText, canvas);
        rewardText.GetComponentInChildren<Text>().text = addedAmount.ToString("N0") + "$";
        rewardText.transform.localPosition = position;
        //Debug.Log(position);
    }
}

