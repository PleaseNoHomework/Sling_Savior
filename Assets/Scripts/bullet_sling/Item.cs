using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("PierceBullet"))
        {
            // skillcanvas.enabled = true;
            newSkillManager.instance.getSkillFlag = 1;
            Debug.Log("skills!");
            Destroy(gameObject);
        }

    }
}
