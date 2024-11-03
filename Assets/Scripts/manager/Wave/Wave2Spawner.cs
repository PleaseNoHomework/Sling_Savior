using System.Collections;
using UnityEngine;

public class Wave2Spawner : MonoBehaviour
{
    public GameObject sandSpiderEnemyPrefab;  // ForwardEnemy
    public GameObject slimeEnemyPrefab;       // ZigzagEnemy
    public GameObject turtleShellEnemyPrefab; // AccelerationEnemy
    public GameObject itemPrefab;             // 아이템 프리팹

    public float spawnInterval = 5f; // 각 스폰 사이의 간격
    private int spawnStep = 0;       // 현재 스폰 단계
    private int activeEnemies = 0;   // 현재 필드에 남아 있는 적의 수

    // GameManager에게 Wave2 종료를 알리기 위한 델리게이트와 이벤트
    public delegate void WaveCompleted();
    public event WaveCompleted OnWave2Completed;

    void Start()
    {
        StartCoroutine(SpawnWave2());
    }

    IEnumerator SpawnWave2()
    {
        while (spawnStep < 9)
        {
            switch (spawnStep)
            {
                case 0: // 첫 번째 스폰: Spider (ForwardEnemy) 3마리
                    for (int i = 0; i < 3; i++)
                    {
                        SpawnEnemy(sandSpiderEnemyPrefab);
                    }
                    break;

                case 1: // 두 번째 스폰: Slime (ZigzagEnemy) 3마리 (가운데는 flag 1)
                    SpawnEnemy(slimeEnemyPrefab, new Vector3(-2, 0, 10));
                    SpawnEnemy(slimeEnemyPrefab, new Vector3(0, 0, 10), 1); // 가운데, flag = 1
                    SpawnEnemy(slimeEnemyPrefab, new Vector3(2, 0, 10));
                    break;

                case 2: // 세 번째 스폰: TurtleShell (AccelerationEnemy) 2마리
                    for (int i = 0; i < 2; i++)
                    {
                        SpawnEnemy(turtleShellEnemyPrefab);
                    }
                    break;

                case 3: // 네 번째 스폰: 양쪽에 Spider, 가운데 TurtleShell 1마리
                    SpawnEnemy(sandSpiderEnemyPrefab, new Vector3(-2, 0, 10));
                    SpawnEnemy(turtleShellEnemyPrefab, new Vector3(0, 0, 10));
                    SpawnEnemy(sandSpiderEnemyPrefab, new Vector3(2, 0, 10));
                    break;

                case 4: // 다섯 번째 스폰: 양쪽에 TurtleShell, 가운데 Slime
                    SpawnEnemy(turtleShellEnemyPrefab, new Vector3(-2, 0, 10));
                    SpawnEnemy(slimeEnemyPrefab, new Vector3(0, 0, 10));
                    SpawnEnemy(turtleShellEnemyPrefab, new Vector3(2, 0, 10));
                    break;

                case 5: // 여섯 번째 스폰: TurtleShell 3마리 (가운데는 flag 1)
                    SpawnEnemy(turtleShellEnemyPrefab, new Vector3(-2, 0, 10));
                    SpawnEnemy(turtleShellEnemyPrefab, new Vector3(0, 0, 10), 1); // 가운데, flag = 1
                    SpawnEnemy(turtleShellEnemyPrefab, new Vector3(2, 0, 10));
                    break;

                case 6: // 일곱 번째 스폰: HP 800인 Slime 2마리
                    SpawnEnemy(slimeEnemyPrefab, hp: 800);
                    SpawnEnemy(slimeEnemyPrefab, hp: 800);
                    break;

                case 7: // 여덟 번째 스폰: HP 800인 TurtleShell 2마리
                    SpawnEnemy(turtleShellEnemyPrefab, hp: 800);
                    SpawnEnemy(turtleShellEnemyPrefab, hp: 800);
                    break;

                case 8: // 아홉 번째 스폰: HP 1000인 Spider 2마리
                    SpawnEnemy(sandSpiderEnemyPrefab, hp: 1000);
                    SpawnEnemy(sandSpiderEnemyPrefab, hp: 1000);
                    break;
            }

            spawnStep++;
            yield return new WaitForSeconds(spawnInterval); // 다음 스폰 간격 대기
        }
    }

    void SpawnEnemy(GameObject prefab, Vector3? position = null, int flag = 0, int hp = 500)
    {
        Vector3 spawnPosition = position ?? new Vector3(
            transform.position.x + Random.Range(-5f, 5f),
            transform.position.y,
            10f
        );

        GameObject enemyInstance = Instantiate(prefab, spawnPosition, transform.rotation);

        // 적 속성 설정 및 이벤트 연결
        if (prefab == sandSpiderEnemyPrefab) // ForwardEnemy
        {
            var enemyComponent = enemyInstance.GetComponent<ForwardEnemy>();
            if (enemyComponent != null)
            {
                enemyComponent.hp = hp;
                enemyComponent.OnDestroyed += () => HandleEnemyDestroyed(enemyInstance, flag);
            }
        }
        else if (prefab == slimeEnemyPrefab) // ZigzagEnemy
        {
            var enemyComponent = enemyInstance.GetComponent<ZigzagEnemy>();
            if (enemyComponent != null)
            {
                enemyComponent.hp = hp;
                enemyComponent.OnDestroyed += () => HandleEnemyDestroyed(enemyInstance, flag);
            }
        }
        else if (prefab == turtleShellEnemyPrefab) // AccelerationEnemy
        {
            var enemyComponent = enemyInstance.GetComponent<AccelerationEnemy>();
            if (enemyComponent != null)
            {
                enemyComponent.hp = hp;
                enemyComponent.flag = flag;
                enemyComponent.OnDestroyed += () => HandleEnemyDestroyed(enemyInstance, flag);
            }
        }

        // 활성화된 적 수 증가
        activeEnemies++;
    }

    void HandleEnemyDestroyed(GameObject enemyInstance, int flag)
    {
        activeEnemies--;

        // flag가 1인 적이 파괴되었을 경우 아이템 생성
        if (flag == 1)
        {
            Instantiate(itemPrefab, enemyInstance.transform.position, Quaternion.identity);
        }

        // 모든 적이 파괴되면 웨이브 종료
        if (spawnStep >= 9 && activeEnemies <= 0)
        {
            OnWave2Completed?.Invoke();
            Debug.Log("Wave 2 클리어");
        }
    }
}
