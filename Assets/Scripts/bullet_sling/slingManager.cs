using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingManager : MonoBehaviour
{
    public static slingManager instance;
    public TutorialManager tutorialManager;
    public float shootCoolTime;
    public int damage;

    public int freezeFlag = 0;
    public int pierceFlag = 0;
    public int multiFlag = 0;


    public Vector3 ballDirection;
    
    public int choiceFlag = 0;
    public SkillData skill;

    // Start is called before the first frame update

    public void setState()
    {
        damage = 100 + 75 * newSkillManager.instance.skills[0].nowSkill + 50 * newSkillManager.instance.skills[4].nowSkill;
        shootCoolTime = 1 * Mathf.Pow(0.7f, newSkillManager.instance.skills[1].nowSkill);

        freezeFlag = newSkillManager.instance.skills[4].nowSkill;
        Debug.Log(damage + ", " + shootCoolTime);

        specialUp();

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        };
    }
    void Start()
    {
        setState();
    }


    public void passiveUp(int skillNo)
    {
        switch(skillNo)
        {
            case 1: //damage Up
                damage = 100 + 100 * SkillManager.instance.acquiredSkills[skillNo].nowSkill;
                Debug.Log("damage Up");
                break;
            case 2: //rateUP
                shootCoolTime = 1;
                for (int i = 0; i < SkillManager.instance.acquiredSkills[skillNo].nowSkill; i++)
                {
                    shootCoolTime *= 0.8f;
                }                   
                Debug.Log("shootCoolTime down");
                break;
            default:
                break;
        }
    }

    public void specialUp()
    {
        if (newSkillManager.instance.skills[5].nowSkill == 1) { multiFlag = 1; newSkillManager.instance.specialFlag = 1; }
        if (newSkillManager.instance.skills[2].nowSkill == 1) { pierceFlag = 1; newSkillManager.instance.specialFlag = 1; }
    }


}
