using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingManager : MonoBehaviour
{
    public static slingManager instance;
    public float shootCoolTime;
    public int damage;

    public Vector3 ballDirection;
    public int shootFlag = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
        }
        damage = 100;
        shootCoolTime = 1f;
    }

    public void passiveUp(int skillNo)
    {
        switch(skillNo)
        {
            case 1: //damage Up
                damage += 100;
                Debug.Log("damage Up");
                break;
            case 2:
                shootCoolTime *= 0.8f;
                Debug.Log("shootCoolTime down");
                break;
        }
    }

    public void specialUp(int skillNo)
    {
        switch(skillNo)
        {
            case 1: //multiple shot?
                break;
            case 2: //pierce shot
                break;
        }
    }


}
