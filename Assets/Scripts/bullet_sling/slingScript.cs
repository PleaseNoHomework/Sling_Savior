using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingScript : MonoBehaviour
{
    public GameObject ball;
    public GameObject leftBall;
    public GameObject rightBall;
    public float speed;
    public GameObject pierceBall;
    public Vector3 ballSpawnPoint;
    public float maxZ, minZ, maxX, minX;
    private float shootTime;
    slingManager slingmanager;


    void Start()
    {
        slingmanager = slingManager.instance;
        shootTime = 0;
        Instantiate(ball, ballSpawnPoint, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        if(slingmanager.shootFlag == 1)
        {
            shootTime += Time.deltaTime;
            if (shootTime > slingmanager.shootCoolTime)
            {
                shootTime = 0;
                slingmanager.shootFlag = 0;
                if (slingmanager.pierceFlag == 1)
                    Instantiate(pierceBall, ballSpawnPoint, Quaternion.identity);
                else if (slingmanager.multiFlag == 1)
                {
                    Vector3 newBallPos1 = ballSpawnPoint;
                    Vector3 newBallPos2 = ballSpawnPoint;
                    newBallPos1.x += 5f;
                    newBallPos2.x -= 5f;
                    Instantiate(ball, ballSpawnPoint, Quaternion.identity);
                    Instantiate(leftBall, newBallPos1, Quaternion.identity);

                }
                else
                {
                    Instantiate(ball, ballSpawnPoint, Quaternion.identity);
                }
            }
               
        }

    }
}
