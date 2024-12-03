using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class waveUIScript : MonoBehaviour
{
    public static waveUIScript instance;
    public TMP_Text waveText;
    public int flag = 0;
    public int gameStartFlag;
    public int gameOverFlag = 0;
    private float time;
    // Start is called before the first frame update

    public void setText(string text)
    {
        waveText.text = text;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        time = 0;
    }
    private void Update()
    {
        if (gameStartFlag == 1)
        {
            waveText.text = "Enemies are comming!";

            time += Time.deltaTime;
            if(time >= 2f)
            {
                time = 0;
                WaveManager.instance.spawnFlag = 1;
                gameStartFlag = 0;
                gameObject.SetActive(false);
            }
        }

        if (flag == 1)
        {
            time += Time.deltaTime;
            if (time <= 2f)
                waveText.text = "Wave Clear!";
            else if (time <= 4f)
                waveText.text = "Next Wave";
            else
            {
                time = 0;
                WaveManager.instance.spawnFlag = 1;
                flag = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
