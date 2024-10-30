using System.Collections;
using UnityEngine;

public class ForwardEnemy : MonoBehaviour
{
    public int hp;
    public float speed;
    public float stun;
    private bool isStunned = false;

    void Update()
    {
        if (!isStunned)
        {
            Move();
        }
    }

    void Move()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) // "Bullet" 태그를 확인
        {
            Bullet bullet = other.GetComponent<Bullet>();
            TakeDamage(bullet.damage);

            if (other.CompareTag("a")) // "a" 태그로 스턴 탄환 확인
            {
                StartCoroutine(Stun());
            }
        }
    }

    IEnumerator Stun()
    {
        isStunned = true;
        yield return new WaitForSeconds(stun);
        isStunned = false;
    }
}
