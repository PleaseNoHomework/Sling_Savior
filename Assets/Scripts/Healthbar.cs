using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBarPrefab; // 체력바 프리팹
    public Vector3 offset = new Vector3(0, 2f, 0); // 체력바 위치 오프셋 (적의 머리 위)

    private EnemyStatus enemyStatus; // 적의 EnemyStatus 컴포넌트 참조
    private Transform target; // 적의 Transform
    private GameObject healthBarInstance; // 생성된 체력바 인스턴스
    private Image fillImage; // 체력을 표시하는 이미지

    private void Start()
    {
        // EnemyStatus 컴포넌트 및 Transform 설정
        enemyStatus = GetComponent<EnemyStatus>();
        target = transform;

        // 체력바 프리팹을 인스턴스화하고 초기화
        if (healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarPrefab);
            healthBarInstance.transform.SetParent(null); // 필요에 따라 부모 설정, 여기서는 월드 공간에 배치

            // fillImage 참조 설정
            fillImage = healthBarInstance.GetComponentInChildren<Image>(); // 프리팹에 있는 fillImage 참조
        }
    }

    private void Update()
    {
        if (enemyStatus != null && healthBarInstance != null)
        {
            // 체력바 위치를 적의 머리 위에 고정
            Vector3 healthBarPosition = Camera.main.WorldToScreenPoint(target.position + offset);
            healthBarInstance.transform.position = healthBarPosition;

            // 체력바 업데이트
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        // 체력 비율을 계산하고 fillImage의 fillAmount를 설정
        float healthPercentage = enemyStatus.currentHP / enemyStatus.maxHP;
        fillImage.fillAmount = Mathf.Clamp01(healthPercentage);
    }

    private void OnDestroy()
    {
        // 적이 파괴될 때 체력바도 제거
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance);
        }
    }
}
