using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void Start()
    {
        if (transform.position.z >= 25f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 25f);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("PierceBullet"))
        {
            // skillcanvas.enabled = true;
            newSkillManager.instance.getSkillFlag = 1;
            Debug.Log("skills!");
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
