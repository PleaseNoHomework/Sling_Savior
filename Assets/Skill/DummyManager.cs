using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    public List<SkillData> allSkills;           // 모든 스킬 목록
    public List<SkillData> availableSkills;     // 획득 가능한 스킬 목록
    public List<SkillData> acquiredSkills;      // 획득한 스킬 목록

    void Start()
    {
        foreach (SkillData skill in allSkills)
        {
            skill.isActive = false;             // 시작 시 모든 스킬 비활성화
            availableSkills.Add(skill);         // 모든 스킬을 획득 가능 목록에 추가
        }
    }

    public void AcquireSkill(SkillData newSkill)
    {
        // Passive 스킬은 중복 방지 없이 획득
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

        // Active 또는 Special 스킬 중 같은 타입이 있을 경우 교체
        for (int i = acquiredSkills.Count - 1; i >= 0; i--)
        {
            SkillData acquiredSkill = acquiredSkills[i];

            if (acquiredSkill.skillType == newSkill.skillType)
            {
                acquiredSkill.isActive = false;
                acquiredSkills.Remove(acquiredSkill);
                availableSkills.Add(acquiredSkill);  // 기존 스킬을 획득 가능 목록으로 이동
                Debug.Log($"{acquiredSkill.skillName} 스킬이 비활성화되고 획득 가능 목록으로 돌아갑니다.");
            }
        }

        // 새로운 스킬 획득 및 활성화
        newSkill.Activate();
        acquiredSkills.Add(newSkill);
        availableSkills.Remove(newSkill);
        Debug.Log($"{newSkill.skillName} 스킬이 획득되고 활성화되었습니다.");
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

