using System.Collections;
using UnityEngine;

public class Wave1Spawner : MonoBehaviour
{
    //12마리
    public WaveSpawner wave;

    public float spawnInterval = 5f; // 각 스폰 사이의 간격
    private int spawnStep = 0;       // 현재 스폰 단계



    public IEnumerator SpawnWave1()
    {
        Vector3 spawnPos = new Vector3(-10, 2, 50);
        Debug.Log("wave1 : " + (spawnStep + 1));
        while (spawnStep < 5)
        {
            switch (spawnStep)
            {
                case 0: // 첫 번째 스폰: Sand Spider (ForwardEnemy) 3마리

                    wave.spawnEnemy(new Vector3(-10, 0, 50), 1);
                    wave.spawnEnemy(new Vector3(0, 0, 50), 1);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 1);
                    break;

                case 1: // 두 번째 스폰: Turtle Shell Enemy (AccelerationEnemy) 2마리
                    wave.spawnEnemy(new Vector3(-5, 0, 50), 2);
                    wave.spawnEnemy(new Vector3(5, 0, 50), 2);
                    break;

                case 2: // 세 번째 스폰: Slime Enemy (ZigzagEnemy) 2마리(양 옆) + Turtle Shell Enemy 1마리(가운데, flag 1)
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 3);
                    wave.spawnItemEnemy(new Vector3(0, 0, 50), 2);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 3);
                    break;

                case 3: // 네 번째 스폰: Sand Spider, Turtle Shell Enemy, Slime Enemy 각각 1마리
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 1);
                    wave.spawnEnemy(new Vector3(0, 0, 50), 2);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 3);
                    break;

                case 4: // 다섯 번째 스폰: 높은 HP의 Sand Spider 한 마리
                    //wave.setHP(1, 300);
                    float rn = Random.Range(0, 1);
                    if (rn < 0.7f)
                        wave.spawnItemEnemy(new Vector3(0, 0, 50), 1);
                    else
                        wave.spawnEnemy(new Vector3(0, 0, 50), 1);
                    wave.lastSpawnEnemyFlag = 1;
                    break;
            }

            spawnStep++;
            if (spawnStep <= 3) yield return new WaitForSeconds(spawnInterval); // 다음 스폰 간격 대기
            else yield return new WaitForSeconds(1f);
        }
        Debug.Log(wave.spawnEnemies);
    }
}