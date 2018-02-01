using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : AssetType {

    private int currentAmount;
    private float baseCpS;
    private float basePrice;
    private float revealAtAmount;
    private Upgrade[] upgrades;

    public float TotalPrice { get { return Mathf.Round(BasePrice * Mathf.Pow(1.15f, currentAmount)); } }

    public float TotalCps { get { return baseCpS * CalcTotalUpgradeBonus() * currentAmount; } }

    public Building(int id, string name, string description, string icon, float baseCps, float basePrice, float revealAtAmount) : base(id, name, description, icon)
    {
        this.BaseCpS = baseCps;
        this.BasePrice = basePrice;
        this.RevealAtAmount = revealAtAmount;
        this.currentAmount = 0;
    }

    public float CalcTotalUpgradeBonus()
    {
        float bonus = 1;
        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.IsBought)
                bonus += upgrade.BonusToCpS;
        }
        return bonus;
    }

    public float BaseCpS
    {
        get
        {
            return baseCpS;
        }

        set
        {
            baseCpS = value;
        }
    }

    public float BasePrice
    {
        get
        {
            return basePrice;
        }

        set
        {
            basePrice = value;
        }
    }

    public float RevealAtAmount
    {
        get
        {
            return revealAtAmount;
        }

        set
        {
            revealAtAmount = value;
        }
    }

    public Upgrade[] Upgrades
    {
        get
        {
            return upgrades;
        }

        set
        {
            upgrades = value;
        }
    }

    public int CurrentAmount
    {
        get
        {
            return currentAmount;
        }

        set
        {
            currentAmount = value;
        }
    }
}
