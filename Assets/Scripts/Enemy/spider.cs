using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : MonoBehaviour
{
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
        switch (enemy._state)
        {
            case EnemyStatus.State.Move:
                spiderMove();
                break;
            case EnemyStatus.State.Attack:
                break;
            case EnemyStatus.State.Die:
                break;
        }
        
    }
}