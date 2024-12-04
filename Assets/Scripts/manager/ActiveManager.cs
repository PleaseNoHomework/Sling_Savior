using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveManager : MonoBehaviour
{
    public AudioSource stunAudio;
    // Start is called before the first frame update
    int coolFlag = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (newSkillManager.instance.activeFlag == 1 && coolFlag == 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                coolFlag = 1;
                switch (newSkillManager.instance.activeNo)
                {
                    case 7: //스턴
                        Debug.Log("Stun!!!!!!!!!!!");
                        StartCoroutine(Stun());
                        break;
                    case 8: //광역 대미지
                        Debug.Log("Attack!!!!!!!!");
                        StartCoroutine(Attack());
                        
                        break;
                    case 9: //??
                        break;
                }                
            }
        }

    }

    public IEnumerator Stun()
    {
        float time = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //모든 적들을 잠깐 멈추게 한다.

        List<float> enemySpeeds = new List<float>();
        if (stunAudio != null) //오디오
        {
            stunAudio.Play();
        }
        foreach (GameObject enemy in enemies)
        {
            // enemyScript 가져오기
            EnemyStatus script = enemy.GetComponent<EnemyStatus>();

            if (script != null) // script가 존재하는 경우만 접근
            {
                enemySpeeds.Add(script.speed); // speed 값을 저장
                script.speed = 0;
            }
        }



        while (time < 1f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        
        coolFlag = 0;
        int i = 0;
        foreach (GameObject enemy in enemies) {
            EnemyStatus script = enemy.GetComponent<EnemyStatus>();
            script.speed = enemySpeeds[i];
            i++;
        }


    }


    public IEnumerator Attack()
    {
        float time = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //모든 적들의 피를 깎는다

        if (stunAudio != null) //오디오
        {
            stunAudio.Play();
        }
        foreach (GameObject enemy in enemies)
        {
            // enemyScript 가져오기
            EnemyStatus script = enemy.GetComponent<EnemyStatus>();

            if (script != null) // script가 존재하는 경우만 접근
            {
                script.currentHP -= 800;
            }
        }



        while (time < 1f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        coolFlag = 0;
        int i = 0;
    }
}
