using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableOnCanBuy : MonoBehaviour {

    //private GameManager gm;
    public Text amount;
    public Button button;

	// Use this for initialization
	void Start () {
        //gm = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        string s = amount.text.Substring(amount.text.Length - 1);
        float price = 0;
        if (s.Equals("$"))
            price = float.Parse(amount.text.Substring(0, amount.text.Length - 1));
        else
            price = float.Parse(amount.text);

        if (price <= GameManager.Instance.totalBalance)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
	}
}
