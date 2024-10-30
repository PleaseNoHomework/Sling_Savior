using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Canvas defaultUI; //기본 UI, 보유 스킬 액티브 버튼 시간 스테이지 HP 등등등
    public Canvas powerUpUI; //파워업 UI 아이템 먹으면 표시해주기
    public Canvas gameStartUI; //게임 시작하면 중간에 게임 시작! 문구 써있는 UI
    public Canvas gameEndUI; //몬스터 다 처치하면(웨이브 클리어) 중간에 Wave Clear! 문구 써있는 UI

    public int getPowerUp;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        getPowerUp = 0;
        defaultUI.gameObject.SetActive(true);
        powerUpUI.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("show UI");
            defaultUI.gameObject.SetActive(false);
            powerUpUI.gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            defaultUI.gameObject.SetActive(true);
            powerUpUI.gameObject.SetActive(false);
        }
    }
}
