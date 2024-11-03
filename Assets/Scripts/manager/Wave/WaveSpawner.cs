using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> enemy;
    // ������ �������� �� �������� ����
    public GameObject itemPrefab;             // ������ ������
    public Vector3 spawnPoint;
    private float time;
    
    public void spawnEnemy(Vector3 spawnPoint, int enemyNo)
    {
        Instantiate(enemy[enemyNo], spawnPoint, Quaternion.identity);
        
    }

    public void setHP(int enemyNo, int HP)
    {
        foreach (GameObject en in enemy)
        {
            EnemyStatus ens = en.GetComponent<EnemyStatus>();
            if (ens != null && ens.enemyNo == enemyNo)
            {
                ens.maxHP = HP;
                ens.currentHP = HP;
                return;
            }
            else
            {
                Debug.Log("not Found!");
            }
        }
    }

}
