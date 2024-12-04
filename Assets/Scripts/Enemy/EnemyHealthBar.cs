using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject Hpbar; // Canvas 오브젝트
    private Slider healthSlider; // 실제 체력 Slider
    public EnemyStatus enemyStatus; // Enemy 상태 정보

    void Start()
    {
        // Hpbar 아래에서 Slider 컴포넌트를 가져옴
        if (Hpbar != null)
        {
            healthSlider = Hpbar.GetComponentInChildren<Slider>();
        }

        if (enemyStatus != null && healthSlider != null)
        {
            // 초기 체력 설정
            healthSlider.maxValue = enemyStatus.maxHP;
            healthSlider.value = enemyStatus.currentHP;
        }
    }

    void Update()
    {
        if (enemyStatus != null && healthSlider != null)
        {
            // 체력바 업데이트
            healthSlider.value = enemyStatus.currentHP;

            // Enemy가 죽으면 체력바를 비활성화
            if (enemyStatus.currentHP <= 0)
            {
                Hpbar.SetActive(false);
            }
        }
    }
}
