using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUIScript : MonoBehaviour
{
    [Header("UI Components")]


    public TMP_Text hpText;
    public TMP_Text waveText;
    // Wave ����
    public Image[] skillIcons;       // ��ų�����ڤ�

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

        // UI �ʱ�ȭ
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
        // GameManager���� currentWave ���� �޾ƿͼ� �ؽ�Ʈ�� �ݿ�
        if (waveSpawner != null)
        {
            waveText.text = $"{waveSpawner.currentWave}";
        }
    }

    private void UpdateSkillIcons()
    {
        // ȹ���� ��ų �������� SkillManager���� �޾ƿ� UI�� ������Ʈ
        if (skillManager != null && skillIcons.Length > 0)
        {
            List<SkillData> acquiredSkills = skillManager.acquiredSkills;

            for (int i = 0; i < skillIcons.Length; i++)
            {
                if (i < acquiredSkills.Count && acquiredSkills[i].icon != null)
                {
                    skillIcons[i].sprite = acquiredSkills[i].icon;     // ��ų ������ ����
                    skillIcons[i].gameObject.SetActive(true);          // ������ Ȱ��ȭ
                }
                else
                {
                    skillIcons[i].gameObject.SetActive(false);         // �� ������ ��Ȱ��ȭ
                }
            }
        }
    }
}
