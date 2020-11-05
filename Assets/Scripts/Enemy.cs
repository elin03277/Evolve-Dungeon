using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject attackPotion;
    public GameObject speedPotion;

    public float speed = 2.5f;
    public float stoppingDistance = 1.1f;
    public float pursueDistance = 10f;
    public float displacement;

    public float enemyMaxHealth = 100f;
    public float currentEnemyHealth;

    public float potionChance = 0.4f;
    public float potionTypeChance = 0.5f;

    public Transform enemyPoint;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentEnemyHealth = enemyMaxHealth;
    }

    public void TakeEnemyDamage(float damage)
    {
        currentEnemyHealth -= damage;
        
        if (currentEnemyHealth <= 0)
        {
            if (Random.value < potionChance)
            {
                if (Random.value < potionTypeChance)
                {
                    Instantiate(attackPotion, enemyPoint.position, Quaternion.identity);
                } else
                {
                    Instantiate(speedPotion, enemyPoint.position, Quaternion.identity);
                }
            }
            
            FindObjectOfType<GameManager>().IncrementKillCounter();
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        displacement = Vector2.Distance(transform.position, target.position);

       if (displacement > stoppingDistance && displacement < pursueDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

    }

}
