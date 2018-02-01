using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public GameRules GameRules;

    public float totalBalance;

    public List<Building> availableBuildings;
    public List<Upgrade> availableUpgrades;
    public List<Achievement> availableAchievements;

    public Animator MachineAnimator;
    public Image[] SlotIcons;
    public Sprite[] IconPool;

    private const string saveDataLoc = "PlayerData";

	void Start ()
    {
        Instance = this;
        totalBalance = 0; // load this from player save json 
        string rulesSTR = Resources.Load<TextAsset>("GameRules").text;
        Debug.Log("GAME RULES:\n"+rulesSTR);

        GameRules = JsonConvert.DeserializeObject<GameRules>(rulesSTR);
        Debug.Log("Deserialize successful! \nbuilding upgrade: "+ GameRules.Buildings[0].Upgrades[0].Name);
        //Debug.Log(GameRules.Achievements[0].Name);

        availableBuildings = new List<Building>();
        availableUpgrades = new List<Upgrade>();
        availableAchievements = new List<Achievement>();

        if (PlayerPrefs.HasKey(saveDataLoc))
        {
            Debug.Log("Loading player save data....");
            LoadPlayerData();
        }

        StartCoroutine(EventInvokingCoroutine());
        StartCoroutine(BalanceUpdaterCoroutine());
        StartCoroutine(AutoSaveGame());
	}

    public IEnumerator BalanceUpdaterCoroutine()
    {
        while (true)
        {
            foreach (Building building in availableBuildings)
            {
                totalBalance += building.TotalCps / 30;
            }
            GUIManager.Instance.UpdatePlayerBalance(totalBalance);
            yield return new WaitForSeconds(1f / 30f);
        }
    }

    public IEnumerator EventInvokingCoroutine()
    {
        while (true)
        {
            foreach (Building rulesBuilding in GameRules.Buildings)
            {
                bool existsBuilding = false;
                foreach (Building savedBuilding in availableBuildings)
                {
                    if (rulesBuilding.Id == savedBuilding.Id)
                        existsBuilding = true;
                }
                if (rulesBuilding.RevealAtAmount <= totalBalance && !existsBuilding)
                {
                    RevealBuilding(rulesBuilding);
                }

                foreach (Upgrade upgrade in rulesBuilding.Upgrades)
                {
                    bool existsUpgrade = false;
                    foreach (Upgrade savedUpgrade in availableUpgrades)
                    {
                        if (upgrade.Id == savedUpgrade.Id)
                            existsUpgrade = true;
                    }
                    if (upgrade.RevealAtAmount <= totalBalance && !existsUpgrade)
                    {
                        RevealUpgrade(upgrade);
                    }
                }
            }
            /*
            foreach (Achievement achiv in GameRules.Achievements)
            {
                bool exists = false;
                foreach (Achievement availAchiv in availableAchievements)
                {
                    if (availAchiv.Id == achiv.Id)
                    {
                        exists = true;
                        break;
                    }
                }
                if (exists)
                    continue;

                foreach (Building building in availableBuildings)
                {
                    if (building.Id == achiv.RequiredAsset)
                    {
                        if (achiv.RequiredAmount >= building.CurrentAmount)
                        {
                            achiv.IsMet = true;
                            availableAchievements.Add(achiv);
                            GUIManager.Instance.RevealAchievement(achiv);
                        }
                    }
                }
            }
            */
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator AutoSaveGame()
    {
        while (true)
        {
            SavePlayerData();
            yield return new WaitForSeconds(10);
        }
    }

    private void LoadPlayerData()
    {
        PlayerSaveData psd = JsonConvert.DeserializeObject<PlayerSaveData>(PlayerPrefs.GetString(saveDataLoc));
        totalBalance = psd.totalBalance;
        GUIManager.Instance.UpdatePlayerBalance(totalBalance);

        availableBuildings = psd.availableBuildings;
        availableUpgrades = psd.availableUpgrades;
        availableAchievements = psd.availableAchievements;

        foreach (Building building in availableBuildings)
        {
            GUIManager.Instance.RevealBuilding(building, () => AddBuildingAction(building));
        }
        foreach (Upgrade upgrade in availableUpgrades)
        {
            if (!upgrade.IsBought)
            {
                GUIManager.Instance.RevealUpgrade(upgrade, () => BuyUpgradeAction(upgrade));
            }
        }
        foreach (Achievement achiv in availableAchievements)
        {
            GUIManager.Instance.RevealAchievement(achiv);
        }
    }

    private void SavePlayerData()
    {
        PlayerSaveData psd = new PlayerSaveData(totalBalance, availableBuildings, availableUpgrades, availableAchievements);
        string json = JsonConvert.SerializeObject(psd, Formatting.Indented);
        PlayerPrefs.SetString(saveDataLoc, json);
    }

    private void RevealUpgrade(Upgrade upgrade)
    {
        availableUpgrades.Add(upgrade);
        GUIManager.Instance.RevealUpgrade(upgrade, ()=> BuyUpgradeAction(upgrade));
    }

    public void BuyUpgradeAction(Upgrade upgrade)
    {
        if (totalBalance - upgrade.Price >= 0)
        {
            // add the upgrade to the building
            upgrade.IsBought = true;

            totalBalance -= upgrade.Price;
            GUIManager.Instance.UpdatePlayerBalance(totalBalance);
            //availableUpgrades.Remove(upgrade);
            GUIManager.Instance.RemoveUpgrade(upgrade);
        }
    }   

    private void RevealBuilding(Building building)
    {
        availableBuildings.Add(building);
        
        GUIManager.Instance.RevealBuilding(building, ()=> AddBuildingAction(building));
    }

    public void AddBuildingAction(Building building)
    {
        if (totalBalance - building.TotalPrice >= 0)
        {
            totalBalance -= building.TotalPrice;
            GUIManager.Instance.UpdatePlayerBalance(totalBalance);
            building.CurrentAmount++;
            GUIManager.Instance.UpdateBuildingButtonText(building);
        }
        else
        {
            Debug.Log("Not enough money.");
        }
    }

    public void ClickChip()
    {
        //if(MachineAnimator.get)
        float addedAmount = 1;
        foreach (Building building in availableBuildings)
        {
            addedAmount += building.TotalCps;
        }

        totalBalance += addedAmount;
        Vector2 pos = new Vector2(480, 685);
        
        /*if (Input.touchCount > 0)
            pos = Input.GetTouch(0).position;
        else
            pos = Input.mousePosition;
            */

        GUIManager.Instance.ShowSpinAmountWon(addedAmount, pos);
        GUIManager.Instance.UpdatePlayerBalance(totalBalance);

        //spin the machine
        MachineAnimator.SetTrigger("Spin");
    }

    public void ChangeOutIcons()
    {
        int rnd = (int)Mathf.Floor(UnityEngine.Random.Range(0, 10));
        Sprite savedIcon = SlotIcons[4].sprite;
        for (int i = 0; i < 3; i++)
        {
            SlotIcons[i].sprite = savedIcon;
        }
        for (int i = 3; i < 6; i++)
        {
            SlotIcons[i].sprite = IconPool[rnd];
        }
    }

}
