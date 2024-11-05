using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class skillUiScript : MonoBehaviour
{

    public List<Button> buttons;

    public void setButton(HashSet<int> skillIndex)
    {
        int x = 0;
        foreach(int i in skillIndex)
        {
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
                selectSkillNo.Add(randomIndex);
                i++;
            }
            if (i >= 3) break;
        }
        return selectSkillNo;
    }


    public void selectSkill(int skillNo)
    {
        Debug.Log("select");
        newSkillManager.instance.skills[skillNo].nowSkill++;
        int index = newSkillManager.instance.acquiredSkills.FindIndex(skill => skill.skillNo == skillNo); //선택한 스킬 번호가 있는 인덱스, 없으면 -1 반환
        if (index == -1)
        {
            Debug.Log("add skill");
            newSkillManager.instance.acquiredSkills.Add(newSkillManager.instance.skills[skillNo]);
        }
        else
        {
            newSkillManager.instance.acquiredSkills[index].nowSkill++;
        }
        newSkillManager.instance.flag = 1;
    }

    public void resumeGame()
    {
        Debug.Log("resume Game");
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }



    // Start is called before the first frame update
    void Start()
    {
        setButton(GetRandomSkillNo());
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
