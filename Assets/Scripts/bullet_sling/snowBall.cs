using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowBall : MonoBehaviour
{
    ballScript ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ball.movePos();

        if (ball.directionFlag == 1)
        {
            transform.Translate(new Vector3(0, 0, -1) * ball.speed * Time.deltaTime);
        }
    }
}
