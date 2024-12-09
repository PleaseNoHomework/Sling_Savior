using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HPManager : MonoBehaviour
{
    public TMP_Text hpText;
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
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = $"{baseHP} / 10";
        if (baseHP <= 0 && waveUIScript.instance.gameOverFlag == 0)
        {
            /*
            waveUIScript.instance.gameOverFlag = 1;
            waveUIScript.instance.setText("Game Over!");
            waveUI.SetActive(true);
            Time.timeScale = 0;*/

            SceneController.instance.LoadGameOverScene();
        }



    }

    public void getBloodEffect()
    {

    }


}
