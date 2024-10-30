using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingScript : MonoBehaviour
{
    public GameObject ball;
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
            Debug.Log("mouse down! " + Input.mousePosition);
            mouseDownPos = Input.mousePosition;
            ready = 1;
        }
        if (Input.GetMouseButtonUp(0)) {
            mouseUpPos = Input.mousePosition;
            if (slingManager.instance.shootFlag == 0 && ready == 1)
            {
                Debug.Log("mouse up! " + Input.mousePosition) ;
                slingManager.instance.shootFlag = 1;
                Vector3 balldirec = -(mouseUpPos - mouseDownPos).normalized;
                balldirec.z = balldirec.y;
                if (balldirec.z < 0) balldirec.z = 0;
                balldirec.y = 0;
                slingManager.instance.ballDirection = balldirec;
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
            if(shootTime > 1f)
            {
                shootTime = 0;
                slingManager.instance.shootFlag = 0;
                Instantiate(ball, ballSpawnPoint, Quaternion.identity);
            }
        }


    }
}
