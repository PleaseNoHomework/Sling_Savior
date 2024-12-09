using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerBall : MonoBehaviour
{
    public ballScript ballscript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ballscript.directionFlag ==1)
        {
            transform.Rotate(new Vector3(0,0,1) * 360f * Time.deltaTime * 3);
        }
    }


    //넉백시키기

    private void OnCollisionEnter(Collision collision)
    {


    }


}
