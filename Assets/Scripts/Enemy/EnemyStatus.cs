using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public enum State {
        Spawn,
        Move,
        Attack,
        Knock,
        Die

    }

    public int isStuned = 0;
    public GameObject hitEffect;
    public State _state;
    public int enemyNo;
    public float speed;
    public float maxHP;
    public float currentHP;
    public int itemFlag;
    public Vector3 moveDirection = new Vector3(0,0,-1);
    public GameObject item;
    public AudioSource audios;
    public float time;
    float knockTime = 0f;
    public int freezeFlag = 0;
    public void takeDamage(float damage)
    {
        currentHP -= damage;
    }

    public void destroyEnemy()
    {
        if (currentHP <= 0) {
            _state = State.Die;      
        }      
    }

    public void setHP(float HP)
    {
        maxHP = HP;
        currentHP = maxHP;
    }
    public void knockBack()
    {
        knockTime += Time.deltaTime;
        if (transform.position.z <= 22f)
            transform.Translate(new Vector3(0, 0, 5) * speed * Time.deltaTime, Space.World);
    }


    private void Start()
    {
        _state = State.Spawn;
        currentHP = maxHP;
        time = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        destroyEnemy();
        switch (_state)
        {
            case State.Knock:
                knockBack();
                if(knockTime >= 0.5f)
                {
                    _state = State.Move;
                    knockTime = 0;                    
                }
                
                break;
            case State.Attack:
                break;
            case State.Die: //
                if (itemFlag == 1)
                {
                    Instantiate(item, transform.position, Quaternion.identity);
                    itemFlag = 0;
                }
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("PierceBullet"))
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            audios.Play();
            Debug.Log(",,,");
            ballScript sc = collision.gameObject.GetComponent<ballScript>();
            if (sc != null)
            {
                takeDamage(sc.damage);
            }

            if(slingManager.instance.freezeFlag == 1 && freezeFlag ==0) //동상이 걸린다면
            {
                speed *= 0.7f;
                freezeFlag = 1;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AttackPoint") && _state == State.Move)
        {
            _state = State.Attack;
        }
    }


}
