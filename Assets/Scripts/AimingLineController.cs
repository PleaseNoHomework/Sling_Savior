using UnityEngine;

public class AimingLineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform bulletSpawnPoint; // 조준선 시작점 (총구 끝)
    public LayerMask aimLayerMask;     // 조준 가능한 오브젝트 레이어

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼을 누르고 있을 때
        {
            // 1. 조준선 시작점 설정
            lineRenderer.SetPosition(0, bulletSpawnPoint.position);

            // 2. 마우스 위치를 월드 좌표로 변환 (Raycast 사용)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimLayerMask))
            {
                // 3. Raycast가 충돌한 지점을 끝점으로 설정
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                // 4. 충돌하지 않을 경우, 적당한 길이로 설정
                lineRenderer.SetPosition(1, ray.GetPoint(10));
            }
        }
        else
        {
            // 마우스 버튼을 떼면 조준선을 숨김
            lineRenderer.SetPosition(0, bulletSpawnPoint.position);
            lineRenderer.SetPosition(1, bulletSpawnPoint.position);
        }
    }
}
