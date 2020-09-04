using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        SaveSystem.Init();

        SaveFile saveFile = new SaveFile
        {
            playerHealth = 2000,
            enemyHealth = 100,
            playerAttackDmg = 10,
            enemyAttackDmg = 10,
            enemySpawn = 0.2f,
            healthSpawn = 0.2f
        };

        string json = JsonUtility.ToJson(saveFile);
        Debug.Log(json);

        SaveSystem.Save(json);
//        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            string saveString = SaveSystem.Load();
            if (saveString != null) {
                SaveFile saveInfo = JsonUtility.FromJson<SaveFile>(saveString);

                Debug.Log(saveInfo.playerAttackDmg);
            }
        }
    }

    public class SaveFile
    {
        public int playerHealth;
        public int enemyHealth;
        public int playerAttackDmg;
        public int enemyAttackDmg;

        public float enemySpawn;
        public float healthSpawn;

    }
}
