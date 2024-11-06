using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingManager : MonoBehaviour
{
    public static slingManager instance;
    public float shootCoolTime;
    public int damage;
    public int pierceFlag = 0;

    public Vector3 ballDirection;
    public int shootFlag = 0;
    public int choiceFlag = 0;
    public SkillData skill;

    // Start is called before the first frame update

    public void setState()
    {
        damage = 100 * (1 + newSkillManager.instance.skills[0].nowSkill);
        shootCoolTime = 1 * Mathf.Pow(0.8f, newSkillManager.instance.skills[1].nowSkill);
        Debug.Log(damage + ", " + shootCoolTime);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        setState();
        //pierceFlag = 1;
    }

    private void Update()
    {
        if (choiceFlag == 1)
        {
            switch (skill.skillType)
            {
                case SkillType.Passive:
                    passiveUp(skill.skillNo);
                    break;
                case SkillType.Special:
                    specialUp(skill.skillNo);
                    break;
            }

            choiceFlag = 0;
        }
    }


    public void passiveUp(int skillNo)
    {
        switch(skillNo)
        {
            case 1: //damage Up
                damage = 100 + 100 * SkillManager.instance.acquiredSkills[skillNo].nowSkill;
                Debug.Log("damage Up");
                break;
            case 2:
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

    public void specialUp(int skillNo)
    {
        switch(skillNo)
        {
            case 5: //multiple shot?
                break;
            case 3: //pierce shot
                pierceFlag = 1;
                Debug.Log("Complete");
                break;
            default:
                break;
        }
    }


}
