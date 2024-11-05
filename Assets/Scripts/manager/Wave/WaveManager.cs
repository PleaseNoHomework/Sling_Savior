using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager: MonoBehaviour
{
    public static WaveManager instance;
    public int spawnFlag = 0; //1�̸� �����ϱ�
    public delegate void WaveCompleted();
    public event WaveCompleted WaveClear;
    public GameObject waveUI;

    private void Start()
    {
        if (instance == null) instance = this;
        WaveSpawner.instance.spawnWave(WaveSpawner.instance.currentWave);
        Debug.Log("Start!");
    }

    private void Update()
    {
        if (WaveSpawner.instance.checkWaveFinished())
        {
            Debug.Log("finished!");
            waveUI.SetActive(true);
            WaveSpawner.instance.currentWave++;
            waveUIScript.instance.flag = 1;
        }

        if(spawnFlag == 1)
        {
            WaveSpawner.instance.spawnWave(WaveSpawner.instance.currentWave);
            spawnFlag = 0;
        }
    }

    void HandleEnemyDestroyed()
    {
    }
}
