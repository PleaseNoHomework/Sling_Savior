using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;   // �Ͻ����� ȭ�� �г�
    private bool isPaused = false;  // ���� �Ͻ����� �������� ����

    void Start()
    {
        // �гθ� ��Ȱ��ȭ
        pausePanel.SetActive(false);
    }

    void Update()
    {
        // E Ű�� ������ �Ͻ����� ���¸� ���
        if (Input.GetKeyDown(KeyCode.E))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;   // �Ͻ����� ���� ����

        if (isPaused)
        {
            // ���� �Ͻ�����
            Time.timeScale = 0;
            pausePanel.SetActive(true); // �Ͻ����� �г� Ȱ��ȭ
        }
        else
        {
            // ���� �簳
            Time.timeScale = 1;
            pausePanel.SetActive(false); // �Ͻ����� �г� ��Ȱ��ȭ
        }
    }
}
