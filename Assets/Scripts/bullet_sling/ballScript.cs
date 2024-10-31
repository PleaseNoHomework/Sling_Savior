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
    public float speed; //공 속도
    private int directionFlag; // 위치 정보 보낼 때
    public float spawnTime; //총알 재장전
    public float availableTime; //관통할 때 잠깐 충돌제거
    public int availableFlag;
    public SphereCollider pierceCollider;
    void Move()
    {
        if(directionFlag == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = transform.position;
                mouseDownPos = Input.mousePosition;
                mouseFlag = 1;
            }

            if (mouseFlag == 1 && Input.GetMouseButton(0))
            {
                mousePos = (mouseDownPos - Input.mousePosition)*0.01f;
                mousePos.z = mousePos.y;
                mousePos.y = 0;

                if (mousePos.z < -50) mousePos.z = -50;
                Vector3 slingPos = startPos - mousePos;
                if (slingPos.z < -65) slingPos.z = -65;
                if (slingPos.x < -40) slingPos.x = -40;
                else if (slingPos.x > 40) slingPos.x = 40;
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
        startPos = transform.position;
        ballDirection = Vector3.zero;
        directionFlag = 0;
        spawnTime = 0f;
        availableTime = 0f;
        availableFlag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (slingManager.instance.shootFlag == 1 && directionFlag == 0)
        {
            ballDirection = slingManager.instance.ballDirection;
            directionFlag = 1;
        }
        transform.Translate(ballDirection * speed * Time.deltaTime);

        if(directionFlag == 1)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime > 5f) Destroy(gameObject);
        }

        if (availableFlag == 1)
        {
            pierceCollider = GetComponent<SphereCollider>();
            pierceCollider.enabled = false;
            availableTime += Time.deltaTime;
            if (availableTime > 0.5f)
            {
                pierceCollider.enabled = true;
                availableTime = 0;
                availableFlag = 0;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.CompareTag("Item"))
        {
            UIManager.instance.UIFlag = 1;
                   }*/

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (gameObject.CompareTag("Bullet")) Destroy(gameObject);
            else
            {
                availableFlag = 1;
            }
        }
        
        if(collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
    }


}
