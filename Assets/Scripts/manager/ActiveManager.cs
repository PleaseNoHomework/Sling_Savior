using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActiveManager : MonoBehaviour
{
    public AudioSource stunAudio;
    public TMP_Text activeButtonText; // 버튼 위에 표시될 텍스트
    private bool isCooldown = false; // 쿨타임 상태 확인

    public void activeActive()
    {
        if (newSkillManager.instance.activeFlag == 1 && !isCooldown)
        {
            switch (newSkillManager.instance.activeNo)
            {
                case 7: // 스턴
                    Debug.Log("Stun!!!!!!!!!!!");
                    StartCoroutine(Stun());
                    break;
                case 8: // 광역 대미지
                    Debug.Log("Attack!!!!!!!!");
                    StartCoroutine(Attack());
                    break;
                case 9: // ?? 스킬
                    break;
                case 10:
                    Debug.Log("typooon!!!!!");
                    StartCoroutine(Gust());
                    break;
            }
        }
    }
    private IEnumerator Gust()
    {
        Debug.Log("Gust skill activated!");

        float pushDistance = 30f; // 총 밀쳐내는 거리
        float pushDuration = 0.1f; // 바람 지속 시간
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                // EnemyStatus 가져오기
                EnemyStatus status = enemy.GetComponent<EnemyStatus>();
                if (status != null)
                {
                    // 상태를 강제로 Move로 변경
                    if (status._state != EnemyStatus.State.Die)
                    {
                        status._state = EnemyStatus.State.Move;

                        // 애니메이션 트리거를 Walk로 변경 (애니메이터가 있을 경우)
                        Animator animator = enemy.GetComponent<Animator>();
                        if (animator != null)
                        {
                            animator.SetTrigger("WalkTrigger");
                        }
                    }
                }

                // 밀쳐내기 구현
                StartCoroutine(PushEnemy(enemy, pushDistance, pushDuration));
            }
        }

        // 쿨다운 처리
        float cooldownTime = 5f; // 스킬 쿨타임
        isCooldown = true;
        UpdateCooldownText(cooldownTime);
        yield return StartCoroutine(CooldownTimer(cooldownTime));
    }


    private IEnumerator PushEnemy(GameObject enemy, float totalDistance, float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = enemy.transform.position; // 초기 위치 저장
        Vector3 targetPosition = initialPosition + new Vector3(0, 0, totalDistance); // 목표 위치 계산

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // 진행 비율 계산
            enemy.transform.position = Vector3.Lerp(initialPosition, targetPosition, t); // 위치 보간
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 위치 보정
        enemy.transform.position = targetPosition;
    }


    private IEnumerator Stun()
    {
        float cooldownTime = 5f; // 스킬 기본 쿨타임
        float stunDuration = 1f; // 스턴 지속 시간

        isCooldown = true;
        UpdateCooldownText(cooldownTime);

        Dictionary<GameObject, float> enemySpeeds = new Dictionary<GameObject, float>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (stunAudio != null) // 오디오
        {
            stunAudio.Play();
        }

        foreach (GameObject enemy in enemies)
        {
            EnemyStatus script = enemy.GetComponent<EnemyStatus>();
            if (script != null && !enemySpeeds.ContainsKey(enemy))
            {
                enemySpeeds[enemy] = script.speed; // 적의 현재 속도를 저장
                script.speed = 0; // 적의 속도를 0으로 설정
            }
        }

        // 스턴 지속 시간 동안 대기 (독립적으로 처리)
        StartCoroutine(HandleStunEnd(stunDuration, enemySpeeds));

        // 쿨타임 실행
        yield return StartCoroutine(CooldownTimer(cooldownTime));
    }

    private IEnumerator Attack()
    {
        float cooldownTime = 3f; // 스킬 기본 쿨타임
        isCooldown = true;
        UpdateCooldownText(cooldownTime);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (stunAudio != null) // 오디오
        {
            stunAudio.Play();
        }

        foreach (GameObject enemy in enemies)
        {
            EnemyStatus script = enemy.GetComponent<EnemyStatus>();
            if (script != null)
            {
                script.currentHP -= 100; // 적 체력 감소
            }
        }

        yield return StartCoroutine(CooldownTimer(cooldownTime));
    }

    private IEnumerator CooldownTimer(float cooldownTime)
    {
        float timeLeft = cooldownTime;

        // 즉시 남은 시간을 반영
        UpdateCooldownText(timeLeft);

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime; // 남은 시간 즉시 감소
            UpdateCooldownText(timeLeft);
            yield return null; // 다음 프레임으로 넘어감
        }

        // 쿨타임 종료
        isCooldown = false;
        UpdateCooldownText(0);
    }



    private void UpdateCooldownText(float timeLeft)
    {
        if (timeLeft <= 0)
        {
            activeButtonText.text = "Ready";
        }
        else
        {
            activeButtonText.text = $"Cooldown: {timeLeft:0.0}s";
        }
    }

    private IEnumerator HandleStunEnd(float stunDuration, Dictionary<GameObject, float> enemySpeeds)
    {
        yield return new WaitForSeconds(stunDuration);

        // 스턴 해제
        foreach (var entry in enemySpeeds)
        {
            if (entry.Key != null) // 적 오브젝트가 존재할 경우
            {
                EnemyStatus script = entry.Key.GetComponent<EnemyStatus>();
                if (script != null)
                {
                    script.speed = entry.Value; // 원래 속도로 복원
                }
            }
        }
    }
}
