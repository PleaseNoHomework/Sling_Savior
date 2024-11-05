using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour
{
    public EnemyStatus enemy;

    public float moveX; //�󸶳� �ٲ�� ��
    public float changeX; //��

    void slimeMove()
    {
        float time = Time.time;
        float zigzag = Mathf.Sin(time * Mathf.PI); // time * Mathf.PI�� 2��, *2�� 1��
        enemy.moveDirection.x = zigzag;
        transform.Translate(enemy.moveDirection * enemy.speed * Time.deltaTime);

    }


    // Update is called once per frame
    void Update()
    {
        if (enemy._state == EnemyStatus.State.Move)
        {
            slimeMove();
        }
    }
}
