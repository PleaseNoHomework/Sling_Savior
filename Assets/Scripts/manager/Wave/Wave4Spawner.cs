using System.Collections;
using UnityEngine;

public class Wave4Spawner : MonoBehaviour
{
    //총 23마리
    public WaveSpawner wave;

    public float spawnInterval = 7f; // 각 스폰 사이의 간격
    private int spawnStep = 0;       // 현재 스폰 단계


    public IEnumerator SpawnWave4()
    {
        Debug.Log("wave4 : " + (spawnStep + 1));
        Vector3 spawnPos = new Vector3(-10, 0, 50);
        while (spawnStep < 9)
        {
            switch (spawnStep)
            {
                case 0: // 첫 번째 스폰: Black Widow 3마리
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 4);
                    wave.spawnEnemy(new Vector3(0, 0, 50), 4);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 4);
                    break;

                case 1: // 두 번째 스폰: Slime (ZigzagEnemy) 3마리 (가운데는 flag 1)
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 3);
                    wave.spawnItemEnemy(new Vector3(0, 0, 50), 3);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 3);
                    break;

                case 2: // 세 번째 스폰: TurtleShell (AccelerationEnemy) 3마리
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 2);
                    wave.spawnEnemy(new Vector3(0, 0, 50), 2);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 2);
                    break;

                case 3: // 네 번째 스폰: 양쪽에 StoneGolem, 가운데 IceGolem 1마리
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 6);
                    wave.spawnEnemy(new Vector3(0, 0, 50), 5);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 6);
                    break;

                case 4: // 다섯 번째 스폰: 양쪽에 ghost, 가운데 slime
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 7);
                    wave.spawnEnemy(new Vector3(0, 0, 50), 3);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 7);
                    break;

                case 5: // 여섯 번째 스폰: TurtleShell 3마리 (가운데는 flag 1)
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 2);
                    wave.spawnItemEnemy(new Vector3(0, 0, 50), 2);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 2);
                    break;

                case 6: // 일곱 번째 스폰: slime 3마리
                    //wave.setHP(3, 800)
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 3);
                    wave.spawnEnemy(new Vector3(0, 0, 50), 3);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 3);
                    break;

                case 7: // 여덟 번째 스폰: HP 800인 TurtleShell 2마리
<<<<<<< HEAD
                    wave.spawnEnemy(new Vector3(-5, 0, 10), 2);
                    wave.spawnEnemy(new Vector3(5, 0, 10), 2);
                    wave.lastSpawnEnemyFlag = 1;
=======
                    wave.spawnEnemy(new Vector3(-5, 0, 50), 2);
                    wave.spawnEnemy(new Vector3(5, 0, 50), 2);
>>>>>>> jeon
                    break;
            }

            spawnStep++;
            yield return new WaitForSeconds(spawnInterval); // 다음 스폰 간격 대기
        }
        Debug.Log(wave.spawnEnemies);
    }
}
