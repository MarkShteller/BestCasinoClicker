using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerSaveData
{
    public float totalBalance { get; set; }
    public List<Building> availableBuildings { get; set; }
    public List<Upgrade> availableUpgrades { get; set; }
    public List<Achievement> availableAchievements { get; set; }

    public PlayerSaveData(float totalBalance, List<Building> availableBuildings, List<Upgrade> availableUpgrades, List<Achievement> availableAchievements)
    {
        this.availableBuildings = availableBuildings;
        this.availableUpgrades = availableUpgrades;
        this.totalBalance = totalBalance;
        this.availableAchievements = availableAchievements;
    }
}

