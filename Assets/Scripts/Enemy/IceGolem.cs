using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGolem : MonoBehaviour
{
    public EnemyStatus enemy;

    public float forwardMoveInterval = 6f; // 직선 이동 시간
    public float sideMoveInterval = 2f;   // X축 이동 시간
    private float elapsedMoveTime = 0f;   // 경과 시간
    private bool isMovingForward = true;  // 현재 직선으로 이동 중인지 여부
    private float[] xDirections = { -1f, 1f }; // X축 방향 배열

    private float minX = -12f;
    private float maxX = 12f;

    private Animator motion;
    private float time;
    private float attackTime = 0f;
    private bool isAttackFinished = false;
    private Collider coll;

    private Vector3 initialDirection = new Vector3(0, 0, 1); // Z축 직선 이동 방향

    void IceGolemMove()
    {
        elapsedMoveTime += Time.deltaTime;

        // X축 경계 처리
        if (transform.position.x <= minX && enemy.moveDirection.x < 0)
        {
            Debug.Log("Reached Left Boundary");
            enemy.moveDirection.x = Mathf.Abs(enemy.moveDirection.x); // 오른쪽으로 이동
            transform.position = new Vector3(minX, transform.position.y, transform.position.z); // 위치 보정
        }
        else if (transform.position.x >= maxX && enemy.moveDirection.x > 0)
        {
            Debug.Log("Reached Right Boundary");
            enemy.moveDirection.x = -Mathf.Abs(enemy.moveDirection.x); // 왼쪽으로 이동
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z); // 위치 보정
        }

        // 이동 방향 전환
        if (isMovingForward && elapsedMoveTime >= forwardMoveInterval)
        {
            isMovingForward = false;
            elapsedMoveTime = 0f;

            // X축 이동 방향 설정
            enemy.moveDirection.x = xDirections[Random.Range(0, xDirections.Length)]*2f;
            enemy.moveDirection.z = 0; // Z축 이동 중지
        }
        else if (!isMovingForward && elapsedMoveTime >= sideMoveInterval)
        {
            isMovingForward = true;
            elapsedMoveTime = 0f;

            // Z축 직선 이동 방향 설정
            enemy.moveDirection.x = 0; // X축 이동 중지
            enemy.moveDirection.z = initialDirection.z; // Z축 앞으로 이동
        }

        // 이동
        transform.Translate(enemy.moveDirection * enemy.speed * Time.deltaTime);

        // X축 경계를 벗어나지 않도록 제한
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }


    void IceGolemAttack()
    {
        if (motion.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            attackTime += Time.deltaTime;
        }

        if (attackTime >= 1f && !isAttackFinished)
        {
            HPManager.instance.baseHP--;
            Debug.Log("HP Down!");
            isAttackFinished = true;
        }

        if (attackTime >= 2.6f)
        {
            motion.SetTrigger("AttackTrigger");
            isAttackFinished = false;
            attackTime = 0;
        }
    }

    private void Awake()
    {
        motion = GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(0, 180, 0);
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
            IceGolemMove();
        }

        if (enemy._state == EnemyStatus.State.Attack)
        {
            IceGolemAttack();
        }

        if (enemy.currentHP <= 0)
        {
            enemy._state = EnemyStatus.State.Die;
            motion.Play("Death");
            motion.SetTrigger("DeathTrigger");
            coll.enabled = false;
        }

        if (enemy._state == EnemyStatus.State.Die && IsAnimationFinished("Death")) {
            WaveSpawner.instance.activeEnemies--;
            Destroy(gameObject);
        }
        

    }

    bool IsAnimationFinished(string animationName)
    {
        return motion.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
               motion.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            enemy._state = EnemyStatus.State.Attack;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackPoint"))
        {
            Debug.Log("ee");
            enemy._state = EnemyStatus.State.Attack;
            motion.SetTrigger("AttackTrigger");
        }
    }
}