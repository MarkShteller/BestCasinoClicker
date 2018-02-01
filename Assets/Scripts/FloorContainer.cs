using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorContainer : MonoBehaviour {

    public Text priceLable;
    public Text nameLable;
    public Text amountLable;
    public SpriteRenderer imageBG;
    public Button buyButton;
    private Building building;


    public void Init(string name, float price, int amount, string imageName, Action buyAction, Building building)
    {
        nameLable.text = name;
        priceLable.text = price.ToString("N0") + "$";
        amountLable.text = amount.ToString("N0");
        //imageBG.sprite = Resources.Load<Sprite>(imageName);
        buyButton.onClick.AddListener(delegate{ buyAction(); });
        this.building = building;
    }
	
    public void UpdateLables(float price, int amount)
    {
        priceLable.text = price.ToString("N0") + "$";
        amountLable.text = amount.ToString("N0");
    }

}
