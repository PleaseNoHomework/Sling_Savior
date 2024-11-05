using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject skillCanvas;
    private void Update()
    {
        if (Time.time >= 2f)
        {
            //skillCanvas.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("PierceBullet"))
        {
            // skillcanvas.enabled = true;
            SkillManager.instance.flag = 1;
            Debug.Log("skills!");
            Destroy(gameObject);
        }

    }
}
