using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtle : MonoBehaviour
{
    //벽에 돌진하여 폭발하는 에너미

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
        turtleMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            Debug.Log("Collision!");
            Destroy(gameObject);
        }
    }

}
