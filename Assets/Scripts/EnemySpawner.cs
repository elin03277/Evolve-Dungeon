using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public Transform spawnPoint;
    public int currentSpawned = 0;
    public int spawnNumber = 5;
    public float spawnTimer = 0f;
    public float enemyHealth = 100f;
    public float enemyPotion = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        // SpawnEnemies();
    }

    void FixedUpdate()
    {
        spawnTimer -= Time.fixedDeltaTime;

        if (spawnTimer < 0 && currentSpawned < spawnNumber)
        {
            GameObject enemySpawn = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
            enemySpawn.GetComponent<Enemy>().enemyMaxHealth = enemyHealth;
            enemySpawn.GetComponent<Enemy>().potionChance = enemyPotion;

            currentSpawned++;
            spawnTimer = Random.Range(7, 11);
        }
    }

}
