using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 필요시 UI 텍스트 업데이트를 위한 사용

public class GameManager : MonoBehaviour
{
    public WaveSpawner wave1;
    public WaveSpawner wave2;
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    private int currentWave = 1;
    private int activeEnemies = 0;
    private bool bossSpawned = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        if (currentWave == 1)
        {
            Debug.Log("Wave 1 시작");
            if(wave1 != null)
            {
                wave1.gameObject.SetActive(true);
                wave1.OnEnemySpawned += IncrementEnemyCount;
                wave1.OnEnemyDestroyed += DecrementEnemyCount;

                // 첫 스폰이 완료될 때까지 대기
                yield return new WaitUntil(() => wave1.isFirstSpawnComplete);
                yield return new WaitUntil(() => activeEnemies <= 0);

                wave1.OnEnemySpawned -= IncrementEnemyCount;
                wave1.OnEnemyDestroyed -= DecrementEnemyCount;
                wave1.gameObject.SetActive(false);
                Debug.Log("Wave 1 클리어");
                currentWave++;
            }
        }

        if (currentWave == 2)
        {
            Debug.Log("Wave 2 시작");
            if (wave1 != null)
            {
                wave2.gameObject.SetActive(true);
                wave2.OnEnemySpawned += IncrementEnemyCount;
                wave2.OnEnemyDestroyed += DecrementEnemyCount;

                // 첫 스폰이 완료될 때까지 대기
                yield return new WaitUntil(() => wave2.isFirstSpawnComplete);
                yield return new WaitUntil(() => activeEnemies <= 0);

                wave1.OnEnemySpawned -= IncrementEnemyCount;
                wave2.OnEnemyDestroyed -= DecrementEnemyCount;
                wave1.gameObject.SetActive(false);
                Debug.Log("Wave 2 클리어");
                currentWave++;
            }
        }

        if (currentWave == 3 && !bossSpawned)
        {
            Debug.Log("Boss Stage 시작");
            // 보스 소환
            GameObject bossInstance = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.Euler(0, 0, 0));

            // 보스 파괴 시 클리어 조건으로 연결
            var bossEnemyComponent = bossInstance.GetComponent<ForwardEnemy>(); // 또는 보스의 스크립트
            if (bossEnemyComponent != null)
            {
                bossEnemyComponent.OnDestroyed += () => DecrementEnemyCount(); // 보스 파괴 시 카운트 감소
            }

            bossSpawned = true;
            IncrementEnemyCount(); // 보스를 하나의 적으로 카운트

            // 보스가 파괴될 때까지 대기
            yield return new WaitUntil(() => activeEnemies <= 0);
            Debug.Log("Boss Stage 클리어");
        }
    }

    private void IncrementEnemyCount()
    {
        activeEnemies++;
    }

    private void DecrementEnemyCount()
    {
        activeEnemies--;
    }
}
