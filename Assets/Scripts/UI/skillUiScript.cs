using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUiScript : MonoBehaviour
{
    public static SkillUiScript instance;

    [Header("UI References")]
    public List<Button> buttons; // 기존 버튼 리스트
    public GameObject activePrefab; // Active 타입 버튼 Prefab
    public GameObject passivePrefab; // Passive 타입 버튼 Prefab
    public GameObject specialPrefab; // Special 타입 버튼 Prefab

    private List<GameObject> createdButtons = new List<GameObject>(); // 생성된 버튼 관리 리스트

    public void SetButtons(HashSet<int> skillIndices)
    {
        // 이전에 생성된 버튼 제거
        ClearCreatedButtons();

        int i = 0;
        foreach (int index in skillIndices)
        {
            SkillData skill = newSkillManager.instance.skills[index];

            // Prefab 가져오기
            GameObject prefabToUse = GetButtonPrefabBySkillType(skill.skillType);

            // 기존 버튼 Transform 정보 가져오기
            Transform existingButtonTransform = buttons[i].transform;

            // Prefab 버튼 생성
            GameObject newButton = Instantiate(prefabToUse);

            // 위치 및 크기 설정
            newButton.transform.SetParent(existingButtonTransform.parent, false); // 부모 설정
            newButton.transform.position = existingButtonTransform.position;      // 위치 복사
            newButton.transform.localScale = existingButtonTransform.localScale;  // 크기 복사
            newButton.transform.rotation = existingButtonTransform.rotation;      // 회전 복사

            // 기존 버튼 숨기기
            buttons[i].gameObject.SetActive(false);

            // 새 버튼 UI 설정
            newButton.transform.Find("SkillIcon").GetComponent<Image>().sprite = skill.icon;
            newButton.transform.Find("SkillName").GetComponent<TMP_Text>().text = skill.skillName;
            newButton.transform.Find("SkillDescription").GetComponent<TMP_Text>().text = skill.description;

            // 새 버튼 클릭 이벤트 설정
            Button buttonComponent = newButton.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => SelectSkill(index));
            buttonComponent.onClick.AddListener(ResumeGame);

            // 생성된 버튼 리스트에 추가
            createdButtons.Add(newButton);

            i++;
        }
    }

    private GameObject GetButtonPrefabBySkillType(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Active:
                return activePrefab;
            case SkillType.Passive:
                return passivePrefab;
            case SkillType.Special:
                return specialPrefab;
            default:
                return activePrefab; // 기본값
        }
    }

    public void ClearCreatedButtons()
    {
        // 기존에 생성된 버튼 삭제
        foreach (GameObject button in createdButtons)
        {
            Destroy(button);
        }
        createdButtons.Clear();
    }

    public void SelectSkill(int skillNo)
    {
        SkillData selectedSkill = newSkillManager.instance.skills[skillNo];
        selectedSkill.nowSkill++;
        newSkillManager.instance.acquiredSkills.Add(selectedSkill);
        Debug.Log($"Selected skill: {selectedSkill.skillName}");
        newSkillManager.instance.flag = 1;
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game...");
        Time.timeScale = 1f;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void OnEnable()
    {
        SetButtons(GetRandomSkillNo());
        Time.timeScale = 0f;
    }

    public HashSet<int> GetRandomSkillNo()
    {
        HashSet<int> selectedSkillNo = new HashSet<int>();
        int count = 0;

        while (count < 3)
        {
            int randomIndex = Random.Range(0, newSkillManager.instance.skills.Count);

            if (!selectedSkillNo.Contains(randomIndex) &&
                newSkillManager.instance.skills[randomIndex].nowSkill < newSkillManager.instance.skills[randomIndex].maxSkill)
            {
                bool isSpecial = newSkillManager.instance.specialFlag == 1 &&
                                 (randomIndex == 2 || randomIndex == 5); // Special 중복 방지

                if (!isSpecial)
                {
                    selectedSkillNo.Add(randomIndex);
                    count++;
                }
            }
        }
        return selectedSkillNo;
    }
}
