using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Achievement : AssetType
{
    private int requiredAsset;
    private int amount;
    private bool isMet;

    public int RequiredAsset
    {
        get
        {
            return requiredAsset;
        }

        set
        {
            requiredAsset = value;
        }
    }

    public int RequiredAmount
    {
        get
        {
            return amount;
        }

        set
        {
            amount = value;
        }
    }

    public bool IsMet
    {
        get
        {
            return isMet;
        }

        set
        {
            isMet = value;
        }
    }

    public Achievement(int id, string name, string description, string icon, int requiredAsset, int requiredAmount, bool isMet) : base(id, name, description, icon)
    {
        this.RequiredAmount = requiredAmount;
        this.RequiredAsset = requiredAsset;
        this.IsMet = isMet;
    }
}

