using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingScript : MonoBehaviour
{
    public static slingScript instance;
    public float speed;
    public GameObject pierceBall;
    public Vector3 ballSpawnPoint;
    public float maxZ, minZ, maxX, minX;
    private float shootTime; //쏘고 난 뒤 쿨타임

    public int canShoot = 0; //게임이 시작되면 1로 바꿔준다. 그 전까지는 쏠 수 없다.
    public int shootFlag = 0; //쐈다는걸 알려주기

    public slingManager slingmanager;

    public GameObject knockBackEnemy;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        shootTime = 0;
        slingmanager = slingManager.instance;
        Instantiate(slingmanager.defaultBall, ballSpawnPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(shootFlag == 1 && canShoot == 1)
        {

            shootTime += Time.deltaTime;
            if (shootTime > slingmanager.shootCoolTime)
            {
                shootTime = 0;
                shootFlag = 0;

                //멀티 샷
                if(newSkillManager.instance.skills[5].nowSkill == 1)
                {
                    Instantiate(slingmanager.defaultBall, ballSpawnPoint + new Vector3(-3,0,0), Quaternion.identity);
                    Instantiate(slingmanager.defaultBall, ballSpawnPoint + new Vector3(3, 0, 0), Quaternion.identity);
                }

                else
                    Instantiate(slingmanager.defaultBall, ballSpawnPoint, Quaternion.identity);
            }
               
        }

    }
}
