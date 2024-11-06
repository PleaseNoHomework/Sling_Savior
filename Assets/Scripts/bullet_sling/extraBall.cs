using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraBall : MonoBehaviour
{
    public static extraBall instance;
    public GameObject mainBall;
    public int flag;
    public float speed;
    Vector3 direction;
    private AudioSource hitAudio;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        hitAudio = gameObject.GetComponent<AudioSource>();
    }

    public void setDirection(Vector3 direc)
    {
        direction = direc;
    }
    // Update is called once per frame
    void Update()
    {

        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Item"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("wall");
            Destroy(gameObject);
        }
    }
}
