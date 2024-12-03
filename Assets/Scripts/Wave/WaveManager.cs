using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager: MonoBehaviour
{
    public static WaveManager instance;
    public int spawnFlag = 0; //1이면 스폰하기
    public GameObject waveUI;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }      
    }

    private void Update()
    {
        if (WaveSpawner.instance.checkWaveFinished())
        {
            Debug.Log("finished!");
            WaveSpawner.instance.lastSpawnEnemyFlag = 0;
            
            WaveSpawner.instance.currentWave++;
            waveUIScript.instance.flag = 1;

            if (WaveSpawner.instance.currentWave > 5)
            {
                SceneController.instance.LoadGameClearScene();
            }
            else
            {
                waveUI.SetActive(true);
            }
            
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
