using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class waveUIScript : MonoBehaviour
{
<<<<<<< HEAD
    /*
=======
    public Canvas canvas;
>>>>>>> 4f678feecd0ca6bfa4e21b15cddfa7dbcc027032
    public TextMeshProUGUI waveText;
    private Image ima;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.finishFlag == 1)
        {
            time += Time.deltaTime;
            makeText(GameManager.instance.currentWave);
        }
        else canvas.enabled = false;
    }



    void makeText(int currentWave)
    {
        canvas.enabled = true;
        switch (currentWave)
        {
            
            case 1:
                waveText.text = "Wave1";
                if (time > 3f)
                {
                    GameManager.finishFlag = 0;
                    time = 0;
                }
                break;
            case 2:

                if (time < 2f) waveText.text = "Wave Clear!";
                else if (time > 2f && time <= 4f)
                {
                    waveText.text = "Wave2";
                }
                else if (time > 4f)
                {
                    GameManager.finishFlag = 0;
                    time = 0;
                }
                break;
            case 3:
                if (time < 2f) waveText.text = "Wave Clear!";
                else if (time < 4f && time >= 2f)
                {
                    waveText.text = "Boss Wave";
                }
                else if (time > 4f)
                {
                    GameManager.finishFlag = 0;
                    time = 0;
                }
                break;
                
        }
        
    }
    */
}
