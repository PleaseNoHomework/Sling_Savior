using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour
{

    public static SceneManage instance;
    public int gameClearFlag;
    public int gameOverFlag;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        gameOverFlag = 0;
        gameClearFlag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moveGameOverScene();
    }


    void moveGameOverScene()
    {
        if (gameOverFlag == 1) SceneManager.LoadScene("GameOverScene");
    }
}
