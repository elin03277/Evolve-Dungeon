
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D[] maps;
    public ColourToPrefab[] colourMappings;
    public GameObject player;
    public ParameterManager parameterManager;
    public GeneticAlgorithm geneticAlgorithm;

    public float enemySetPotionChance = 0.4f;
    public float enemySetHealth = 100f;
    public float enemySpawnerChance = 0.4f;
    public float heartChance = 0.4f;
    public float obstacleChance = 0.9f;
    public int roomChoice;

    List<GameObject> allChildrenObjects = new List<GameObject>();
    GameObject[] enemies;
    GameObject[] attackPotions;
    GameObject[] speedPotions;

    // Start is called before the first frame update
    void Start()
    {
        // enemySpawnerChance = 0.4f;
        /*Vector2 position = new Vector2(0, 0);
        Instantiate(colourMappings[0].prefab, position, Quaternion.identity, transform);*/
        GenerateLevel();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("y"))
        {
            RestartLevel();
        }

    }

    public void RestartLevel()
    {
        DestroyLevel();
        GenerateLevel();
    }

    void DestroyLevel()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        attackPotions = GameObject.FindGameObjectsWithTag("AttackPotion");
        speedPotions = GameObject.FindGameObjectsWithTag("SpeedPotion");

        foreach (GameObject child in allChildrenObjects)
        {
            Destroy(child);
        }

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        foreach (GameObject attackPotion in attackPotions)
        {
            Destroy(attackPotion);
        }

        foreach (GameObject speedPotion in speedPotions)
        {
            Destroy(speedPotion);
        }

        allChildrenObjects.Clear();
    }

    void GenerateLevel()
    {
        //Debug.Log(GameObject.Find("Player(clone)"));
        if (GameObject.Find("Player(Clone)") == null)
        {
            GameObject p = Instantiate(player, new Vector2(0, 0), Quaternion.identity);
        }

        parameterManager.SetGame();

        FindObjectOfType<GameManager>().gemCreated = false;

        if (geneticAlgorithm.generation >= 2)
        {
            if (roomChoice != (int)(geneticAlgorithm.population[0].fitness % 4))
            {
                roomChoice = (int)(geneticAlgorithm.population[0].fitness % 4);
                // Debug.Log("Using genetic algo: " + roomChoice);
            }
            else
            {
                roomChoice++;
                roomChoice %= 4;
                // Debug.Log("Same as last time: " + roomChoice);
            }
        }
        else
        {
            roomChoice++;
            roomChoice %= 4;

            // Debug.Log("Not genetic algo: " + roomChoice);
        }
        // roomChoice = Random.Range(0, 4);

        for (int x = 0; x < maps[roomChoice].width; x++)
        {
            for (int y = 0; y < maps[roomChoice].height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColour = maps[roomChoice].GetPixel(x, y);

        // Ignore if transparent
        if (pixelColour.a == 0)
        {
            return;
        }


        foreach (ColourToPrefab colourMapping in colourMappings)
        {

            if (colourMapping.color.Equals(pixelColour))
            {
                Vector2 position = new Vector2(x - 13.5f, y - 6.5f);

                if (Color.black.Equals(pixelColour))
                { 
                    if (Random.value < obstacleChance)
                    {
                        GameObject obstacle = Instantiate(colourMapping.prefab, position, Quaternion.identity, transform);

                        allChildrenObjects.Add(obstacle);
                      
                    }
                }
                
                if (Color.red.Equals(pixelColour)) {
                    if (Random.value < heartChance)
                    {
                        GameObject heart = Instantiate(colourMapping.prefab, position, Quaternion.identity, transform);

                        allChildrenObjects.Add(heart);
                    }
                }

                if (Color.blue.Equals(pixelColour))
                {
                    if (Random.value < enemySpawnerChance)
                    {
                        GameObject enemySpawner = Instantiate(colourMapping.prefab, position, Quaternion.identity, transform);
                        enemySpawner.GetComponent<EnemySpawner>().enemyHealth = enemySetHealth;
                        enemySpawner.GetComponent<EnemySpawner>().enemyPotion = enemySetPotionChance;
                       
                        allChildrenObjects.Add(enemySpawner);
                    }
                }
            }
        }
    }

}
