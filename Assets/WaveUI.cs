using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    public WaveSpawn waveSpawn;
    public Animator animator;
    public Text waveCountdownText;
    public Text waveCountText;

    private WaveSpawn.SpawnState previousState;
    // Start is called before the first frame update
    void Start()
    {
        if (waveSpawn == null)
            this.enabled = false;
        if (animator == null)
            this.enabled = false;
        if (waveCountdownText == null)
            this.enabled = false;
        if (waveCountText == null)
            this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (waveSpawn.State)
        {
            case WaveSpawn.SpawnState.COUNTING:
                UpdateCountingUI();
                break;
            case WaveSpawn.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
        }
        previousState = waveSpawn.State;
    }
    void UpdateCountingUI()
    {
        if (previousState!=WaveSpawn.SpawnState.COUNTING)
        {
            animator.SetBool("WaveIncoming", false);
            animator.SetBool("WaveCountdown", true);
        }
        waveCountdownText.text =((int) waveSpawn.WaveCountDown).ToString();
    }
    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawn.SpawnState.SPAWNING)
        {
            animator.SetBool("WaveIncoming", true);
            animator.SetBool("WaveCountdown", false);
            waveCountText.text = ((int)waveSpawn.WaveIndex).ToString();
            Debug.Log("Spawn");
        }
    }
}
