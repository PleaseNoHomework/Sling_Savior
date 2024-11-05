using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;         // PausePanel, 일시정지 시 표시
    public Image[] skillIcons;            // 스킬 아이콘을 표시할 Image 배열
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
            Time.timeScale = 0; // 일시정지
            DisplaySkillIcons();
        }
        else
        {
            Time.timeScale = 1; // 일시정지 해제
        }
    }

    private void DisplaySkillIcons()
    {
        List<SkillData> acquiredSkills = SkillManager.instance.acquiredSkills;

        for (int i = 0; i < skillIcons.Length; i++)
        {
            if (i < acquiredSkills.Count && acquiredSkills[i].icon != null)
            {
                skillIcons[i].sprite = acquiredSkills[i].icon; // 스킬 아이콘 설정
                skillIcons[i].gameObject.SetActive(true);
            }
            else
            {
                skillIcons[i].gameObject.SetActive(false); // 빈 슬롯은 비활성화
            }
        }
    }
}
