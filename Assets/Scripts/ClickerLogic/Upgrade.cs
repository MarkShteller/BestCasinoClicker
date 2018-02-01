public class Upgrade : AssetType
{
    private float price;
    private float bonusToCpS;
    private float bonusToPrice;
    private int itemID;
    private int revealAtAmount;
    private bool isBought;

    public float Price
    {
        get
        {
            return price;
        }

        set
        {
            price = value;
        }
    }

    public float BonusToCpS
    {
        get
        {
            return bonusToCpS;
        }

        set
        {
            bonusToCpS = value;
        }
    }

    public float BonusToPrice
    {
        get
        {
            return bonusToPrice;
        }

        set
        {
            bonusToPrice = value;
        }
    }

    public int ItemID
    {
        get
        {
            return itemID;
        }

        set
        {
            itemID = value;
        }
    }

    public int RevealAtAmount
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

    public bool IsBought
    {
        get
        {
            return isBought;
        }

        set
        {
            isBought = value;
        }
    }

    public Upgrade(int id, string name, string description, string icon, int itemID, float price, float bonusToCpS, float bonusToPrice, int revealAtAmount) : base(id, name, description, icon)
    {
        this.BonusToCpS = bonusToCpS;
        this.BonusToPrice = bonusToPrice;
        this.ItemID = itemID;
        this.revealAtAmount = revealAtAmount;
        this.price = price;
        this.IsBought = false;
    }
}