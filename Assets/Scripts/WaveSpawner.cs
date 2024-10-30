using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public float startTime; // 시작 시간
    public float endTime;   // 종료 시간
    public float spawnRate; // 웨이브 간격 (웨이브 사이의 대기 시간)

    void Start()
    {
        if (enemyPrefabs.Count > 0)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(startTime); // 시작 시간 대기

        float elapsedTime = 0f;

        while (elapsedTime < endTime)
        {
            int enemyTypesToSpawn = Random.Range(1, 4); // 한번에 생성할 적의 종류 수 (1~3종류 랜덤)

            for (int i = 0; i < enemyTypesToSpawn; i++)
            {
                // 랜덤한 적 프리팹 선택
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

                // 랜덤 위치 생성
                Vector3 randomPosition = new Vector3(
                    transform.position.x + Random.Range(-5f, 5f),
                    transform.position.y,
                    transform.position.z + Random.Range(-10f, 10f)
                );

                // 적 생성
                Instantiate(enemyPrefab, randomPosition, transform.rotation);
            }

            // 다음 웨이브 간격 대기
            yield return new WaitForSeconds(spawnRate);
            elapsedTime += spawnRate;
        }
    }
}
