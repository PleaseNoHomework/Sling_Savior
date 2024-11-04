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

    public void takeDamage(int damage)
    {
        currentHP -= damage;
    }

    public void destroyEnemy()
    {
        //animationP pay or Particle
        //finsihed, Destroy
        if (currentHP <= 0) {
            _state = State.Die;
            if (itemFlag == 1) {
                Instantiate(item, transform.position, Quaternion.identity);
            }

            
        }
        
    }

    public void setHP(float HP)
    {
        maxHP = HP;
        currentHP = maxHP;
    }


    private void Start()
    {
        _state = State.Move;
    }

    private void Update()
    {
        destroyEnemy();
        switch (_state)
        {
            case EnemyStatus.State.Attack:
                break;
            case EnemyStatus.State.Die:
                OnDestroyed?.Invoke();
                WaveSpawner.instance.activeEnemies++;
                Destroy(gameObject);
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
