using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseUISkillScript : MonoBehaviour
{
    public GameObject activePanel;
    public Image activeImage;
    public TMP_Text activeName;
    public TMP_Text activeText;

    public Image[] passiveIcons;

    public Image uniqueImage;
    public TMP_Text uniqueName;
    public TMP_Text uniqueText;


    public GameObject passivePanel;
    public GameObject uniquePanel;
    private newSkillManager skillManager;
    List<SkillData> acquiredSkills;
    // Start is called before the first frame update
    void Start()
    {
        skillManager = newSkillManager.instance;

        activePanel.SetActive(false);
        uniquePanel.SetActive(false);
    }

    public void updatePanel()
    {
        acquiredSkills = skillManager.acquiredSkills;
        updateActive();
        updatePassive();
        updateUnique();
    }

    // Update is called once per frame
    void updateActive()
    {
        
        foreach(SkillData skill in acquiredSkills)
        {
            if (skill.skillType == SkillType.Active)
            {
                activeImage.sprite = skill.icon;
                activeName.text = skill.skillName;
                activeText.text = skill.description;
                activePanel.SetActive(true);
            }
        }
        
    }

    void updatePassive()
    {
        int x = 0;
        for (int i = 0; i < acquiredSkills.Count; i++) //현재 있는 것중
        {
            if (acquiredSkills[i].icon != null && acquiredSkills[i].skillType == SkillType.Passive)
            {
                passiveIcons[x].sprite = acquiredSkills[i].icon;     // 스킬 아이콘 설정
                passiveIcons[x].gameObject.SetActive(true);          // 아이콘 활성화
                x++;
            }
        }

    }

    void updateUnique()
    {
        foreach (SkillData skill in acquiredSkills)
        {
            if (skill.skillType == SkillType.Special)
            {
                uniqueImage.sprite = skill.icon;
                uniqueName.text = skill.skillName;
                uniqueText.text = skill.description;
                uniquePanel.SetActive(true);
            }
        }
    }
}
