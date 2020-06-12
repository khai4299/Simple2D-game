using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject playerdeathEffect;
    public GameObject enemydeathEffect;
    public Transform spawnPos;
    public float timeRepawn;
    public GameObject playerPrefab;
    public GameObject spawnEffect;
    public GameObject gameOverUI;
    public int live = 3;
    public static int currentLive;
    public GameObject upgradeMenuUI;
    public WaveSpawn waveSpawn;
    public delegate void UpgradeMenuCallBack(bool active);
    public UpgradeMenuCallBack onUpgradeMenu;
    public int value = 10;
    public PlayerData playerData;
    private void Start()
    {
        currentLive = live;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            upgradeMenuUI.SetActive(!upgradeMenuUI.activeSelf);
            waveSpawn.enabled = !upgradeMenuUI.activeSelf;
            onUpgradeMenu.Invoke(upgradeMenuUI.activeSelf);
        }
    }
    public void PlayerDie(Player player)
    {
        Vector3 pos = player.transform.position;
        Destroy(player.gameObject);
        GameObject playerdeathEffectIns = Instantiate(playerdeathEffect, pos, Quaternion.identity);
        Destroy(playerdeathEffectIns, 1.5f);
        currentLive--;
        if (currentLive<=0)
        {
            EndGame(); 
        }
        else
        {
            StartCoroutine("Repawn");
        }
    }
    public void Win()
    {
        Debug.Log("");
    }
    public void EndGame()
    {
        gameOverUI.SetActive(true);
        playerData.SetDefaut();
    }
    public void EnemyDie(Enemy enemy)
    {
        GameObject enemydeathEffectIns = Instantiate(enemydeathEffect, enemy.transform.position, Quaternion.identity);
        Destroy(enemydeathEffectIns, 1.5f);
        Destroy(enemy.gameObject);
        WaveSpawn.enermiesAlive--;
        PlayerData.money += value;
    }
    IEnumerator Repawn()
    {
        yield return new WaitForSeconds(timeRepawn);
        GameObject playerIns = Instantiate(playerPrefab, spawnPos.position, Quaternion.identity);
        GameObject spawnIns = Instantiate(spawnEffect, spawnPos.position, Quaternion.identity);
        Destroy(spawnIns, 1.5f);
        yield return new WaitForSeconds(0.1f);
    }
}
