using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGolem : MonoBehaviour
{
    //벽에 가까이 붙으면 공격
    public EnemyStatus enemy;

    public float changeInterval;                          // 방향 변경 간격
    private float nextChangeTime = 0;                     // 다음 방향 변경 시간
    private float[] offsets = {-1f, -0.5f, 0f, 0.5f, 1f}; // 좌우 이동 방향

    private float minX = -15f;
    private float maxX = 15f;

    void IceGolemMove()
    {
        float time = Time.time;
        // 벽에 도달했을 때마다 즉시 방향을 반전하도록 설정
        if (transform.position.x <= minX && enemy.moveDirection.x < 0)
        {
            Debug.Log("Reached Left Boundary");
            enemy.moveDirection.x = Mathf.Abs(enemy.moveDirection.x); // 오른쪽으로 이동
            transform.position = new Vector3(minX, transform.position.y, transform.position.z); // 위치 보정
        }
        else if (transform.position.x >= maxX && enemy.moveDirection.x > 0)
        {
            Debug.Log("Reached Right Boundary");
            enemy.moveDirection.x = -Mathf.Abs(enemy.moveDirection.x); // 왼쪽으로 이동
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z); // 위치 보정
        }
        // 일정 시간마다 이동 방향을 무작위로 변경 (벽에 도달하지 않은 경우)
        else if (time >= nextChangeTime)
        {
            nextChangeTime = time + changeInterval;
            enemy.moveDirection.x = offsets[Random.Range(0, offsets.Length)];
        }

        transform.Translate(new Vector3(enemy.moveDirection.x, 0, 1) * enemy.speed * Time.deltaTime);

        // x 좌표가 범위를 벗어나지 않도록 제한
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy._state == EnemyStatus.State.Move)
        {
            IceGolemMove();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            enemy._state = EnemyStatus.State.Attack;
        }
    }
}