using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // 싱글톤 인스턴스

    public Wave1Spawner wave1;           // Wave1Spawner 참조
    public Wave2Spawner wave2;           // Wave2Spawner 참조
    public Boss bossPrefab;              // Boss 프리팹 참조
    public Transform bossSpawnPoint;     // Boss 스폰 위치

    public int bulletDamage = 100;       // 발사체 데미지 기본 값

    private int currentWave = 1;

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
        StartWave1(); // 게임 시작 시 첫 번째 웨이브 시작
    }

    void StartWave1()
    {
        Debug.Log("Wave 1 시작");
        wave1.OnWave1Completed += StartWave2; // Wave1 완료 시 StartWave2 호출
        wave1.gameObject.SetActive(true);     // Wave1 활성화
    }

    void StartWave2()
    {
        Debug.Log("Wave 1 클리어");

        // Wave1 종료 설정
        wave1.OnWave1Completed -= StartWave2; // 이벤트 구독 해제
        wave1.gameObject.SetActive(false);    // Wave1 비활성화

        Debug.Log("Wave 2 시작");
        wave2.OnWave2Completed += StartBossWave; // Wave2 완료 시 보스 웨이브 시작
        wave2.gameObject.SetActive(true);        // Wave2 활성화
    }

    void StartBossWave()
    {
        Debug.Log("Wave 2 클리어");

        // Wave2 종료 설정
        wave2.OnWave2Completed -= StartBossWave; // 이벤트 구독 해제
        wave2.gameObject.SetActive(false);       // Wave2 비활성화

        Debug.Log("Boss Stage 시작");
        Boss bossInstance = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        bossInstance.OnBossDestroyed += OnGameCompleted; // 보스 파괴 시 게임 완료 처리
    }

    void OnGameCompleted()
    {
        Debug.Log("모든 웨이브 완료, 게임 종료!");
    }
}
