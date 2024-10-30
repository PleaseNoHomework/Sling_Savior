using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // 소환할 적 프리팹 리스트
    public int hp = 1500;
    public float speed;
    public float moveRange;         // 좌우 이동 범위
    public float spawnRate;          // 적 소환 주기

    private Vector3 startPosition;
    private bool movingRight = true;      // 이동 방향 (좌/우)

    // GameManager에게 보스 파괴를 알리기 위한 델리게이트와 이벤트
    public delegate void BossDestroyed();
    public event BossDestroyed OnBossDestroyed;

    void Start()
    {
        startPosition = transform.position;            // 보스의 초기 위치를 저장
        transform.rotation = Quaternion.Euler(0, 180, 0); // 보스가 반대를 보고있어서 회전
        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate); // 일정 간격으로 적을 소환
    }

    void Update()
    {
        // 좌우로 이동 (World 좌표계를 기준으로 이동)
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

            // 오른쪽 한계 도달 시 방향 전환
            if (transform.position.x >= startPosition.x + moveRange)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

            // 왼쪽 한계 도달 시 방향 전환
            if (transform.position.x <= startPosition.x - moveRange)
            {
                movingRight = true;
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Count > 0) // 적 프리팹이 존재할 때만 소환
        {
            int enemyTypesToSpawn = Random.Range(1, 4); // 한번에 소환할 적 종류 수 (1~3개 랜덤)

            for (int i = 0; i < enemyTypesToSpawn; i++)
            {
                // 적 프리팹을 무작위로 선택하여 소환
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

                // 보스의 위치에서 적 소환
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            OnBossDestroyed?.Invoke(); // 보스 파괴 시 이벤트 호출
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) // Bullet 태그의 발사체와 충돌 시
        {
            Bullet bullet = other.GetComponent<Bullet>(); // 발사체의 데미지 가져오기
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject); // 발사체 파괴
            }
        }
    }
}
