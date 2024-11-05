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
            Debug.Log(i);
            SkillData skill = newSkillManager.instance.skills[i];
            buttons[x].transform.Find("SkillIcon").GetComponent<Image>().sprite = skill.icon;
            buttons[x].transform.Find("SkillText").GetComponent<TMP_Text>().text = skill.skillName;
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
            if (i > 3) break;
        }
        return selectSkillNo;
    }



    // Start is called before the first frame update
    void Start()
    {
        setButton(GetRandomSkillNo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
