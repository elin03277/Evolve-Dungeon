using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public ParameterManager parameterManager;

    // Start is called before the first frame update
    void Awake()
    {
       SaveSystem.Initialize();

       //SaveFile saveFile = new SaveFile
       // {
       //     playerAttack = 10f,
       //     playerHealth = 10f,
       //     playerHeal = 10f,
       //     potionChance = 10f,
       //     roomLength = 10,
       //     enemyHealth = 10f,
       //     enemyAttack = 10f,
       //     enemyChance = 10f,
       //     fitness = 10f
       //};
       // Save(saveFile);
        // File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public void Save(GeneticAlgorithm.DNA saveDNA, int generation)// SaveFile gameSave)
    {
        SaveFile gameSave = new SaveFile
        {
            playerHealth = saveDNA.genes[0],
            playerAttack = saveDNA.genes[1],
            playerHeal = saveDNA.genes[2],
            potionChance = saveDNA.genes[3],
            roomLength = saveDNA.genes[4],
            enemyHealth = saveDNA.genes[5],
            enemyAttack = saveDNA.genes[6],
            enemyChance = saveDNA.genes[7],
            fitness = saveDNA.fitness,
            generation = generation
        };

        string json = JsonUtility.ToJson(gameSave);
        //Debug.Log(json);

        SaveSystem.Save(json);
        // File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public void LoadSave()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveFile saveInfo = JsonUtility.FromJson<SaveFile>(saveString);
            // Debug.Log(saveInfo);
            parameterManager.setHealth = saveInfo.playerHealth;
            parameterManager.setDamage = saveInfo.playerAttack;
            parameterManager.setHeal = saveInfo.playerHeal;
            parameterManager.setPotionSpawn = saveInfo.potionChance;
            parameterManager.setRoomLength = (int)saveInfo.roomLength;
            // Debug.Log(parameterManager.setRoomLength);
            parameterManager.setEnemyHealth = saveInfo.enemyHealth;
            parameterManager.setEnemyDamage = saveInfo.enemyAttack;
            parameterManager.setEnemySpawner = saveInfo.enemyChance;
        }
    }

    public class SaveFile
    {
        public float playerHealth;
        public float playerAttack;
        public float playerHeal;
        public float potionChance;
        public float roomLength;
        public float enemyHealth;
        public float enemyAttack;
        public float enemyChance;
        public float fitness;
        public float generation;
    }
}