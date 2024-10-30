using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;   // 일시정지 화면 패널
    private bool isPaused = false;  // 현재 일시정지 상태인지 여부

    void Start()
    {
        // 패널만 비활성화
        pausePanel.SetActive(false);
    }

    void Update()
    {
        // E 키를 누르면 일시정지 상태를 토글
        if (Input.GetKeyDown(KeyCode.E))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;   // 일시정지 상태 변경

        if (isPaused)
        {
            // 게임 일시정지
            Time.timeScale = 0;
            pausePanel.SetActive(true); // 일시정지 패널 활성화
        }
        else
        {
            // 게임 재개
            Time.timeScale = 1;
            pausePanel.SetActive(false); // 일시정지 패널 비활성화
        }
    }
}
