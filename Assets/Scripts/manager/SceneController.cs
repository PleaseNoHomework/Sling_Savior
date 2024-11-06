using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string SceneName;
    // 특정 씬으로 전환하는 메서드
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    // 게임 오버 씬으로 전환
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene"); 
    }

    // 게임 클리어 씬으로 전환
    public void LoadGameClearScene()
    {
        SceneManager.LoadScene("GameClearScene");
    }
}

