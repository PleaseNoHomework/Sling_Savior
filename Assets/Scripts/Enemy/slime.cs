using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour
{
    public EnemyStatus enemy;

    private Animator motion;
    private float time;
    private float attackTime = 0f;
    bool isAttackFinished = false;
    private Collider coll;

    void slimeMove()
    {
        float time = Time.time;
        float zigzag = Mathf.Sin(time * Mathf.PI);
        enemy.moveDirection.x = zigzag;
        transform.Translate(enemy.moveDirection * enemy.speed * Time.deltaTime);
    }

    void slimeAttack()
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
        coll = GetComponent<Collider>();
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
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
            slimeMove();
        }

        if (enemy._state == EnemyStatus.State.Attack)
        {
            slimeAttack();
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
            Debug.Log(WaveSpawner.instance.activeEnemies + " , " + WaveSpawner.instance.spawnEnemies);
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
