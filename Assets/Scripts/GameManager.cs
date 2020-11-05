using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject levelGem;
    public TextMeshProUGUI roomText;
    public TextMeshProUGUI killText;
    public QuestionnaireManager questionnaireManager;
    public DataManager dataManager;
    public ParameterManager parameterManager;
    public GameOver gameOver;
    public GameObject dataButton;
    public AudioManager audioManager;
    //public SaveManager saveManager;
    //public SaveManager.SaveFile save;

    public float[] targetDNA;

    public int enemiesNeeded = 3;
    public int enemiesKilled;

    public int roomsNeeded = 3;
    public int roomsCleared = 0;

    public bool gemCreated;

    // (25 - |target gene - individual gene|) * 10

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        parameterManager.Initialize();
        enemiesKilled = 0;
        roomsCleared = 0;
        killText.text = "ENEMIES KILLED: " + enemiesKilled + "/" + enemiesNeeded;
        roomText.text = "ROOMS CLEARED: " + roomsCleared;
        questionnaireManager = GameObject.FindGameObjectWithTag("Questionnaire").GetComponent<QuestionnaireManager>();//FindObjectOfType<QuestionnaireManager>();
        dataManager = FindObjectOfType<DataManager>();
        gameOver = FindObjectOfType<GameOver>();
        QuestionnaireOff();
        DataOff();
        DataButtonOff();
        GameOverOff();
        audioManager.ChangeMusic("MainTheme");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (enemiesKilled >= enemiesNeeded && !gemCreated)
        {
            Instantiate(levelGem, new Vector2(0, 0), Quaternion.identity);
            gemCreated = true;
        }

    }

    public void SetTargetDNA()
    {
        targetDNA = new float[8];
        questionnaireManager.SetTarget();
        //parameterManager.Initialize();
        //Debug.Log(parameterManager.setHealth);
        //Debug.Log(questionnaireManager.target[0]);
        //Debug.Log(targetDNA[0]);
        targetDNA[0] = parameterManager.setHealth * questionnaireManager.target[0];
        targetDNA[1] = parameterManager.setDamage * questionnaireManager.target[1];
        targetDNA[2] = parameterManager.setHeal * questionnaireManager.target[2];
        targetDNA[3] = parameterManager.setPotionSpawn * questionnaireManager.target[3];
        targetDNA[4] = parameterManager.setRoomLength * questionnaireManager.target[4];
        targetDNA[5] = parameterManager.setEnemyHealth * questionnaireManager.target[5];
        targetDNA[6] = parameterManager.setEnemyDamage * questionnaireManager.target[6];
        targetDNA[7] = parameterManager.setEnemySpawner * questionnaireManager.target[7];
        QuestionnaireOff();
    }

    public void GameOverOn()
    {
        audioManager.ChangeMusic("GameOver");
        gameOver.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOverOff()
    {
        audioManager.ChangeMusic("Menu");
        gameOver.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuestionnaireOn()
    {
        audioManager.ChangeMusic("Pause");
        questionnaireManager.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuestionnaireOff()
    {
        audioManager.ChangeMusic("MainTheme");
        questionnaireManager.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void DataButtonOn()
    {
        audioManager.ChangeMusic("Pause");
        dataButton.SetActive(true);
    }


    public void DataButtonOff()
    {
        audioManager.ChangeMusic("MainTheme");
        dataButton.SetActive(false);
    }

    public void DataOn()
    {
        dataManager.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DataOff()
    {
        dataManager.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ResetKills()
    {
        enemiesKilled = 0;
        killText.text = "ENEMIES KILLED: " + enemiesKilled + "/" + enemiesNeeded;
    }

    public void IncrementKillCounter()
    {
        if (enemiesKilled < enemiesNeeded)
        {
            enemiesKilled++;
            killText.text = "ENEMIES KILLED: " + enemiesKilled + "/" + enemiesNeeded;
        }
    }

    public void IncrementRoomCleared()
    {
        roomsCleared++;
        roomText.text = "ROOMS CLEARED: " + roomsCleared;
    }
}