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
    public int maxHP;
    public int currentHP;
    public Vector3 moveDirection = new Vector3(0,0,-1);

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
            Destroy(gameObject);
            
        }
        
    }

    private void Start()
    {
        currentHP = maxHP;
        _state = State.Move;
    }

    private void Update()
    {
        destroyEnemy();
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
