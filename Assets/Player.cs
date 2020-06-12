using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject gameMaster;
    public Image heathBar;
    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.Find("GameMaster");
        gameMaster.GetComponent<GameMaster>().onUpgradeMenu += OnUpgradeMenuToggle;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Damage(9999);
        }
        if (transform.position.y<=-15)
        {
            Damage(Mathf.Infinity);
        }
    }
    void OnUpgradeMenuToggle(bool active)
    {
        if (this != null)
        {
            GetComponent<Platformer2DUserControl>().enabled = !active;
            Weapon weapon = GetComponentInChildren<Weapon>();
            if (weapon != null)
            {
                weapon.enabled = !active;
            }
        }
    }
    public void Damage(float amount)
    {
        PlayerStat.instance.heath -= amount;
        heathBar.fillAmount = PlayerStat.instance.heath / PlayerData.heath;
        if (PlayerStat.instance.heath<=0)
        {
            gameMaster.GetComponent<GameMaster>().PlayerDie(this);
        }
    }
}
