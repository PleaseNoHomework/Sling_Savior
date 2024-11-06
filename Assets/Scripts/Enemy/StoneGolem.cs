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

    // Update is called once per frame
    void Update()
    {
        if (enemy._state == EnemyStatus.State.Move)
        {
            StoneGolemMove();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            enemy._state = EnemyStatus.State.Attack;
        }
    }
}
