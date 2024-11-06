using System.Collections;
using UnityEngine;

public class Wave2Spawner : MonoBehaviour
{
    //총 19마리
    public WaveSpawner wave;

    public float spawnInterval = 5f; // 각 스폰 사이의 간격
    private int spawnStep = 0;       // 현재 스폰 단계

    // GameManager에게 Wave2 종료를 알리기 위한 델리게이트와 이벤트
    public delegate void WaveCompleted();
    public event WaveCompleted OnWave2Completed;

    public IEnumerator SpawnWave2()
    {
        Debug.Log("wave2 : " + (spawnStep + 1));
        Vector3 spawnPos = new Vector3(-10, 0, 50);
        while (spawnStep < 9)
        {
            switch (spawnStep)
            {
                case 0: // 첫 번째 스폰: StoneGolem 3마리
                    for (int i = 0; i < 3; i++)
                    {
                        wave.spawnEnemy(spawnPos, 6);
                        spawnPos.x += 10;
                    }
                    break;

                case 1: // 두 번째 스폰: IceGolem 3마리 (가운데는 flag 1)
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 5);
                    wave.spawnItemEnemy(new Vector3(0, 0, 50), 5);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 5);
                    break;

                case 2: // 세 번째 스폰: ghost 2마리
                    wave.spawnEnemy(new Vector3(-5, 0, 50), 7);
                    wave.spawnEnemy(new Vector3(5, 0, 50), 7);
                    break;

                case 3: // 네 번째 스폰: 양쪽에 Black Widow, 가운데 TurtleShell 1마리
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 4);
                    wave.spawnEnemy(new Vector3(0, 0, 50), 2);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 4);
                    break;

                case 4: // 다섯 번째 스폰: 양쪽에 TurtleShell, 가운데 Slime (가운데는 flag 1)
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 2);
                    wave.spawnItemEnemy(new Vector3(0, 0, 50), 3);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 2);
                    break;

                case 5: // 여섯 번째 스폰: ghost 3마리
                    wave.spawnEnemy(new Vector3(-10, 0, 50), 7);
                    wave.spawnEnemy(new Vector3(0, 0, 50), 7);
                    wave.spawnEnemy(new Vector3(10, 0, 50), 7);
                    break;

                case 6: // 일곱 번째 스폰: HP 800인 StoneGolem 2마리
                    //wave.setHP(3, 800)
                    wave.spawnEnemy(new Vector3(-5, 0, 50), 6);
                    wave.spawnEnemy(new Vector3(5, 0, 50), 6);
                    wave.lastSpawnEnemyFlag = 1;
                    break;
            }

            spawnStep++;
            yield return new WaitForSeconds(spawnInterval); // 다음 스폰 간격 대기
        }
        Debug.Log(wave.spawnEnemies);
    }
}
