using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat: MonoBehaviour
{
    public static PlayerStat instance;
    public float heath;
    public int money;
    private void Start()
    {
        heath = PlayerData.heath;
        money = PlayerData.money;
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
