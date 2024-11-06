using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUIScript : MonoBehaviour
{
    [Header("UI Components")]


    public TMP_Text hpText;
    public TMP_Text waveText;
    // Wave 정보
    public Image[] skillIcons;       // 스킬아이코ㄴ

    private HPManager hpManager;
    private GameManager gameManager;
    private WaveSpawner waveSpawner;
    private newSkillManager skillManager;

    void Start()
    {
        hpManager = HPManager.instance;
        gameManager = GameManager.instance;
        skillManager = newSkillManager.instance;
        waveSpawner = WaveSpawner.instance;

        // UI 초기화
        UpdateHP();
        UpdateWave();
        UpdateSkillIcons();
    }

    void Update()
    {
        UpdateHP();
        UpdateWave();
        UpdateSkillIcons();
    }

    private void UpdateHP()
    {
        if (hpManager != null)
        {
            hpText.text = $"{hpManager.baseHP}/5";
        }
    }

    private void UpdateWave()
    {
        // GameManager에서 currentWave 값을 받아와서 텍스트에 반영
        if (waveSpawner != null)
        {
            waveText.text = $"{waveSpawner.currentWave}";
        }
    }

    private void UpdateSkillIcons()
    {
        // 획득한 스킬 아이콘을 SkillManager에서 받아와 UI에 업데이트
        if (skillManager != null && skillIcons.Length > 0)
        {
            List<SkillData> acquiredSkills = skillManager.acquiredSkills;

            for (int i = 0; i < skillIcons.Length; i++)
            {
                if (i < acquiredSkills.Count && acquiredSkills[i].icon != null)
                {
                    skillIcons[i].sprite = acquiredSkills[i].icon;     // 스킬 아이콘 설정
                    skillIcons[i].gameObject.SetActive(true);          // 아이콘 활성화
                }
                else
                {
                    skillIcons[i].gameObject.SetActive(false);         // 빈 슬롯은 비활성화
                }
            }
        }
    }
}
