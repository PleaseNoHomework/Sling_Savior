using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    public List<SkillData> allSkills;           // ��� ��ų ���
    public List<SkillData> availableSkills;     // ȹ�� ������ ��ų ���
    public List<SkillData> acquiredSkills;      // ȹ���� ��ų ���

    void Start()
    {
        foreach (SkillData skill in allSkills)
        {
            skill.isActive = false;             // ���� �� ��� ��ų ��Ȱ��ȭ
            availableSkills.Add(skill);         // ��� ��ų�� ȹ�� ���� ��Ͽ� �߰�
        }
    }

    public void AcquireSkill(SkillData newSkill)
    {
        // Passive ��ų�� �ߺ� ���� ���� ȹ��
        if (newSkill.skillType == SkillType.Passive)
        {
            if (!acquiredSkills.Contains(newSkill))
            {
                newSkill.Activate();
                acquiredSkills.Add(newSkill);
                availableSkills.Remove(newSkill);
            }
            return;
        }

        // Active �Ǵ� Special ��ų �� ���� Ÿ���� ���� ��� ��ü
        for (int i = acquiredSkills.Count - 1; i >= 0; i--)
        {
            SkillData acquiredSkill = acquiredSkills[i];

            if (acquiredSkill.skillType == newSkill.skillType)
            {
                acquiredSkill.isActive = false;
                acquiredSkills.Remove(acquiredSkill);
                availableSkills.Add(acquiredSkill);  // ���� ��ų�� ȹ�� ���� ������� �̵�
                Debug.Log($"{acquiredSkill.skillName} ��ų�� ��Ȱ��ȭ�ǰ� ȹ�� ���� ������� ���ư��ϴ�.");
            }
        }

        // ���ο� ��ų ȹ�� �� Ȱ��ȭ
        newSkill.Activate();
        acquiredSkills.Add(newSkill);
        availableSkills.Remove(newSkill);
        Debug.Log($"{newSkill.skillName} ��ų�� ȹ��ǰ� Ȱ��ȭ�Ǿ����ϴ�.");
    }

    public void ApplySkillEffects(GameObject target)
    {
        foreach (SkillData skill in acquiredSkills)
        {
            if (skill.isActive)
            {
            }
        }
    }
}

