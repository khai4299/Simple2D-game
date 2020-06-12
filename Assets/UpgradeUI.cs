using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeUI : MonoBehaviour
{
    public Text heathText;
    public Text speedText;
    public Text damageText;
    public float heathMultiplier = 1.3f;
    public float speedMultiplier = 2f;
    public int upgradeCost = 50;
    private void OnEnable()
    {
        UpdateValue();
    }
    void UpdateValue()
    {
        heathText.text = "HEATH: " + PlayerData.heath.ToString();
        speedText.text = "SPEED: " + PlayerData.speed.ToString();
    }
    public void UpgradeHeath()
    {
        if (PlayerStat.instance.money < upgradeCost)
            return;
        PlayerData.money -= upgradeCost;
        PlayerData.heath = (int)(PlayerData.heath * heathMultiplier);
        UpdateValue();
    }
    public void UpgradeSpeed()
    {
        if (PlayerStat.instance.money < upgradeCost)
            return;
        PlayerData.money -= upgradeCost;
        PlayerData.speed = (int)(PlayerData.speed + speedMultiplier);
        UpdateValue();
    }
}
