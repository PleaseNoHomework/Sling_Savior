using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public enum State {
        Spawn,
        Move,
        Attack,
        Die

    }

    public State _state;
    public int enemyNo;
    public int speed;
    public float maxHP;
    public float currentHP;
    public int itemFlag;
    public Vector3 moveDirection = new Vector3(0,0,-1);
    public GameObject item;

    public delegate void DestroyEvent();
    public event DestroyEvent OnDestroyed;
    public float time;
    public void takeDamage(int damage)
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
            case EnemyStatus.State.Attack:
                break;
            case EnemyStatus.State.Die: //
                if (itemFlag == 1)
                {
                    Instantiate(item, transform.position, Quaternion.identity);
                    Destroy(gameObject);
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
            takeDamage(slingManager.instance.damage);
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
