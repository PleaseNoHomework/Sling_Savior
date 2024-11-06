using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public static HPManager instance;
    public int baseHP;
    public GameObject waveUI;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        baseHP = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (baseHP <= 0 && waveUIScript.instance.gameOverFlag == 0)
        {
            waveUIScript.instance.gameOverFlag = 1;
            waveUIScript.instance.setText("Game Over!");
            waveUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
