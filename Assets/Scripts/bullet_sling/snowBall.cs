using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowBall : MonoBehaviour
{
    public ballScript ball;
    public float scaleSpeed = 1f;
    Vector3 sizeUp = Vector3.zero;
    public Vector3 maxScale;
    public Vector3 initialScale;
    public float maxDamage;
    public float initialDamage;
    float damage;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        maxScale = transform.localScale * 2;
        initialDamage = ball.damage;
        maxDamage = initialDamage * 2;

        damage = initialDamage;
    }

    // Update is called once per frame
    void Update()
    {
        ball.movePos();

        //조준하고 있는 동안
        if (ball.mouseFlag == 1)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxScale, scaleSpeed * Time.deltaTime);
            if (ball.damage < maxDamage)
            {
                ball.damage += (maxDamage - initialDamage) * Time.deltaTime;
            }
        }

        else
        {
            if(ball.directionFlag != 1)
            {
                transform.localScale = initialScale;
                ball.damage = initialDamage;
            }
                
            
        }

    }
}
