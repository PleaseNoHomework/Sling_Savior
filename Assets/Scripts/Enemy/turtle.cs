using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtle : MonoBehaviour
{
    public EnemyStatus enemy;
    public float acc;
    public float nowSpeed;
    void turtleMove()
    {
        float time = Time.deltaTime;
        if (nowSpeed <= enemy.speed)
        {
            nowSpeed += acc * time;
        }
        transform.Translate(enemy.moveDirection * nowSpeed * time);
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        switch (enemy._state)
        {
            case EnemyStatus.State.Move:
                turtleMove();
                break;
            case EnemyStatus.State.Attack:
                break;
            case EnemyStatus.State.Die:
                break;
        }
    }
}
