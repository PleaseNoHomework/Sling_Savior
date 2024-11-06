using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;
    public List<GameObject> enemy;
    public float HPCoeffecient; //HP ���
    public int currentWave = 1;

    public Wave1Spawner wave1;           // Wave1Spawner ����
    public Wave2Spawner wave2;           // Wave2Spawner ����
    
    public List<int> currentWaveEnemy;


    //���� ������ WaveClear�� �ִ� �Լ��� ����ȴ�.
    //������ ���� ������ WaveClear�� �ִ� �Լ��� ����ȴ�.
    // ������ �������� �� �������� ����
    //wave���� Wave1Spawner, Wave2Spawner�� ����ִ� ������Ʈ�� �ִ�.


    public GameObject itemPrefab;             // ������ ������
    public Vector3 spawnPoint;

    public int spawnEnemies = 0; //������ �� ��
    public int activeEnemies = 0;   // ���� �ʵ忡 ���� �ִ� ���� ��

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

    //currentWave ���� ���� ���̺긦 ��ȯ�Ѵ�.
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

    public bool checkWaveFinished() //���� �����ǰ� ��� ������ true ��ȯ
    {
        return (spawnEnemies == currentWaveEnemy[currentWave] && activeEnemies == 0);
    }
    private void Start()
    {
        if (instance == null) instance = this;
        HPCoeffecient = 1;
    }

}
