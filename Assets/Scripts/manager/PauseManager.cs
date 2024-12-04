using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public PauseUISkillScript pas;
    public static bool GameIsPaused = false; // 현재 게임 상태
    public GameObject pauseMenuUI; // 일시정지 메뉴 UI
    public GameObject pauseButton;

    // 게임 재개 메서드
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Pause UI 비활성화
        Time.timeScale = 1f; // 게임 진행 재개
        GameIsPaused = false; // 게임이 진행 상태로 변경
    }

    // 게임 일시정지 메서드
    public void Pause()
    {
        pas.updatePanel();
        pauseMenuUI.SetActive(true); // Pause UI 활성화
        Debug.Log("show pause");
        Time.timeScale = 0f; // 게임 멈춤
        GameIsPaused = true; // 게임이 일시정지 상태로 변경
    }

 

    // 게임 종료 메서드
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 종료
#else
        Application.Quit(); // 빌드 환경에서 종료
#endif
    }
}