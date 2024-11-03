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
    
    void spawnEnemy(Vector3 spawnPoint, int enemyNo)
    {
        Instantiate(enemy[enemyNo], spawnPoint, Quaternion.identity);
        
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 2)
        {
            spawnEnemy(spawnPoint, 1);
            time = 0;
        }
    }
}
