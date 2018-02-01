using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeContainer : MonoBehaviour {


    public Text priceLable;
    public Text nameLable;
    public Text descriptionLable;
    public Image imageBG;
    public Button buyButton;
    public Upgrade upgrade;


    public void Init(string name, float price, string description, string imageName, Action buyAction, Upgrade upgrade)
    {
        nameLable.text = name;
        priceLable.text = price.ToString("N0") + "$";
        descriptionLable.text = description;
        imageBG.sprite = Resources.Load<Sprite>(imageName);
        buyButton.onClick.AddListener(delegate { buyAction(); });
        this.upgrade = upgrade;
    }

}
