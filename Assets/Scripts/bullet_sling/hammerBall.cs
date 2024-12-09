using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerBall : MonoBehaviour
{
    float duration = 0.1f;
    float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //쏘고있는 오브젝트가 빙빙도는 함수
    void shootRotate()
    {

    }

    //넉백시키기

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStatus enemy = collision.gameObject.GetComponent<EnemyStatus>();
            Animator animator = collision.gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("WalkTrigger");
            }
            if (enemy != null)
            {
                if (enemy._state != EnemyStatus.State.Die)
                {
                    enemy._state = EnemyStatus.State.Move;
                }
                elapsedTime = 0;
                StartCoroutine(pushEnemy(collision.gameObject));
            }
        }

    }

    public IEnumerator pushEnemy(GameObject enemy)
    {
        Debug.Log("push!");

        Vector3 initialPosition = enemy.transform.position; // 초기 위치 저장
        Vector3 targetPosition = initialPosition + new Vector3(0, 0, 30f); // 목표 위치 계산
        Debug.Log(initialPosition);
        Debug.Log(targetPosition);
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // 진행 비율 계산
            enemy.transform.position = Vector3.Lerp(initialPosition, targetPosition, t); // 위치 보간
            elapsedTime += Time.deltaTime;
            Debug.Log(enemy.transform.position);
            yield return null;
        }
        Debug.Log("=---------------------");
        // 최종 위치 보정
        enemy.transform.position = targetPosition;
        Debug.Log(enemy.transform.position);
    }

}
