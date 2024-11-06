/*using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject gameStartCanvas;  // ���� ���� UI ĵ����
    public GameObject gamePlayUI;       // ���� �÷��� UI
    public Camera initialCamera;        // �ʱ� ī�޶�
    public Camera mainCamera;           // ���� ���� ī�޶�

    private void Start()
    {
        // ���� �� ���� �ʱ�ȭ
        gamePlayUI.SetActive(false);                // ���� �÷��� UI ��Ȱ��ȭ
        gameStartCanvas.SetActive(true);            // ���� ���� UI Ȱ��ȭ
        initialCamera.gameObject.SetActive(true);   // �ʱ� ī�޶� Ȱ��ȭ
        mainCamera.gameObject.SetActive(false);     // ���� ī�޶� ��Ȱ��ȭ
    }

    public void OnStartGameButtonClicked()
    {
        // ��ư Ŭ�� �� ���� ���� ������ ����
        StartCoroutine(StartGameSequence());
    }

    private IEnumerator StartGameSequence()
    {
        // 1. ���� ���� UI ��Ȱ��ȭ
        gameStartCanvas.SetActive(false);
        Debug.Log("GameStartCanvas Hidden");

        // 2. ī�޶� ��ȯ �� UI Ȱ��ȭ
        initialCamera.gameObject.SetActive(false);   // �ʱ� ī�޶� ��Ȱ��ȭ
        mainCamera.gameObject.SetActive(true);       // ���� ī�޶� Ȱ��ȭ
        gamePlayUI.SetActive(true);                  // ���� �÷��� UI Ȱ��ȭ
        Debug.Log("Cameras switched and GamePlay UI activated");

        yield return null;
    }
}*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject gameStartCanvas;   // ���� ���� UI ĵ����
    public GameObject gamePlayUI;        // ���� �÷��� UI
    public Camera initialCamera;         // �ʱ� ī�޶�
    public Camera mainCamera;            // ���� ���� ī�޶�
    public float cameraMoveDuration = 2.0f; // ī�޶� �̵� �ð�

    private void Start()
    {
        // ���� �� ���� �ʱ�ȭ
        gamePlayUI.SetActive(false);                 // ���� �÷��� UI ��Ȱ��ȭ
        gameStartCanvas.SetActive(true);             // ���� ���� UI Ȱ��ȭ
        initialCamera.gameObject.SetActive(true);    // �ʱ� ī�޶� Ȱ��ȭ
        mainCamera.gameObject.SetActive(false);      // ���� ī�޶� ��Ȱ��ȭ
    }

    public void OnStartGameButtonClicked()
    {
        // ��ư Ŭ�� �� ī�޶� ��ȯ ������ ����
        StartCoroutine(StartGameSequence());
    }

    private IEnumerator StartGameSequence()
    {
        gameStartCanvas.SetActive(false);
        Debug.Log("GameStartCanvas Hidden");

        Vector3 startPosition = initialCamera.transform.position;
        Quaternion startRotation = initialCamera.transform.rotation;
        Vector3 endPosition = mainCamera.transform.position;
        Quaternion endRotation = mainCamera.transform.rotation;

        float elapsed = 0f;
        while (elapsed < cameraMoveDuration)
        {
            elapsed += Time.unscaledDeltaTime;  // Time.unscaledDeltaTime�� ����� �ð� ��� ����
            float t = Mathf.Clamp01(elapsed / cameraMoveDuration);

            initialCamera.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            initialCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            yield return null;
        }

        initialCamera.gameObject.SetActive(false);  // �ʱ� ī�޶� ��Ȱ��ȭ
        mainCamera.gameObject.SetActive(true);      // ���� ī�޶� Ȱ��ȭ
        gamePlayUI.SetActive(true);                 // ���� �÷��� UI Ȱ��ȭ

        Debug.Log("Camera transition completed");
    }

}
