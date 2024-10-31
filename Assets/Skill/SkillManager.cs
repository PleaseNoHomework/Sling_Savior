using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [Header("UI Elements")]
    public GameObject skillSelectionPanel; // 스킬 선택 UI 패널
    public Button[] skillButtons;          // 3개의 스킬 선택 버튼

    [Header("Skill Data")]
    public List<SkillData> allSkills;           // 전체 스킬 목록
    public List<SkillData> availableSkills;     // 획득 가능한 스킬 목록
    public List<SkillData> acquiredSkills;      // 획득한 스킬 목록

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // 모든 스킬을 획득 가능 목록에 추가
        foreach (SkillData skill in allSkills)
        {
            if (!availableSkills.Contains(skill))
            {
                availableSkills.Add(skill);
            }
        }
        skillSelectionPanel.SetActive(false); // 시작 시 스킬 선택 UI 비활성화
    }

    private void Update()
    {
        // S 키 입력 시 스킬 선택 UI 호출 및 게임 일시정지
        if (Input.GetKeyDown(KeyCode.S) && !skillSelectionPanel.activeSelf)
        {
            OpenSkillSelection();
        }
    }

    public void OpenSkillSelection()
    {
        // 게임 일시정지
        Time.timeScale = 0;
        skillSelectionPanel.SetActive(true);

        // 3개의 무작위 스킬 선택
        List<SkillData> randomSkills = GetRandomSkills(3);

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (i < randomSkills.Count)
            {
                int skillIndex = i;  // 버튼 인덱스를 저장하여 익명 함수에서 사용
                SkillData skill = randomSkills[i];

                skillButtons[i].transform.Find("SkillText").GetComponent<Text>().text = skill.skillName;
                skillButtons[i].transform.Find("SkillDescription").GetComponent<Text>().text = skill.description;
                skillButtons[i].transform.Find("SkillIcon").GetComponent<Image>().sprite = skill.icon;

                skillButtons[i].onClick.RemoveAllListeners();
                skillButtons[i].onClick.AddListener(() => SelectSkill(skill));
            }
            else
            {
                // 선택 가능한 스킬이 부족한 경우 버튼 비활성화
                skillButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> selectedSkills = new List<SkillData>();
        HashSet<int> selectedIndices = new HashSet<int>(); // 선택된 인덱스를 기록하여 중복 방지

        while (selectedSkills.Count < count && selectedSkills.Count < availableSkills.Count)
        {
            int randomIndex = Random.Range(0, availableSkills.Count);

            // 중복된 인덱스가 아닌 경우에만 선택
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);              // 인덱스 기록
                selectedSkills.Add(availableSkills[randomIndex]); // 스킬 추가
            }
        }

        return selectedSkills;
    }


    public void SelectSkill(SkillData selectedSkill)
    {
        // 스킬 획득 처리
        selectedSkill.isActive = true;

        if (selectedSkill.skillType == SkillType.Passive)
        {
            acquiredSkills.Add(selectedSkill);  // 패시브는 중첩 가능
        }
        else
        {
            // 액티브와 스페셜은 중복 방지 및 중첩 방지
            acquiredSkills.RemoveAll(s => s.skillType == selectedSkill.skillType);
            acquiredSkills.Add(selectedSkill);
            availableSkills.Remove(selectedSkill);  // 획득 목록에서 제거
        }

        /* SlingManager에 스킬 선택 전달
        SlingManager.instance.choiceFlag = 1;
        SlingManager.instance.skill = selectedSkill;*/

        // 스킬 선택 UI 닫기 및 게임 재개
        skillSelectionPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
