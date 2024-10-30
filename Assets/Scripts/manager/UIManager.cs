using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Canvas defaultUI; //�⺻ UI, ���� ��ų ��Ƽ�� ��ư �ð� �������� HP ����
    public Canvas powerUpUI; //�Ŀ��� UI ������ ������ ǥ�����ֱ�
    public Canvas waveUI;

    public int UIFlag = 0;


    public int getPowerUp;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        getPowerUp = 0;
        defaultUI.gameObject.SetActive(true);
        powerUpUI.gameObject.SetActive(false);
        waveUI.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        showUI(UIFlag);
    }

    void showUI(int flag)
    {
        switch (flag)
        {
            case 1: //�Ŀ��� ������ ����
                
                break;
            case 2:
                showWaveUI();
                break;
            default:
                break;
        }
    }

    void showWaveUI() {
        waveUI.gameObject.SetActive(true);
    }
    public void ClearUI() {
        defaultUI.gameObject.SetActive(true);
        powerUpUI.gameObject.SetActive(false);
        waveUI.gameObject.SetActive(false);
        UIFlag = 0;
    }

}
