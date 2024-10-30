using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    private Vector3 ballDirection;
    public float speed;
    private int directionFlag;
    private float damage;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        ballDirection = Vector3.zero;
        directionFlag = 0;
        spawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (slingManager.instance.shootFlag == 1 && directionFlag == 0)
        {
            ballDirection = slingManager.instance.ballDirection;
            Debug.Log("shtto : " + ballDirection * speed);
            directionFlag = 1;
        }
        transform.Translate(ballDirection * speed * Time.deltaTime);

        if(directionFlag == 1)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime > 5f) Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }


}
