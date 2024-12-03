
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject gameStartUI;   // 게임 시작 UI 캔버스
    public GameObject defaultUI;        // 게임 플레이 UI
    public Camera initialCamera;         // 초기 카메라
    public Camera mainCamera;            // 게임 메인 카메라
    public float cameraMoveDuration = 2.0f; // 카메라 이동 시간
    public GameObject waveUI;

    public TutorialManager tutorialManager;
    private void Start()
    {
        // 시작 시 상태 초기화
        waveUI.SetActive(false);
        defaultUI.SetActive(false);                 // 게임 플레이 UI 비활성화
        gameStartUI.SetActive(true);             // 게임 시작 UI 활성화
        initialCamera.gameObject.SetActive(true);    // 초기 카메라 활성화
        mainCamera.gameObject.SetActive(false);      // 메인 카메라 비활성화
    }
    public void OnTutorialButtonClicked()
    {
        StartCoroutine(TutorialGameSequence());
    }

    public void OnStartGameButtonClicked()
    {
        // 버튼 클릭 시 카메라 전환 시퀀스 시작
        StartCoroutine(StartGameSequence());
    }

    private IEnumerator TutorialGameSequence()
    {
        yield return moveCamera();

        yield return tutorialManager.tutorial();
    }


    private IEnumerator moveCamera()
    {
        gameStartUI.SetActive(false);
        Vector3 startPosition = initialCamera.transform.position;
        Quaternion startRotation = initialCamera.transform.rotation;
        Vector3 endPosition = mainCamera.transform.position;
        Quaternion endRotation = mainCamera.transform.rotation;

        float elapsed = 0f;
        while (elapsed < cameraMoveDuration)
        {
            elapsed += Time.unscaledDeltaTime;  // Time.unscaledDeltaTime을 사용해 시간 경과 측정
            float t = Mathf.Clamp01(elapsed / cameraMoveDuration);

            initialCamera.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            initialCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            yield return null;
        }
        initialCamera.gameObject.SetActive(false);  // 초기 카메라 비활성화
        mainCamera.gameObject.SetActive(true);      // 메인 카메라 활성화
        waveUIScript.instance.gameStartFlag = 1;
        slingScript.instance.canShoot = 1;
    }

    private IEnumerator StartGameSequence()
    {
        yield return moveCamera();
        defaultUI.SetActive(true);                 // 게임 플레이 UI 활성화              
        waveUI.SetActive(true);
        Debug.Log("Camera transition completed");

        yield return null;
    }

}
