using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    public static TutorialManager instance;

    public int ballCount = 0; //첫번째 튜토리얼 3번 쏘면 다음(허수아비소환)으로 넘어감
    public int targetCount = 0; //두번째 튜토리얼 3명 죽이면 다음(아이템 소환, 아이템 획득)으로 넘어감
    public int itemCount = 0; //세번째 튜토리얼 아이템 먹으면 본게임 시작
    public int isTutorial = 0;

    public GameObject tutorialEnemy; //허수아비
    public GameObject tutorialItem; //아이템
    public GameObject dragUI; // 위에 나올 UI
    
    TMP_Text texts;
    private float time = 0;
    private IEnumerator dragTutorial()
    {
        isTutorial = 1;
        Debug.Log("dragUI active");
        
        dragUI.SetActive(true);
        //드래그를 3번 하세요 ~

        while (ballCount < 3)
        {
            texts.text = "Drag! " + (3 - ballCount);
            yield return null;
        }

        texts.text = "Well Done!";
        time = 0;
        while (time < 3f)
        {
            time += Time.deltaTime;
            yield return null;
        }


    }

    private IEnumerator shootTarget() //표적을 3번 맞추세요~
    {
        Instantiate(tutorialEnemy, new Vector3(-10, 0, 20), Quaternion.identity);
        Instantiate(tutorialEnemy, new Vector3(0, 0, 20), Quaternion.identity);
        Instantiate(tutorialEnemy, new Vector3(10, 0, 20), Quaternion.identity);
        
        //if ~~~ 다 죽으면~~
        //flag = 1;

        while (targetCount < 3)
        {
            texts.text = "Kill! " + (3 - targetCount);
            yield return null;
        }

        texts.text = "Very Good!";
        time = 0;
        while (time < 3f)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator getItem()
    {
        texts.text = "Shoot Item!";
        Instantiate(tutorialItem, new Vector3(-10, 0, 10), Quaternion.identity);
        while(itemCount != 1)
        {
            yield return null;
        }

        texts.text = "Game Start!";
    }


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        dragUI.SetActive(false);
        texts = dragUI.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator tutorial()
    {
        yield return StartCoroutine(dragTutorial());
        yield return StartCoroutine(shootTarget());
        yield return StartCoroutine(getItem());

        time = 0;
        while (time < 2f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        dragUI.SetActive(false);


        //여기에 본게임 시작 함수 넣기

    }

}
