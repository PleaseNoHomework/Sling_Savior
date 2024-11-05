using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillSelectUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OpenSkillSelection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSkill(SkillData skill, GameObject button)
    {

    }


    public void OpenSkillSelection()
    {
        Time.timeScale = 0;
        List<SkillData> randomSkills = GetRandomSkills(3);


    }

    //선택되지 않은 스킬 목록을 가져온다
    private List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> selectedSkills = new List<SkillData>();
        HashSet<int> selectedIndices = new HashSet<int>(); // 선택된 인덱스를 기록하여 중복 방지

        while (selectedSkills.Count < count && selectedSkills.Count < SkillManager.instance.availableSkills.Count)
        {
            int randomIndex = Random.Range(0, SkillManager.instance.availableSkills.Count);

            // 중복된 인덱스가 아닌 경우에만 선택
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);              // 인덱스 기록
                selectedSkills.Add(SkillManager.instance.availableSkills[randomIndex]); // 스킬 추가
            }
        }

        return selectedSkills;
    }


    public void SelectSkill(SkillData selectedSkill)
    {
        // 스킬 획득 처리
        selectedSkill.isActive = true;

        if (selectedSkill.skillType == SkillType.Passive)
        {
            SkillManager.instance.acquiredSkills.Add(selectedSkill);  // 패시브는 중첩 가능
        }
        else
        {
            // 액티브와 스페셜은 중복 방지 및 중첩 방지
            SkillManager.instance.acquiredSkills.RemoveAll(s => s.skillType == selectedSkill.skillType);
            SkillManager.instance.acquiredSkills.Add(selectedSkill);
            SkillManager.instance.availableSkills.Remove(selectedSkill);  // 획득 목록에서 제거
        }

        // 스킬 선택 UI 닫기 및 게임 재개
        Time.timeScale = 1;
    }




}
