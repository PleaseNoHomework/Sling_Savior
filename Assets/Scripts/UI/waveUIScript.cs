using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class waveUIScript : MonoBehaviour
{
    public static waveUIScript instance;
    public TMP_Text waveText;
    public int flag = 0;
    private Image ima;
    private float time;
    // Start is called before the first frame update

    private void Start()
    {
        if (instance == null) instance = this;
        gameObject.SetActive(false);
        time = 0;
    }

    private void Update()
    {
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
