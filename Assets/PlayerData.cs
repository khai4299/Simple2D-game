using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData :MonoBehaviour
{
    public static float heath=100f;
    public static int money=100;
    public static float speed=10f;

    public void SetDefaut()
    {
        heath = 100f;
        money = 100;
        speed = 10f;
    }
}
