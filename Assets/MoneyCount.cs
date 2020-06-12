using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyCount : MonoBehaviour
{
    public Text moneyText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + PlayerData.money.ToString();
    }
}
