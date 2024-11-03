using System.Collections;
using UnityEngine;

public class Wave1Spawner : MonoBehaviour
{
    public WaveSpawner wave;
    public GameObject itemPrefab;             // 아이템 프리팹

    public float spawnInterval = 5f; // 각 스폰 사이의 간격
    private int spawnStep = 0;       // 현재 스폰 단계
    private int activeEnemies = 0;   // 현재 필드에 남아 있는 적의 수
    private Vector3 defaultSpawnPoint = Vector3.zero;

    // GameManager에게 Wave1 종료를 알리기 위한 델리게이트와 이벤트
    public delegate void WaveCompleted();
    public event WaveCompleted OnWave1Completed;

    void Start()
    {
        
        StartCoroutine(SpawnWave1());
    }

    IEnumerator SpawnWave1()
    {
        Vector3 spawnPos = new Vector3(-10, 0, 10);
        while (spawnStep < 5)
        {
            switch (spawnStep)
            {
                case 0: // 첫 번째 스폰: Sand Spider (ForwardEnemy) 3마리
                    
                    for (int i = 0; i < 3; i++)
                    {
                        //SpawnEnemy(sandSpiderEnemyPrefab, spawnPos);
                        wave.spawnEnemy(spawnPos, 1);
                        spawnPos.x += 10;
                    }
                    break;

                case 1: // 두 번째 스폰: Turtle Shell Enemy (AccelerationEnemy) 2마리
                    spawnPos.x = -10;
                    for (int i = 0; i < 2; i++)
                    {
                        wave.spawnEnemy(defaultSpawnPoint, 2);
                        spawnPos.x += 10;
                    }
                    break;

                case 2: // 세 번째 스폰: Slime Enemy (ZigzagEnemy) 2마리(양 옆) + Turtle Shell Enemy 1마리(가운데, flag 1)
                    wave.spawnEnemy(new Vector3(-10, 0, 10), 3);
                    wave.spawnEnemy(new Vector3(0, 0, 10), 2);
                    wave.spawnEnemy(new Vector3(10, 0, 10), 3);
                    break;

                case 3: // 네 번째 스폰: Sand Spider, Turtle Shell Enemy, Slime Enemy 각각 1마리
                    wave.spawnEnemy(new Vector3(-10, 0, 10), 1);
                    wave.spawnEnemy(new Vector3(0, 0, 10), 2);
                    wave.spawnEnemy(new Vector3(10, 0, 10), 3);
                    break;

                case 4: // 다섯 번째 스폰: 높은 HP의 Sand Spider 한 마리
                    wave.spawnEnemy(new Vector3(0, 0, 10), 0);
                    wave.setHP(1, 300);
                    break;
            }

            spawnStep++;
            if (spawnStep <= 3) yield return new WaitForSeconds(spawnInterval); // 다음 스폰 간격 대기
            else yield return new WaitForSeconds(8f);
        }
    }

    /*
    void SpawnEnemy(GameObject prefab, Vector3? position = null, int flag = 0, int hp = 100)
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
    */
    void HandleEnemyDestroyed(GameObject enemyInstance, int flag)
    {
        activeEnemies--;

        // flag가 1인 적이 파괴되었을 경우 아이템 생성
        if (flag == 1)
        {
            Instantiate(itemPrefab, enemyInstance.transform.position, Quaternion.identity);
        }

        // 모든 적이 파괴되면 웨이브 종료
        if (spawnStep >= 5 && activeEnemies <= 0)
        {
            OnWave1Completed?.Invoke();
            Debug.Log("Wave 1 클리어");
        }
    }
}