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

    //���õ��� ���� ��ų ����� �����´�
    private List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> selectedSkills = new List<SkillData>();
        HashSet<int> selectedIndices = new HashSet<int>(); // ���õ� �ε����� ����Ͽ� �ߺ� ����

        while (selectedSkills.Count < count && selectedSkills.Count < SkillManager.instance.availableSkills.Count)
        {
            int randomIndex = Random.Range(0, SkillManager.instance.availableSkills.Count);

            // �ߺ��� �ε����� �ƴ� ��쿡�� ����
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);              // �ε��� ���
                selectedSkills.Add(SkillManager.instance.availableSkills[randomIndex]); // ��ų �߰�
            }
        }

        return selectedSkills;
    }


    public void SelectSkill(SkillData selectedSkill)
    {
        // ��ų ȹ�� ó��
        selectedSkill.isActive = true;

        if (selectedSkill.skillType == SkillType.Passive)
        {
            SkillManager.instance.acquiredSkills.Add(selectedSkill);  // �нú�� ��ø ����
        }
        else
        {
            // ��Ƽ��� ������� �ߺ� ���� �� ��ø ����
            SkillManager.instance.acquiredSkills.RemoveAll(s => s.skillType == selectedSkill.skillType);
            SkillManager.instance.acquiredSkills.Add(selectedSkill);
            SkillManager.instance.availableSkills.Remove(selectedSkill);  // ȹ�� ��Ͽ��� ����
        }

        // ��ų ���� UI �ݱ� �� ���� �簳
        Time.timeScale = 1;
    }




}
