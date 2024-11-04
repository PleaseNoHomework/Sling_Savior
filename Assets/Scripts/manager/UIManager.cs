using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Canvas defaultUI; //기본 UI, 보유 스킬 액티브 버튼 시간 스테이지 HP 등등등
    public Canvas powerUpUI; //파워업 UI 아이템 먹으면 표시해주기
    public Canvas waveUI;

    public int UIFlag = 0;
    public int powerUPFlag;

    public int getPowerUp;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;

        defaultUI.enabled = true;
        powerUpUI.enabled = false;
        waveUI.enabled = false;

        getPowerUp = 0;
        powerUPFlag = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void showUI(int flag)
    {
    }


}
