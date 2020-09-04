using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float stoppingDistance = 1f;

    public float tempEnemyMaxHealth = 100f;
    public float tempCurrentEnemyHealth;

    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tempCurrentEnemyHealth = tempEnemyMaxHealth;
    }

    public void TakeEnemyDamage(float damage)
    {
        tempCurrentEnemyHealth -= damage;
        
        if (tempCurrentEnemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
       if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

    }
}
