using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigzagEnemy : MonoBehaviour
{
    public int hp = 100;
    public float speed;
    public float zigzagFrequency; // 주파수
    public float zigzagAmplitude; // 진폭
    public float stun;
    private bool isStunned = false;
    public int flag = 0;

    // 적이 파괴될 때 GameManager에 알리기 위한 이벤트
    public delegate void DestroyEvent();
    public event DestroyEvent OnDestroyed;

    void Update()
    {
        if (!isStunned)
        {
            float zigzag = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude; // 사인곡선 형태로 좌우 이동, 진폭으로 이동폭 조절
            transform.Translate(new Vector3(zigzag, 0, -1) * speed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            OnDestroyed?.Invoke(); // 적이 파괴될 때 이벤트 호출
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            HPManager.instance.baseHP--;
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(slingManager.instance.damage);
            Debug.Log("Damage! " + slingManager.instance.damage);
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
