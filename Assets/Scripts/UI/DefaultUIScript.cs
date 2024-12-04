using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUIScript : MonoBehaviour
{
    [Header("UI Components")]


    public TMP_Text hpText;
    public TMP_Text waveText;
    // Wave 정보
    public Image[] skillIcons;       // 스킬아이코ㄴ

    private HPManager hpManager;
    private GameManager gameManager;
    private WaveSpawner waveSpawner;
    private newSkillManager skillManager;

    void Start()
    {
        hpManager = HPManager.instance;
        gameManager = GameManager.instance;
        skillManager = newSkillManager.instance;
        waveSpawner = WaveSpawner.instance;

        // UI 초기화
        //UpdateHP();
        UpdateWave();
    }

    void Update()
    {
        //UpdateHP();
        UpdateWave();
    }

    /*
    private void UpdateHP()
    {
        if (hpManager != null)
        {
            hpText.text = $"{hpManager.baseHP}/5";
        }
    }*/

    private void UpdateWave()
    {
        // GameManager에서 currentWave 값을 받아와서 텍스트에 반영
        if (waveSpawner != null)
        {
            waveText.text = $"{waveSpawner.currentWave}";
        }
    }
}
