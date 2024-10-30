using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    private Vector3 ballDirection;
    private Vector3 mouseDownPos;
    private Vector3 mousePos;
    private Vector3 startPos;
    private int mouseFlag = 0;
    public float speed;
    private int directionFlag;
    private float damage;
    private float spawnTime;
    private BoxCollider coll;

    void Move()
    {
        if(directionFlag == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = gameObject.transform.position;
                mouseDownPos = Input.mousePosition;
                mouseFlag = 1;
            }

            if (mouseFlag == 1 && Input.GetMouseButton(0))
            {
                mousePos = (mouseDownPos - Input.mousePosition)*0.01f;
                mousePos.z = mousePos.y;
                mousePos.y = 0;

                if (mousePos.z < 0) mousePos.z = 0;
                Vector3 slingPos = startPos - mousePos;
                if (slingPos.z < -10) slingPos.z = -10;
                if (slingPos.x < -5) slingPos.x = -5;
                else if (slingPos.x > 5) slingPos.x = 5;
                transform.position = slingPos;


            }
            if(Input.GetMouseButtonUp(0))
            {
                if(slingManager.instance.shootFlag == 0)
                    transform.position = startPos;
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        ballDirection = Vector3.zero;
        directionFlag = 0;
        spawnTime = 0f;
        coll = GetComponent<BoxCollider>();
        coll.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (slingManager.instance.shootFlag == 1 && directionFlag == 0)
        {
            ballDirection = slingManager.instance.ballDirection;
            directionFlag = 1;
            coll.enabled = true;
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
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }


}
