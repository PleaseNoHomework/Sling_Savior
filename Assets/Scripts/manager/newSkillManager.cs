using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newSkillManager : MonoBehaviour
{
    public static newSkillManager instance;

    [Header("Skill Data")]
    public List<SkillData> skills;
    
    public List<SkillData> acquiredSkills;      // »πµÊ«— Ω∫≈≥ ∏Ò∑œ

    public int passiveFlag = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (passiveFlag == 1)
        {
            slingManager.instance.setState();
            passiveFlag = 0;
        }
    }
}
