using UnityEngine;

public class AimingLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        // LineRenderer 컴포넌트 추가
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // LineRenderer 설정
        lineRenderer.positionCount = 2; // 시작점과 끝점 설정
        lineRenderer.startWidth = 0.1f; // 선의 시작 두께
        lineRenderer.endWidth = 0.1f;   // 선의 끝 두께
        lineRenderer.useWorldSpace = true; // 월드 좌표계 기준 사용
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")); // 색깔 설정
        lineRenderer.startColor = Color.green; // 시작 색상
        lineRenderer.endColor = Color.green;   // 끝 색상
        lineRenderer.enabled = false;          // 기본적으로 비활성화
    }
}
