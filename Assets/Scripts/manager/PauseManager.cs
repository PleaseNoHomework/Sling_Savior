using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;         // PausePanel, �Ͻ����� �� ǥ��
    public Image[] skillIcons;            // ��ų �������� ǥ���� Image �迭
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0; // �Ͻ�����
            DisplaySkillIcons();
        }
        else
        {
            Time.timeScale = 1; // �Ͻ����� ����
        }
    }

    private void DisplaySkillIcons()
    {
        List<SkillData> acquiredSkills = SkillManager.instance.acquiredSkills;

        for (int i = 0; i < skillIcons.Length; i++)
        {
            if (i < acquiredSkills.Count && acquiredSkills[i].icon != null)
            {
                skillIcons[i].sprite = acquiredSkills[i].icon; // ��ų ������ ����
                skillIcons[i].gameObject.SetActive(true);
            }
            else
            {
                skillIcons[i].gameObject.SetActive(false); // �� ������ ��Ȱ��ȭ
            }
        }
    }
}
