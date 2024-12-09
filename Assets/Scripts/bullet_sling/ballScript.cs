using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    public Camera mainCamera;
    private AudioSource fireAudio;
    private AudioSource drawAudio;
    public Vector3 ballDirection;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 defaultPos;

    private int mouseFlag = 0;
    public float speed; // 공 속도
    public int directionFlag; // 위치 정보 보낼 때
    public float spawnTime; // 총알 재장전

    private int soundFlag = 0;
    public int availableFlag;

    public float maxX, minX, maxZ, minZ;

    // LineRenderer 추가
    private LineRenderer aimingLine;

    public void movePos() // 방향 지정해주기
    {
        if (directionFlag == 0)
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
                Vector3 pullPos = -(endPos - startPos) * 0.25f; //공이 당겨진 위치
                transform.position = (defaultPos - pullPos);

                // 조준선 업데이트
                UpdateAimingLine(defaultPos, defaultPos - ballDirection.normalized * 25f); // 방향을 반대로 설정
                ballDirection = (endPos - startPos).normalized; // 방향 갱신

                if (pullPos.magnitude < 0.1f) aimingLine.material.color = Color.red;

                if (pullPos.magnitude >= 0.1f && soundFlag == 0)
                {
                    drawAudio.Play();
                    soundFlag = 1;
                    aimingLine.material.color = Color.green;
                }
            }

            if (Input.GetMouseButtonUp(0) && mouseFlag == 1)
            {
                ballDirection = (endPos - startPos) * 0.01f; //볼 벡터
                ballDirection = ballDirection.normalized * 0.5f;
                /*
                if (ballDirection.magnitude <= 0.5f && ballDirection.magnitude > 0.3f)
                {
                    ballDirection = ballDirection.normalized * 0.5f;
                }*/
                /*
                else if (ballDirection.magnitude >= 3f)
                {
                    ballDirection = ballDirection.normalized * 3f;
                }*/

                mouseFlag = 0;

                // 조준선 숨기기
                aimingLine.enabled = false;

                if (ballDirection.z < 0 && ballDirection.magnitude >= 0.3f)
                {
                    //튜토리얼
                    TutorialManager.instance.ballCount++;

                    directionFlag = 1; // 방향 설정
                    slingScript.instance.shootFlag = 1;
                    fireAudio.Play();
                    extraBall.instance.setDirection(-ballDirection);
                }
                else
                {
                    transform.position = defaultPos;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
    }

    void rotateBullet(Vector3 start, Vector3 end) // 조준할 때 객체 회전시키기
    {
        Vector3 rotates = end - start;
        float angle = Mathf.Atan2(rotates.x, rotates.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void setDirection(Vector3 direction)
    {
        ballDirection = direction;
        directionFlag = 1;
    }

    // 조준선을 업데이트하는 함수
    void UpdateAimingLine(Vector3 start, Vector3 end)
    {
        if (aimingLine != null)
        {
            aimingLine.enabled = true;
            aimingLine.SetPosition(0, start); // 시작점
            aimingLine.SetPosition(1, end);   // 끝점
        }
    }

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        fireAudio = audioSources[0];
        drawAudio = audioSources[1];

        transform.rotation = Quaternion.Euler(0, 180, 0);
        float size = 1f * (2 + newSkillManager.instance.skills[6].nowSkill); //사이즈
        if (newSkillManager.instance.skills[2].nowSkill == 1)
        {
            transform.localScale = new Vector3(size * 3f, size * 3f, size);
        }
        else
            transform.localScale = new Vector3(size, size, size);

        defaultPos = transform.position;

        ballDirection = Vector3.zero;
        directionFlag = 0;
        spawnTime = 0f;

        // LineRenderer 초기화
        aimingLine = GetComponent<LineRenderer>();
        if (aimingLine == null)
        {
            aimingLine = gameObject.AddComponent<LineRenderer>();
        }

        // LineRenderer 설정
        aimingLine.positionCount = 2;
        aimingLine.startWidth = 0.05f;
        aimingLine.endWidth = 0.05f;
        aimingLine.useWorldSpace = true;
        aimingLine.material = new Material(Shader.Find("Unlit/Color"));
        aimingLine.material.color = Color.red;
        aimingLine.enabled = false; // 초기 상태에서 비활성화
    }

    void Update()
    {//shootFlag가 0일 때 조준 발사 가능, 발사 시 directionFlag = 1
        if (slingScript.instance.shootFlag == 0 && slingScript.instance.canShoot == 1) movePos();

        if (directionFlag == 1)
        {
            transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
            spawnTime += Time.deltaTime;
            if (spawnTime > 5f) Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Item"))
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Wall"))
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
