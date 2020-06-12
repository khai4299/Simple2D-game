using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSpawn : MonoBehaviour
{
    public enum SpawnState { SPAWNING,WAITING,COUNTING};
    public Transform[] spawnPoint;
    public float TimeBetweenWave = 5f;
    private float countdown = 2f;
    public float WaveCountDown
    {
        get { return countdown; }
    }
    public static int enermiesAlive = 0;
    public Wave[] waves;
    private int waveIndex = 0;
    public float WaveIndex
    {
        get { return waveIndex+1; }
    }
    public GameMaster gameMaster;
    private SpawnState state = SpawnState.COUNTING;
    public SpawnState State
    {
        get { return state; }
    }
    private void Start()
    {
        enermiesAlive = 0;
    }
    private void Update()
    {
        if (enermiesAlive > 0)
        {
            return;
        }
        if (waveIndex == waves.Length)
        {
            gameMaster.Win();
            this.enabled = false;
        }
        state = SpawnState.COUNTING;
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = TimeBetweenWave;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity);
    }
    IEnumerator SpawnWave()
    {
        state = SpawnState.SPAWNING;
        Wave wave = waves[waveIndex];
        enermiesAlive = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnermy(wave.enermy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;
        state = SpawnState.WAITING;
    }

    void SpawnEnermy(GameObject enermy)
    {
        Transform spawnPos = spawnPoint[Random.Range(0, spawnPoint.Length-1)];
        Instantiate(enermy, spawnPos.position, spawnPos.rotation);
    }
}
