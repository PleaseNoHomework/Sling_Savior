using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolem : MonoBehaviour
{
    //벽에 가까이 붙으면 공격
    public EnemyStatus enemy;

    public float moveDuration;      // 움직이는 시간
    public float stopDuration;      // 멈춰있는 시간

    private bool isMoving = true;
    private float timer = 0f;

    private Animator motion;
    private float time;
    private float attackTime = 0f;
    bool isAttackFinished = false;
    private Collider coll;

    void StoneGolemMove()
    {
        timer += Time.deltaTime;
        if (isMoving)
        {
            float time = Time.deltaTime;
            transform.Translate(enemy.moveDirection * enemy.speed * time);
            // 일정 시간이 지나면 멈추는 상태로 전환
            if (timer >= moveDuration)
            {
                isMoving = false;
                timer = 0f;
            }
        }
        else
        {
            if (timer >= stopDuration)
            {
                isMoving = true;
                timer = 0f;
            }
        }
    }

    void StoneGolemAttack()
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
            StoneGolemMove();
        }

        if (enemy._state == EnemyStatus.State.Attack)
        {
            StoneGolemAttack();
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
            Debug.Log(WaveSpawner.instance.activeEnemies + ", " + WaveSpawner.instance.lastSpawnEnemyFlag);
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
