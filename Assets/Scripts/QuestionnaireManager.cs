using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionnaireManager : MonoBehaviour
{
    public GameObject tmPlayerHealth;
    public GameObject jrPlayerHealth;
    public GameObject nePlayerHealth;
    public GameObject tmPlayerAttack;
    public GameObject jrPlayerAttack;
    public GameObject nePlayerAttack;
    public GameObject tmPlayerHeal;
    public GameObject jrPlayerHeal;
    public GameObject nePlayerHeal;
    public GameObject tmBuffDuration;
    public GameObject jrBuffDuration;
    public GameObject neBuffDuration;
    public GameObject tmRoomLength;
    public GameObject jrRoomLength;
    public GameObject neRoomLength;
    public GameObject tmEnemyHealth;
    public GameObject jrEnemyHealth;
    public GameObject neEnemyHealth;
    public GameObject tmEnemyAttack;
    public GameObject jrEnemyAttack;
    public GameObject neEnemyAttack;
    public GameObject tmEnemySpawn;
    public GameObject jrEnemySpawn;
    public GameObject neEnemySpawn;


    public float[] target;
    public float playerHealthModifier = 1f;
    public float playerAttackModifier = 1f;
    public float playerHealModifier = 1f;
    public float buffDurationModifier = 1f;
    public int killsNeededModifier = 1;
    public float enemyHealthModifier = 1f;
    public float enemyAttackModifier = 1f;
    public float enemySpawnModifier = 1f;

    public void SetTarget()
    {
        target = new float[8]; 
        target[0] = playerHealthModifier;
        target[1] = playerAttackModifier;
        target[2] = playerHealModifier;
        target[3] = buffDurationModifier;
        target[4] = killsNeededModifier;
        target[5] = enemyHealthModifier;
        target[6] = enemyAttackModifier;
        target[7] = enemySpawnModifier;
    }

    public void ResetQuestionnaire()
    {
        playerHealthModifier = 1f;
        playerAttackModifier = 1f;
        playerHealModifier = 1f;
        buffDurationModifier = 1f;
        killsNeededModifier = 1;
        enemyHealthModifier = 1f;
        enemyAttackModifier = 1f;
        enemySpawnModifier = 1f;
    }

    public void TooMuchPlayerHealth()
    {

        if (tmPlayerHealth.GetComponent<Toggle>().isOn == true) {
            playerHealthModifier = 0.75f;
        }
    }
    public void JustRightPlayerHealth()
    {
        if (jrPlayerHealth.GetComponent<Toggle>().isOn == true)
        {
            playerHealthModifier = 1f;
        }
    }
    public void NotEnoughPlayerHealth()
    {
        if (nePlayerHealth.GetComponent<Toggle>().isOn == true)
        {
            playerHealthModifier = 1.25f;
        }
    }

    public void TooMuchPlayerAttack()
    {
        if (tmPlayerAttack.GetComponent<Toggle>().isOn == true)
        {
            playerAttackModifier = 0.75f;
        }
    }
    public void JustRightPlayerAttack()
    {
        if (jrPlayerAttack.GetComponent<Toggle>().isOn == true)
        {
            playerAttackModifier = 1f;
        }
    }
    public void NotEnoughPlayerAttack()
    {
        if (nePlayerAttack.GetComponent<Toggle>().isOn == true)
        {
            playerAttackModifier = 1.25f;
        }
    }

    public void TooMuchPlayerHealed()
    {
        if (tmPlayerHeal.GetComponent<Toggle>().isOn == true)
        {
            playerHealModifier = 0.75f;
        }
    }
    public void JustRightPlayerHealed()
    {
        if (jrPlayerHeal.GetComponent<Toggle>().isOn == true)
        {
            playerHealModifier = 1f;
        }
    }
    public void NotEnoughPlayerHealed()
    {
        if (nePlayerHeal.GetComponent<Toggle>().isOn == true)
        {
            playerHealModifier = 1.25f;
        }
    }
    
    public void TooMuchBuffDuration()
    {
        if (tmBuffDuration.GetComponent<Toggle>().isOn == true)
        {
            buffDurationModifier = 0.75f;
        }
    }
    public void JustRightBuffDuration()
    {
        if (jrBuffDuration.GetComponent<Toggle>().isOn == true)
        {
            buffDurationModifier = 1f;
        }
    }
    public void NotEnoughBuffDuration()
    {
        if (neBuffDuration.GetComponent<Toggle>().isOn == true)
        {
            buffDurationModifier = 1.25f;
        }
    }

    public void TooMuchRoomLength()
    {
        if (tmRoomLength.GetComponent<Toggle>().isOn == true)
        {
            killsNeededModifier = 1;
        }
    }

    public void JustRightRoomLength()
    {
        if (jrRoomLength.GetComponent<Toggle>().isOn == true)
        {
            killsNeededModifier = 1;
        }
    }
    public void NotEnoughRoomLength()
    {
        if (neRoomLength.GetComponent<Toggle>().isOn == true)
        {
            killsNeededModifier = 2;
        }
    }

    public void TooMuchEnemyHealth()
    {
        if (tmEnemyHealth.GetComponent<Toggle>().isOn == true)
        {
            enemyHealthModifier = 0.75f;
        }
    }

    public void JustRightEnemyHealth()
    {
        if (jrEnemyHealth.GetComponent<Toggle>().isOn == true)
        {
            enemyHealthModifier = 1f;
        }
    }
    public void NotEnoughEnemyHealth()
    {
        if (neEnemyHealth.GetComponent<Toggle>().isOn == true)
        {
            enemyHealthModifier = 1.25f;
        }
    }

    public void TooMuchEnemyAttack()
    {
        if (tmEnemyAttack.GetComponent<Toggle>().isOn == true)
        {
            enemyAttackModifier = 0.75f;
        }
    }

    public void JustRightEnemyAttack()
    {
        if (jrEnemyAttack.GetComponent<Toggle>().isOn == true)
        {
            enemyAttackModifier = 1f;
        }
    }
    public void NotEnoughEnemyAttack()
    {
        if (neEnemyAttack.GetComponent<Toggle>().isOn == true)
        {
            enemyAttackModifier = 1.25f;
        }
    }

    public void TooMuchEnemySpawn()
    {
        if (tmEnemySpawn.GetComponent<Toggle>().isOn == true)
        {
            enemySpawnModifier = 0.75f;
        }
    }

    public void JustRightEnemySpawn()
    {
        if (jrEnemySpawn.GetComponent<Toggle>().isOn == true)
        {
            enemySpawnModifier = 1f;
        }
    }
    public void NotEnoughEnemySpawn()
    {
        if (neEnemySpawn.GetComponent<Toggle>().isOn == true)
        {
            enemySpawnModifier = 1.25f;
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
