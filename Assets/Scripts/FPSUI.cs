using UnityEngine;
using TMPro; // TextMeshPro를 사용하는 경우 필요

public class FPSUI : MonoBehaviour
{
    public TMP_Text fpsText; // TextMeshPro를 사용하는 경우
    // public Text fpsText; // UnityEngine.UI.Text를 사용하는 경우

    private float deltaTime = 0.0f;

    void Update()
    {
        // 델타 타임을 사용해 FPS 계산
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        // 텍스트에 FPS 값 표시
        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
    }
}
