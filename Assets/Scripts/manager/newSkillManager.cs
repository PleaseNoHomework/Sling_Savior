using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newSkillManager : MonoBehaviour
{
    public GameObject skillSelectUI;
    public static newSkillManager instance;

    [Header("Skill Data")]
    public List<SkillData> skills;
    
    public List<SkillData> acquiredSkills;      // 획득한 스킬 목록
    public int flag = 0;
    public int specialFlag = 0;
    public int getSkillFlag = 0;
    public int activeFlag = 0;
    public int activeNo = 0;

    public GameObject activeButton;
    // Start is called before the first frame update

    private void Awake()
    { 
        if (instance == null) instance = this;
        acquiredSkills = new List<SkillData>();
        resetSkill();
    }

    // Update is called once per frame
    void Update()
    {

        if (flag == 1)
        {
            slingManager.instance.setState();
            flag = 0;
        }

        if (skillSelectUI != null)
        {
            if (getSkillFlag == 1 && acquiredSkills.Count < 8)
            {
                skillSelectUI.SetActive(true);
            }

            else if (getSkillFlag == 2)
            {
                //튜토리얼용
                TutorialManager.instance.itemCount = 1;
                if (activeFlag == 1)
                {
                    activeButton.SetActive(true);
                }
                skillSelectUI.SetActive(false);
                getSkillFlag = 0;
            }
        }


    }

    void resetSkill()
    {
        foreach (SkillData skill in skills)
        {
            skill.nowSkill = 0;
        }
    }

}
