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
        makeText(GameManager.instance.currentWave);
        time += Time.deltaTime;
        if (time > 3f)
        {
            waveText.text = "Next Wave";
        }
        if (time > 6f)
        {
            UIManager.instance.ClearUI();
            //gameObject.SetActive(false);
        }

    }

    void makeText(int currentWave, float time)
    {
        switch(currentWave)
        {
            case 1:
                break;
            case 2:

                break;
            case 3:
                waveText.text = "Final Stage";
                break;
        }

    }
    
}
