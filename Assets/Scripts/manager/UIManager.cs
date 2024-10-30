using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Canvas defaultUI; //�⺻ UI, ���� ��ų ��Ƽ�� ��ư �ð� �������� HP ����
    public Canvas powerUpUI; //�Ŀ��� UI ������ ������ ǥ�����ֱ�
    public Canvas gameStartUI; //���� �����ϸ� �߰��� ���� ����! ���� ���ִ� UI
    public Canvas gameEndUI; //���� �� óġ�ϸ�(���̺� Ŭ����) �߰��� Wave Clear! ���� ���ִ� UI

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
