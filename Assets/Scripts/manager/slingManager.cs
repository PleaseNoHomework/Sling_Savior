using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingManager : MonoBehaviour
{
    public static slingManager instance;
    public Vector3 ballDirection;
    public int shootFlag = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
