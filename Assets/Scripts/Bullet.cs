using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // 탄환이 충돌 시 파괴
    }
}