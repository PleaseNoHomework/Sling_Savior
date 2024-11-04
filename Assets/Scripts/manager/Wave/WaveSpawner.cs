using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;
    public List<GameObject> enemy;
    public float HPCoeffecient; //HP 계수

    //적을 잡으면 WaveClear에 있는 함수가 실행된다.
    //마지막 적을 잡으면 WaveClear에 있는 함수가 실행된다.
    // 각각의 프리팹을 적 유형별로 설정
    public GameObject itemPrefab;             // 아이템 프리팹
    public Vector3 spawnPoint;

    public int spawnEnemies = 0;
    public int activeEnemies = 0;   // 현재 필드에 남아 있는 적의 수

    public delegate void WaveCompleted();
    public event WaveCompleted WaveClear;

    public void spawnEnemy(Vector3 spawnPoint, int enemyNo)
    {
        GameObject newEnemy = Instantiate(enemy[enemyNo], spawnPoint, Quaternion.identity);
        EnemyStatus status = newEnemy.GetComponent<EnemyStatus>();
        status.setHP(status.maxHP * HPCoeffecient);
        status.itemFlag = 0;
        activeEnemies++;
        spawnEnemies++;
        status.OnDestroyed += () => HandleEnemyDestroyed();

    }

    public void spawnItemEnemy(Vector3 spawnPoint, int enemyNo)
    {
        GameObject newEnemy = Instantiate(enemy[enemyNo], spawnPoint, Quaternion.identity);
        EnemyStatus status = newEnemy.GetComponent<EnemyStatus>();
        status.setHP(status.maxHP * HPCoeffecient);
        status.itemFlag = 1;
        activeEnemies++;
        spawnEnemies++;
        status.OnDestroyed += () => HandleEnemyDestroyed();
    }

    public void setHP(int enemyNo, float HP)
    {
        EnemyStatus en = enemy[enemyNo].GetComponent<EnemyStatus>();
        en.setHP(HP);
    }



    private void Start()
    {
        if (instance == null) instance = this;
        HPCoeffecient = 1;
    }

    void HandleEnemyDestroyed()
    {
        activeEnemies--;

        if (spawnEnemies > 5 && activeEnemies == 0)
        {
            WaveClear?.Invoke();
            Debug.Log("Wave 클리어");
        }
        
    }
}
