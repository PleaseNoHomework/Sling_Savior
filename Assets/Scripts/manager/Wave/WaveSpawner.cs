using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;
    public List<GameObject> enemy;
    public float HPCoeffecient; //HP 계수
    public int currentWave = 1;

    public Wave1Spawner wave1;           // Wave1Spawner 참조
    public Wave2Spawner wave2;           // Wave2Spawner 참조
    
    public List<int> currentWaveEnemy;


    //적을 잡으면 WaveClear에 있는 함수가 실행된다.
    //마지막 적을 잡으면 WaveClear에 있는 함수가 실행된다.
    // 각각의 프리팹을 적 유형별로 설정
    //wave에는 Wave1Spawner, Wave2Spawner이 들어있는 오브젝트가 있다.


    public GameObject itemPrefab;             // 아이템 프리팹
    public Vector3 spawnPoint;

    public int spawnEnemies = 0; //생성된 적 수
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
    }

    public void spawnItemEnemy(Vector3 spawnPoint, int enemyNo)
    {
        GameObject newEnemy = Instantiate(enemy[enemyNo], spawnPoint, Quaternion.identity);
        EnemyStatus status = newEnemy.GetComponent<EnemyStatus>();
        status.setHP(status.maxHP * HPCoeffecient);
        status.itemFlag = 1;
        activeEnemies++;
        spawnEnemies++;
    }

    public void setHP(int enemyNo, float HP)
    {
        EnemyStatus en = enemy[enemyNo].GetComponent<EnemyStatus>();
        en.setHP(HP);
    }

    //currentWave 값에 따라 웨이브를 소환한다.
    public void spawnWave(int no)
    {
        spawnEnemies = 0;
        activeEnemies = 0;
        switch (no)
        {
            case 1:
                StartCoroutine(wave1.SpawnWave1());
                break;
            case 2:
                StartCoroutine(wave2.SpawnWave2());
                break;
            default:
                break;
        }
    }

    public bool checkWaveFinished() //전부 생성되고 모두 잡으면 true 반환
    {
        return (spawnEnemies == currentWaveEnemy[currentWave] && activeEnemies == 0);
    }
    private void Start()
    {
        if (instance == null) instance = this;
        HPCoeffecient = 1;
    }

}
