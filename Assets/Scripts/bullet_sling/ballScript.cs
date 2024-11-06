using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    public Camera mainCamera;

    private Vector3 ballDirection;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 defaultPos;

    public Vector3 rays = new Vector3(50,0,0);

    private int mouseFlag = 0;
    public float speed; //공 속도
    private int directionFlag; // 위치 정보 보낼 때
    public float spawnTime; //총알 재장전
    public float availableTime; //관통할 때 잠깐 충돌제거

    public int availableFlag;
    public SphereCollider pierceCollider;

    public float maxX, minX, maxZ, minZ;
    void MovePos()
    {
        if(directionFlag == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = GetMouseWorldPosition();
                mouseFlag = 1;
            }


            if (mouseFlag == 1 && Input.GetMouseButton(0))
            {
                endPos = GetMouseWorldPosition();
                rotateBullet(startPos, endPos);
                Vector3 pullPos = -(endPos - startPos) * 0.1f;
                transform.position = (defaultPos - pullPos);

            }
            if(Input.GetMouseButtonUp(0) && mouseFlag == 1)
            {
                ballDirection = (endPos - startPos) * 0.05f;
                if (ballDirection.magnitude <= 0.5f)
                {
                    ballDirection = ballDirection.normalized * 0.5f;
                }
                else if (ballDirection.magnitude >= 1f)
                {
                    ballDirection = ballDirection.normalized;
                }
                mouseFlag = 0;

                if (ballDirection.z < 0)
                {
                    directionFlag = 1;
                    slingManager.instance.shootFlag = 1;
                }
                else
                {
                    transform.position = defaultPos;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }

            }
        }

    }

    void rotateBullet(Vector3 start, Vector3 end)
    {
        Vector3 rotates = end - start;
        float angle = Mathf.Atan2(rotates.x, rotates.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        float size = 1f * (2 + newSkillManager.instance.skills[6].nowSkill); //사이즈
        transform.localScale = new Vector3(size, size, size);


        defaultPos = transform.position;

        ballDirection = Vector3.zero;
        directionFlag = 0;
        spawnTime = 0f;
        availableTime = 0f;
    }

    // Update is called once per frame
    void Update()
    { //shootFlag가 0일 때 조준 발사 가능, 발사 시 directionFlag = 1
        if(slingManager.instance.shootFlag == 0) MovePos();

        

        if(directionFlag == 1)
        {
            transform.Translate(new Vector3(0,0,-1) * speed * Time.deltaTime);
            spawnTime += Time.deltaTime;
            if (spawnTime > 5f) Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Item"))
        {
            Destroy(gameObject);
        }
        
        if(collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("wall");
            Destroy(gameObject);
        }
            
    }

    private Vector3 GetMouseWorldPosition()
    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);  // 마우스 위치를 기준으로 광선 쏘기
        Plane xzPlane = new Plane(Vector3.up, Vector3.zero);  // xz 평면 설정 (y축이 평면의 법선 벡터)

        if (xzPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);  // 광선과 xz 평면의 교차점 반환
        }

        return transform.position;  // 평면과 교차하지 않으면 현재 위치 반환
    }
}
