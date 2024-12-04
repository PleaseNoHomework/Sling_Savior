using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class skillUiScript : MonoBehaviour
{
    public static skillUiScript instance;
    public List<Button> buttons;
    public int flag = 0;
    public void setButton(HashSet<int> skillIndex)
    {
        int x = 0;
        foreach(int i in skillIndex)
        {
            buttons[x].onClick.RemoveAllListeners();
            if (TutorialManager.instance.isTutorial == 1) //튜토리얼일 때만 추가하기
                buttons[x].onClick.AddListener(() => selectSkill(i));
            buttons[x].onClick.AddListener(() => resumeGame());
            SkillData skill = newSkillManager.instance.skills[i];
            buttons[x].transform.Find("SkillIcon").GetComponent<Image>().sprite = skill.icon;
            buttons[x].transform.Find("SkillName").GetComponent<TMP_Text>().text = skill.skillName;
            buttons[x].transform.Find("SkillDescription").GetComponent<TMP_Text>().text = skill.description;
            x++;
        }

    }
  
    public HashSet<int> GetRandomSkillNo() {
        HashSet<int> selectSkillNo = new HashSet<int>();
        int i = 0;
        while(true)
        {
            int randomIndex = Random.Range(0, newSkillManager.instance.skills.Count);
            if(!selectSkillNo.Contains(randomIndex) && newSkillManager.instance.skills[randomIndex].nowSkill < newSkillManager.instance.skills[randomIndex].maxSkill)
            {
                bool isSpecial = newSkillManager.instance.specialFlag == 1 && (randomIndex == 5 || randomIndex == 2); //스페셜 이미 가지고 있는 경우
                bool isActive = newSkillManager.instance.activeFlag == 1 && (randomIndex == 7 || randomIndex == 8 || randomIndex == 9); //액티브 이미 가지고 있는 경우
                if (!isSpecial && !isActive)
                {
                    selectSkillNo.Add(randomIndex);
                    i++;
                }

            }
            if (i >= 3) break;
        }
        return selectSkillNo;
    }


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
