using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    public static EffectSpawner instance; // 싱글톤 패턴
    public Transform managerTransform; // Manager의 위치

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SpawnEffect(GameObject effectPrefab)
    {
        if (effectPrefab != null && managerTransform != null)
        {
            // Manager 위치에 프리팹 생성
            Instantiate(effectPrefab, managerTransform.position, Quaternion.identity);
        }
    }
}
