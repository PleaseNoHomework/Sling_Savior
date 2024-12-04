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

    public GameObject uniquePanel;
    private newSkillManager skillManager;
    List<SkillData> acquiredSkills;
    // Start is called before the first frame update

    private void Awake()
    {
    }

    void Start()
    {
        skillManager = newSkillManager.instance;
        for (int i = 0; i < 6; i++)
        {
            passiveIcons[i].gameObject.SetActive(false);
        }

    }

    public void updatePanel()
    {
        Debug.Log("check Skilll");
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
                activePanel.SetActive(true);
                activeImage.sprite = skill.icon;
                activeName.text = skill.skillName;
                activeText.text = skill.description;
                
            }
        }
        
    }

    void updatePassive()
    {
        int x = 0;
        for (int i = 0; i < acquiredSkills.Count; i++)
        {
            if (acquiredSkills[i].skillType == SkillType.Passive)
            {
                passiveIcons[x].gameObject.SetActive(true);          // 아이콘 활성화
                passiveIcons[x].sprite = acquiredSkills[i].icon;     // 스킬 아이콘 설정
                
                x++;
            }
        }
        for (int i = x; i < passiveIcons.Length; i++)
        {
            passiveIcons[i].gameObject.SetActive(false);
        }
    }

    void updateUnique()
    {
        foreach (SkillData skill in acquiredSkills)
        {
            if (skill.skillType == SkillType.Special)
            {
                uniquePanel.SetActive(true);
                uniqueImage.sprite = skill.icon;
                uniqueName.text = skill.skillName;
                uniqueText.text = skill.description;
                
            }
        }
    }
}
