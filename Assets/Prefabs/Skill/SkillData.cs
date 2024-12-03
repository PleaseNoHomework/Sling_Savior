using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum SkillType
{
    Active,
    Passive,
    Special
}
[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Skill")]
public class SkillData : ScriptableObject
{
    public int skillNo;
    public int maxSkill;
    public int nowSkill;
    public string skillName;
    public string description;
    public Sprite icon;
    public SkillType skillType;
    public bool isActive = false;      // ��ų Ȱ��ȭ ����

    public void Activate()
    {
        isActive = true;
        Debug.Log($"{skillName} ��ų Ȱ��ȭ!");
    }
}
