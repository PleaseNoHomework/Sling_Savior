using UnityEngine;

public class AimingLine : MonoBehaviour
{
    public Transform slingshot;  // 새총의 위치
    public Transform targetPoint;  // 조준 끝 지점 (예: 마우스 위치)

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))  // 마우스 왼쪽 버튼 클릭 중
        {
            Vector3 start = slingshot.position;
            Vector3 end = targetPoint.position;

            // LineRenderer의 시작점과 끝점 설정
            lineRenderer.SetPosition(0, start);  // 시작점
            lineRenderer.SetPosition(1, end);   // 끝점
        }
        else
        {
            // 마우스를 떼면 조준선을 숨김
            lineRenderer.enabled = false;
        }
    }
}
