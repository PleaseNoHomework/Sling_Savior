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
