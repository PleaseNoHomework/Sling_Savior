using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class waveUIScript : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    private Image ima;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        waveText.text = "Wave Clear!!";
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.finishFlag == 1)
        {
            gameObject.SetActive(true);
            makeText(GameManager.instance.currentWave);
            time += Time.deltaTime;
        }

    }

    void makeText(int currentWave)
    {
        switch(currentWave)
        {
            case 1:
                waveText.text = "Wave1";
                if (time > 3f)
                {
                    gameObject.SetActive(false);
                    GameManager.instance.finishFlag = 0;
                    time = 0;
                }
                break;
            case 2:
                if (time > 0f) waveText.text = "Wave Clear!";
                else if (time > 3f)
                {
                    waveText.text = "Wave2";
                }
                else if (time > 6f)
                {
                    gameObject.SetActive(false);
                    GameManager.instance.finishFlag = 0;
                    time = 0;
                }
                break;
            case 3:
                if (time > 0f) waveText.text = "Wave Clear!";
                else if (time > 3f)
                {
                    waveText.text = "Boss Wave";
                }
                else if (time > 6f)
                {
                    gameObject.SetActive(false);
                    GameManager.instance.finishFlag = 0;
                    time = 0;
                }
                break;
                
        }
        
    }
    
}
