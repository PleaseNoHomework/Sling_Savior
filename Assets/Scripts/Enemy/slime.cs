using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour
{
    public EnemyStatus enemy;

    public float moveX; //얼마나 바뀌는 지
    public float changeX; //빈도

    void slimeMove()
    {
        float time = Time.time;
        float zigzag = Mathf.Sin(time * Mathf.PI * 0.5f) * 2; // time * Mathf.PI는 2초, *2시 1초
        enemy.moveDirection.x = zigzag;
        transform.Translate(enemy.moveDirection * enemy.speed * Time.deltaTime);

    }


    // Update is called once per frame
    void Update()
    {
        switch (enemy._state)
        {
            case EnemyStatus.State.Move:
                slimeMove();
                break;
            case EnemyStatus.State.Attack:
                break;
            case EnemyStatus.State.Die:
                break;
        }
    }
}
