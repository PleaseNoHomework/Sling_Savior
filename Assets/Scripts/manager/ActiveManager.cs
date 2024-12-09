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

    public float duration = 0.5f; // 흔들림 지속 시간
    public float magnitude = 0.1f; // 흔들림 강도
    private Vector3 originalPosition; // 카메라의 원래 위치

    public GameObject mainCamera;

    public void activeActive()
    {
        if (newSkillManager.instance.activeFlag == 1 && !isCooldown)
        {
            switch (newSkillManager.instance.activeNo)
            {
                case 7: // 스턴
                    Debug.Log("Stun!!!!!!!!!!!");
                    StartCoroutine(ShakeCoroutine());
                    StartCoroutine(Stun());
                    break;
                case 8: // 광역 대미지
                    Debug.Log("Attack!!!!!!!!");
                    StartCoroutine(Attack());
                    break;
                case 9: // ?? 스킬
                    break;
            }
        }
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

    private IEnumerator ShakeCoroutine()
    {
        originalPosition = mainCamera.transform.localPosition;
        float elapsed = 0.0f;
        Debug.Log("camera moved");
        while (elapsed < duration)
        {
            // 임의의 흔들림 위치 계산
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null; // 한 프레임 대기
        }

        mainCamera.transform.localPosition = originalPosition; // 원래 위치로 복구
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }









}
