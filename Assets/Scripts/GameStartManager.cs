using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject gameStartCanvas;  // 게임 시작 UI 캔버스
    public GameObject gamePlayUI;       // 게임 플레이 UI
    public Camera initialCamera;        // 초기 카메라
    public Camera mainCamera;           // 게임 메인 카메라

    private void Start()
    {
        // 시작 시 상태 초기화
        gamePlayUI.SetActive(false);                // 게임 플레이 UI 비활성화
        gameStartCanvas.SetActive(true);            // 게임 시작 UI 활성화
        initialCamera.gameObject.SetActive(true);   // 초기 카메라 활성화
        mainCamera.gameObject.SetActive(false);     // 메인 카메라 비활성화
    }

    public void OnStartGameButtonClicked()
    {
        // 버튼 클릭 시 게임 시작 시퀀스 실행
        StartCoroutine(StartGameSequence());
    }

    private IEnumerator StartGameSequence()
    {
        // 1. 게임 시작 UI 비활성화
        gameStartCanvas.SetActive(false);
        Debug.Log("GameStartCanvas Hidden");

        // 2. 카메라 전환 및 UI 활성화
        initialCamera.gameObject.SetActive(false);   // 초기 카메라 비활성화
        mainCamera.gameObject.SetActive(true);       // 메인 카메라 활성화
        gamePlayUI.SetActive(true);                  // 게임 플레이 UI 활성화
        Debug.Log("Cameras switched and GamePlay UI activated");

        yield return null;
    }
}
