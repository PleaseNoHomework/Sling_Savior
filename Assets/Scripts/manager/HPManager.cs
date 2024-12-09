using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public TMP_Text hpText;
    public static HPManager instance;
    public int baseHP;
    public GameObject waveUI;

    public Image hitEffectImage; // 붉은 이미지 연결
    public float fadeDuration = 0.5f; // 페이드 아웃 지속 시간
    private int previousHP; // 이전 HP 값
    private bool isFading = false; // 페이드 중 여부

    public Transform cameraTransform; // 카메라 트랜스폼
    public float shakeDuration = 0.3f; // 흔들림 지속 시간
    public float shakeIntensity = 0.5f; // 흔들림 강도
    private Vector3 originalCameraPosition; // 카메라 원래 위치

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        previousHP = baseHP; // 초기값 설정
        if (cameraTransform != null)
        {
            originalCameraPosition = cameraTransform.localPosition;
        }
    }

    void Update()
    {
        hpText.text = $"{baseHP} / 10";

        // HP가 줄어들었는지 확인
        if (baseHP < previousHP)
        {
            // 피격 효과 출력
            getBloodEffect();
            if (cameraTransform != null)
            {
                StartCoroutine(CameraShakeCoroutine());
            }
        }

        // 이전 HP 업데이트
        previousHP = baseHP;

        // HP가 0 이하일 때 게임 오버 처리
        if (baseHP <= 0 && waveUIScript.instance.gameOverFlag == 0)
        {
            SceneController.instance.LoadGameOverScene();
        }
    }

    public void getBloodEffect()
    {
        if (!isFading)
        {
            StartCoroutine(BloodEffectCoroutine());
        }
    }

    private IEnumerator BloodEffectCoroutine()
    {
        isFading = true;

        // Alpha 값을 1로 설정하여 이미지를 완전히 보이게 만듦
        hitEffectImage.color = new Color(hitEffectImage.color.r, hitEffectImage.color.g, hitEffectImage.color.b, 1f);

        float elapsedTime = 0f;

        // Alpha 값을 서서히 0으로 줄여가며 페이드 아웃
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            hitEffectImage.color = new Color(hitEffectImage.color.r, hitEffectImage.color.g, hitEffectImage.color.b, alpha);
            yield return null;
        }

        // 완전히 투명하게 설정
        hitEffectImage.color = new Color(hitEffectImage.color.r, hitEffectImage.color.g, hitEffectImage.color.b, 0f);

        isFading = false;
    }

    private IEnumerator CameraShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;

            // 카메라를 랜덤하게 흔들림
            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;
            cameraTransform.localPosition = originalCameraPosition + randomOffset;

            yield return null;
        }

        // 카메라 위치 복원
        cameraTransform.localPosition = originalCameraPosition;
    }
}
