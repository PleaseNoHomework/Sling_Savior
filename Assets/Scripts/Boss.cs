using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // 생성할 적 프리팹 리스트
    public float speed; // 보스의 좌우 이동 속도
    public float moveRange = 10f; // 좌우 이동 범위
    public float spawnRate = 2f; // 적 생성 간격

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position; // 보스의 초기 위치를 저장
        InvokeRepeating("SpawnEnemy", 0f, spawnRate); // 적 생성 주기 설정
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // 좌우로 이동
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            // 오른쪽 이동 제한
            if (transform.position.x >= startPosition.x + moveRange)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // 왼쪽 이동 제한
            if (transform.position.x <= startPosition.x - moveRange)
            {
                movingRight = true;
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Count > 0) // 적 프리팹이 있는지 확인
        {
            // 적 프리팹을 무작위로 선택하여 생성
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Vector3 spawnPosition = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z - 2f // 보스 위치 앞에서 생성
            );

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
