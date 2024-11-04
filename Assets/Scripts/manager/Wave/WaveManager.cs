using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;  // �̱��� �ν��Ͻ�
    public WaveSpawner wave;

    public Wave1Spawner wave1;           // Wave1Spawner ����
    public Wave2Spawner wave2;           // Wave2Spawner ����
    public Boss bossPrefab;              // Boss ������ ����
    public Transform bossSpawnPoint;     // Boss ���� ��ġ
    public static int finishFlag;
    public int currentWave = 1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        finishFlag = 1;
        StartWave1(); // ���� ���� �� ù ��° ���̺� ����
    }

    void StartWave1()
    {

        Debug.Log("Wave 1 ����");
        finishFlag = 1;
        wave.WaveClear += StartWave2;
        //wave1.OnWave1Completed += StartWave2; // Wave1 �Ϸ� �� StartWave2 ȣ��
        wave1.gameObject.SetActive(true);     // Wave1 Ȱ��ȭ
    }

    void StartWave2()
    {
        // Wave1 ���� ����
        //wave1.OnWave1Completed -= StartWave2; // �̺�Ʈ ���� ����
        //wave2.OnWave2Completed += StartBossWave; // Wave2 �Ϸ� �� ���� ���̺� ����

        wave1.gameObject.SetActive(false);    // Wave1 ��Ȱ��ȭ
        wave.WaveClear -= StartWave2;
        wave.WaveClear += StartBossWave;

        
        currentWave++;
        finishFlag = 1;
        Debug.Log("Wave 2 ����");
        
        
        wave2.gameObject.SetActive(true);        // Wave2 Ȱ��ȭ
    }

    void StartBossWave()
    {
        Debug.Log("Wave 2 Ŭ����");

        // Wave2 ���� ����
        wave2.OnWave2Completed -= StartBossWave; // �̺�Ʈ ���� ����
        wave2.gameObject.SetActive(false);       // Wave2 ��Ȱ��ȭ

        currentWave++;
        Debug.Log("Boss Stage ����");
        Boss bossInstance = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        bossInstance.OnBossDestroyed += OnGameCompleted; // ���� �ı� �� ���� �Ϸ� ó��
    }

    void OnGameCompleted()
    {
        Debug.Log("��� ���̺� �Ϸ�, ���� ����!");
    }
}