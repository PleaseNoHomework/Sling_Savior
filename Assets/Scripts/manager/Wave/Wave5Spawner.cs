using System.Collections;
using UnityEngine;

public class Wave5Spawner : MonoBehaviour
{
    public WaveSpawner wave;
    public float spawnInterval = 5f;

    public IEnumerator SpawnWave5()
    {
        Debug.Log("Wave 5 시작: 최종 보스 생성");

        wave.spawnEnemy(new Vector3(0, 0, 30), 9);
        wave.lastSpawnEnemyFlag = 1;

        yield return new WaitForSeconds(spawnInterval);  // 다음 웨이브 대기시간
        yield break;
    }
}
