using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public GameObject gameMaster;
    public Image image;
    public float startHeath = 100f;
    public float enemyHeath;
    public float damage = 10f;
    void Start()
    {
        enemyHeath = startHeath;
        gameMaster = GameObject.Find("GameMaster");
        gameMaster.GetComponent<GameMaster>().onUpgradeMenu += OnUpgradeMenuToggle;
    }
    void Update()
    {

    }
    void OnUpgradeMenuToggle(bool active)
    {
        if (this!=null)
        {
            GetComponent<EnemyAI>().enabled = !active;
        }
    }
    public void Damage(float amount)
    {
        enemyHeath -= amount;
        image.fillAmount = enemyHeath / startHeath;
        if (enemyHeath <= 0)
        {
            gameMaster.GetComponent<GameMaster>().EnemyDie(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player!=null)
        {
            player.Damage(damage);
            gameMaster.GetComponent<GameMaster>().GetComponent<CameraShake>().Shake(0.1f, 0.2f);
            Damage(9999f);
        }
    }
    private void OnDestroy()
    {
        //gameMaster.GetComponent<GameMaster>().onUpgradeMenu -= OnUpgradeMenuToggle;
    }
}
