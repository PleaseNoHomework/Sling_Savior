using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource startingBGM; // 스타팅 브금 오디오 소스
    public AudioSource inGameBGM;   // 인게임 브금 오디오 소스

    private void Start()
    {
        startingBGM.loop = true;
        inGameBGM.loop = true;

        startingBGM.Play();
        inGameBGM.Stop();
    }

    public void OnGameStartButtonClicked()
    {
        if (startingBGM.isPlaying)
        {
            startingBGM.Stop();
        }

        if (!inGameBGM.isPlaying)
        {
            inGameBGM.Play();
        }
    }
}
