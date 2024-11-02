using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [Header("UI Elements")]
    public GameObject skillSelectionPanel; // ��ų ���� UI �г�
    public Button[] skillButtons;          // 3���� ��ų ���� ��ư

    [Header("Skill Data")]
    public List<SkillData> allSkills;           // ��ü ��ų ���
    public List<SkillData> availableSkills;     // ȹ�� ������ ��ų ���
    public List<SkillData> acquiredSkills;      // ȹ���� ��ų ���

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // ��� ��ų�� ȹ�� ���� ��Ͽ� �߰�
        foreach (SkillData skill in allSkills)
        {
            if (!availableSkills.Contains(skill))
            {
                availableSkills.Add(skill);
            }
        }
        skillSelectionPanel.SetActive(false); // ���� �� ��ų ���� UI ��Ȱ��ȭ
    }

    private void Update()
    {
        // S Ű �Է� �� ��ų ���� UI ȣ�� �� ���� �Ͻ�����
        if (Input.GetKeyDown(KeyCode.S) && !skillSelectionPanel.activeSelf)
        {
            OpenSkillSelection();
        }
    }

    public void OpenSkillSelection()
    {
        // ���� �Ͻ�����
        Time.timeScale = 0;
        skillSelectionPanel.SetActive(true);

        // 3���� ������ ��ų ����
        List<SkillData> randomSkills = GetRandomSkills(3);

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (i < randomSkills.Count)
            {
                int skillIndex = i;  // ��ư �ε����� �����Ͽ� �͸� �Լ����� ���
                SkillData skill = randomSkills[i];

                skillButtons[i].transform.Find("SkillText").GetComponent<Text>().text = skill.skillName;
                skillButtons[i].transform.Find("SkillDescription").GetComponent<Text>().text = skill.description;
                skillButtons[i].transform.Find("SkillIcon").GetComponent<Image>().sprite = skill.icon;

                skillButtons[i].onClick.RemoveAllListeners();
                skillButtons[i].onClick.AddListener(() => SelectSkill(skill));
            }
            else
            {
                // ���� ������ ��ų�� ������ ��� ��ư ��Ȱ��ȭ
                skillButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> selectedSkills = new List<SkillData>();
        HashSet<int> selectedIndices = new HashSet<int>(); // ���õ� �ε����� ����Ͽ� �ߺ� ����

        while (selectedSkills.Count < count && selectedSkills.Count < availableSkills.Count)
        {
            int randomIndex = Random.Range(0, availableSkills.Count);

            // �ߺ��� �ε����� �ƴ� ��쿡�� ����
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);              // �ε��� ���
                selectedSkills.Add(availableSkills[randomIndex]); // ��ų �߰�
            }
        }

        return selectedSkills;
    }


    public void SelectSkill(SkillData selectedSkill)
    {
        // ��ų ȹ�� ó��
        selectedSkill.isActive = true;

        if (selectedSkill.skillType == SkillType.Passive)
        {
            acquiredSkills.Add(selectedSkill);  // �нú�� ��ø ����
        }
        else
        {
            // ��Ƽ��� ������� �ߺ� ���� �� ��ø ����
            acquiredSkills.RemoveAll(s => s.skillType == selectedSkill.skillType);
            acquiredSkills.Add(selectedSkill);
            availableSkills.Remove(selectedSkill);  // ȹ�� ��Ͽ��� ����
        }

        /* SlingManager�� ��ų ���� ����
        SlingManager.instance.choiceFlag = 1;
        SlingManager.instance.skill = selectedSkill;*/

        // ��ų ���� UI �ݱ� �� ���� �簳
        skillSelectionPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
