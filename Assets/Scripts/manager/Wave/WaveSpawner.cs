using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> enemy;
    // 각각의 프리팹을 적 유형별로 설정
    public GameObject itemPrefab;             // 아이템 프리팹
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
