using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;
    public List<GameObject> enemy;
    public float HPCoeffecient; //HP ���

    //���� ������ WaveClear�� �ִ� �Լ��� ����ȴ�.
    //������ ���� ������ WaveClear�� �ִ� �Լ��� ����ȴ�.
    // ������ �������� �� �������� ����
    public GameObject itemPrefab;             // ������ ������
    public Vector3 spawnPoint;

    public int spawnEnemies = 0;
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
            Debug.Log("Wave Ŭ����");
        }
        
    }
}
