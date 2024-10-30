using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingScript : MonoBehaviour
{
    public GameObject ball;
    public GameObject pierceBall;
    public Vector3 ballSpawnPoint;
    public float maxZ, minZ, maxX, minX;
    private float shootTime;

    private Vector3 mouseDownPos;
    private Vector3 mouseUpPos;
    private int ready = 0;
    void shoot()
    {
        if(Input.GetMouseButtonDown(0) && slingManager.instance.shootFlag == 0)
        {
            mouseDownPos = Input.mousePosition;
            ready = 1;
        }
        if (Input.GetMouseButtonUp(0)) {
            mouseUpPos = Input.mousePosition;
            if (slingManager.instance.shootFlag == 0 && ready == 1)
            {
                if (Vector3.Magnitude((mouseDownPos - mouseUpPos)) > 100f) {                 
                    Vector3 balldirec = -(mouseUpPos - mouseDownPos).normalized;
                    balldirec.z = balldirec.y;
                    if (balldirec.z < 0) balldirec.z = 0;
                    balldirec.y = 0;
                    slingManager.instance.ballDirection = balldirec;
                    slingManager.instance.shootFlag = 1;
                }

                ready = 0;
            }
        }
    }



    void Start()
    {
        shootTime = 0;
        Instantiate(ball, ballSpawnPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        shoot();

        if (slingManager.instance.shootFlag == 1)
        {
            shootTime += Time.deltaTime;
            if(shootTime > slingManager.instance.shootCoolTime)
            {
                shootTime = 0;
                slingManager.instance.shootFlag = 0;
                if (slingManager.instance.pierceFlag == 0) 
                    Instantiate(ball, ballSpawnPoint, Quaternion.identity);
                else
                {
                    Instantiate(pierceBall, ballSpawnPoint, Quaternion.identity);
                }
                    
            }
        }


    }
}
