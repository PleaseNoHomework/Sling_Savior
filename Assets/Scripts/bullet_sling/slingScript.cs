using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingScript : MonoBehaviour
{
    public static slingScript instance;
    public GameObject ball;
    public GameObject leftBall;
    public GameObject rightBall;
    public float speed;
    public GameObject pierceBall;
    public Vector3 ballSpawnPoint;
    public float maxZ, minZ, maxX, minX;
    private float shootTime;

    public int canShoot = 0; //게임이 시작되면 1로 바꿔준다. 그 전까지는 쏠 수 없다.
    public int shootFlag = 0;

    public slingManager slingmanager;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        shootTime = 0;
        slingmanager = slingManager.instance;
        GameObject spawnBall = Instantiate(ball, ballSpawnPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(shootFlag == 1 && canShoot == 1)
        {

            shootTime += Time.deltaTime;
            if (shootTime > slingmanager.shootCoolTime)
            {
                shootTime = 0;
                shootFlag = 0;
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
