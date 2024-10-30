using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationEnemy : MonoBehaviour
{
    public int hp = 100;
    public float speed;
    public float acceleration;
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
            speed += acceleration * Time.deltaTime;
            transform.Translate(Vector3.back * speed * Time.deltaTime);
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

        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("PierceBullet"))
        {
            TakeDamage(slingManager.instance.damage);
            Debug.Log("Damage! " + slingManager.instance.damage);
        }

    }

    IEnumerator Stun()
    {
        isStunned = true;
        yield return new WaitForSeconds(stun);
        isStunned = false;
    }
}
