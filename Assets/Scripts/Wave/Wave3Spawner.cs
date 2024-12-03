using System.Collections;
using UnityEngine;

public class Wave3Spawner : MonoBehaviour
{
    public WaveSpawner wave;
    public float spawnInterval = 5f;

    public IEnumerator SpawnWave3()
    {
        Debug.Log("Wave 3 시작: 중간 보스 생성");

        wave.spawnItemEnemy(new Vector3(0, 0, 30), 8);
        wave.lastSpawnEnemyFlag = 1;
        yield return new WaitForSeconds(spawnInterval);  // 다음 웨이브 대기시간
        yield break;
    }
}
