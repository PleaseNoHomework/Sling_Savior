using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //쏘고있는 오브젝트가 빙빙도는 함수
    void shootRotate()
    {

    }

    //넉백시키기

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStatus enemy = collision.gameObject.GetComponent<EnemyStatus>();
            Animator animator = collision.gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("WalkTrigger");
            }
            if (enemy != null)
            {
                if (enemy._state != EnemyStatus.State.Die)
                {
                    enemy._state = EnemyStatus.State.Knock;
                }

            }

        }

    }


}
