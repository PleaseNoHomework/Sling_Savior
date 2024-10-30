using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class waveUIScript : MonoBehaviour
{
    private TMP_Text waveText;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        waveText = GetComponent<TMP_Text>();
        if (waveText != null)
        {
            Debug.Log("faf");
        }
        waveText.text = "Wave Clear!!";
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 3f)
        {
            waveText.text = "Next Wave";
        }
        else if (time > 6f)
        {
            UIManager.instance.ClearUI();
        }
    }
}
