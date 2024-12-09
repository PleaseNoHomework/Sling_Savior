using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class skillUiScript : MonoBehaviour
{
    public static skillUiScript instance;
    public List<Button> buttons; // 기존 버튼 리스트
    public GameObject activePrefab; // Active 타입 버튼 Prefab
    public GameObject passivePrefab; // Passive 타입 버튼 Prefab
    public GameObject specialPrefab; // Special 타입 버튼 Prefab

    private List<GameObject> createdButtons = new List<GameObject>(); // 생성된 버튼 관리 리스트

    public void ClearCreatedButtons()
    {
        // 기존에 생성된 버튼 삭제
        foreach (GameObject button in createdButtons)
        {
            Destroy(button);
        }
        createdButtons.Clear();
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

    public void setButton(HashSet<int> skillIndex)
    {
        ClearCreatedButtons();
        int i = 0;
        foreach (int index in skillIndex)
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
            if(TutorialManager.instance.isTutorial == 1)
                buttonComponent.onClick.AddListener(() => selectSkill(index));
            buttonComponent.onClick.AddListener(() => resumeGame());

            // 생성된 버튼 리스트에 추가
            createdButtons.Add(newButton);

            i++;
        }

    }
  
    //해결
    public HashSet<int> GetRandomSkillNo() {
        HashSet<int> selectSkillNo = new HashSet<int>();
        int count = 0;
        while (true)
        {
            int randomIndex = Random.Range(0, newSkillManager.instance.skills.Count);
            if(!selectSkillNo.Contains(randomIndex) && 
                newSkillManager.instance.skills[randomIndex].nowSkill < newSkillManager.instance.skills[randomIndex].maxSkill)
            {
                bool isSpecial = newSkillManager.instance.specialFlag == 1 && (randomIndex == 5 || randomIndex == 2 || randomIndex == 9); //스페셜 이미 가지고 있는 경우
                bool isActive = newSkillManager.instance.activeFlag == 1 && (randomIndex == 7 || randomIndex == 8 ); //액티브 이미 가지고 있는 경우
                if (!isSpecial && !isActive)
                {
                    Debug.Log(randomIndex);
                    selectSkillNo.Add(randomIndex);
                    count++;
                }

            }
            if (count >= 3) break;
        }
        return selectSkillNo;
    }


    //고른 스킬을 추가해줌 해결
    public void selectSkill(int skillNo)
    {
        if (newSkillManager.instance.skills[skillNo].skillType == SkillType.Active) //액티브를 추가하였을 경우
        {
            newSkillManager.instance.activeFlag = 1;
            newSkillManager.instance.activeNo = skillNo; //사용할 액티브 스킬 넘버 => activeManager
            Debug.Log("Add Active");
            //액티브 버튼 추가하는 코드
        }

        else if (newSkillManager.instance.skills[skillNo].skillType == SkillType.Special) //스페셜 추가하였을 경우
        {
            newSkillManager.instance.specialFlag = 1;
        }

        Debug.Log("select : " + skillNo);
        newSkillManager.instance.skills[skillNo].nowSkill++;
        newSkillManager.instance.acquiredSkills.Add(newSkillManager.instance.skills[skillNo]);
        newSkillManager.instance.flag = 1;
    }

    public void resumeGame()
    {
        newSkillManager.instance.getSkillFlag = 2;
        Debug.Log("resume Game");
        Time.timeScale = 1;
        
    }

    private void Awake()
    {
        if(instance == null ) instance = this;
    }

    private void OnEnable()
    {
            setButton(GetRandomSkillNo());          
            Time.timeScale = 0;
        
    }
}
