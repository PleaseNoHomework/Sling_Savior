using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseScript : MonoBehaviour
{
  private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HPManager.instance.baseHP--;
            Debug.Log("hp down! " + HPManager.instance.baseHP);
        }
    }
}
