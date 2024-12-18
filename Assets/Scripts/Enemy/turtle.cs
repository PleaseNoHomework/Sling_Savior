using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtle : MonoBehaviour
{
    //벽에 돌진하여 폭발

    public AudioSource hitAudio;
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

    private void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        turtleMove();

        if (enemy.currentHP <= 0)
        {
            WaveSpawner.instance.activeEnemies--;
            Debug.Log(WaveSpawner.instance.activeEnemies + " , " + WaveSpawner.instance.spawnEnemies);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            hitAudio.Play();
            WaveSpawner.instance.activeEnemies--;
            HPManager.instance.baseHP -= 2;
            Destroy(gameObject);
        }
    }

}
