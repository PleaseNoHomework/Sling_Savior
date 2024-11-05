using System.Collections;
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
}
