using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillSelectionUI : MonoBehaviour
{
    public GameObject selectionPanel;       // ��ų ���� �г�
    public Button[] skillButtons;           // 3���� ��ų ��ư
    private SkillManager slingManager;      // SlingManager ����

    void Start()
    {
        slingManager = FindObjectOfType<SkillManager>();
        selectionPanel.SetActive(false);   // ���� �� ��Ȱ��ȭ
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !selectionPanel.activeSelf)
        {
            ShowSkillOptions();
        }
    }

    public void ShowSkillOptions()
    {
        Time.timeScale = 0;
        selectionPanel.SetActive(true);

        List<SkillData> availableSkills = GetRandomSkills(3);

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (i < availableSkills.Count)
            {
                skillButtons[i].transform.Find("Icon").GetComponent<Image>().sprite = availableSkills[i].icon;
                skillButtons[i].transform.Find("Description").GetComponent<Text>().text = availableSkills[i].description;

                skillButtons[i].onClick.RemoveAllListeners();
                skillButtons[i].onClick.AddListener(() => SelectSkill(availableSkills[i]));
            }
            else
            {
                SetDummyButton(skillButtons[i]);  // DUMMY ����
            }
        }
    }

    private List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> availableSkills = slingManager.allSkills.FindAll(skill => !slingManager.acquiredSkills.Contains(skill));
        List<SkillData> randomSkills = new List<SkillData>();

        while (randomSkills.Count < count && availableSkills.Count > 0)
        {
            int randomIndex = Random.Range(0, availableSkills.Count);
            randomSkills.Add(availableSkills[randomIndex]);
            availableSkills.RemoveAt(randomIndex);
        }

        return randomSkills;
    }

    private void SelectSkill(SkillData selectedSkill)
    {
        slingManager.AcquireSkill(selectedSkill);
        selectionPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void SetDummyButton(Button button)
    {
        button.transform.Find("Icon").GetComponent<Image>().sprite = null;  // ������ ����
        button.transform.Find("Description").GetComponent<Text>().text = "No Skill Available";
        button.onClick.RemoveAllListeners();
    }
}
