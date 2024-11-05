using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : MonoBehaviour
{
    //벽에 가까이 붙으면 공격
    public EnemyStatus enemy;
    // Start is called before the first frame update

    void spiderMove()
    {
        float time = Time.deltaTime;
        transform.Translate(enemy.moveDirection * enemy.speed * time);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy._state == EnemyStatus.State.Move)
        {
            spiderMove();
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