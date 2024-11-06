using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;    // 소환할 적 프리팹 리스트
    public EnemyStatus enemy;
    public float moveRange = 5.0f;           // 좌우 이동 범위
    public float spawnRate = 3.0f;           // 적 소환 주기

    private Vector3 startPosition;           // 보스의 시작 위치
    private bool movingRight = true;         // 이동 방향 제어
    private float spawnTimer = 0f;           // 적 소환 타이머
    private Animator motion;                 // 애니메이션 컨트롤러
    private float time;
    private Collider coll;

    void BossMove()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.right * enemy.speed * Time.deltaTime, Space.World);
            if (transform.position.x >= startPosition.x + moveRange)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * enemy.speed * Time.deltaTime, Space.World);
            if (transform.position.x <= startPosition.x - moveRange)
            {
                movingRight = true;
            }
        }
    }

    void SpawnMinions()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            if (enemyPrefabs.Count > 0)
            {
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
            spawnTimer = 0f;
        }
    }

    private void Awake()
    {
        motion = GetComponent<Animator>();
        coll = GetComponent<Collider>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (enemy._state == EnemyStatus.State.Spawn)
        {
            if (time >= 1f)
            {
                motion.SetTrigger("WalkTrigger");
                enemy._state = EnemyStatus.State.Move;
            }
        }

        if (enemy._state == EnemyStatus.State.Move)
        {
            BossMove();
            SpawnMinions();
        }

        if (enemy.currentHP <= 0)
        {
            enemy._state = EnemyStatus.State.Die;
            motion.Play("Death");
            motion.SetTrigger("DeathTrigger");
            coll.enabled = false;
            if (IsAnimationFinished("Death"))
            {
                Destroy(gameObject);
            }
        }
    }

    bool IsAnimationFinished(string animationName)
    {
        return motion.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
               motion.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    }
}
