using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class PlayerTests
    {

        [UnityTest]
        public IEnumerator QuestionnaireButtonTest()
        {
            SceneManager.LoadScene("EvolveDungeon");
            yield return null;

            GameObject canvas = GameObject.Find("Canvas").transform.gameObject;
            GameObject canvas2 = GameObject.Find("Canvas").transform.gameObject;
            GameObject questionnaire = canvas2.transform.Find("Questionnaire").transform.gameObject;
            //GameObject panel = canvas.transform.Find("MainMenu").gameObject;
            Button questionnaireButton = canvas.transform.Find("Button").GetComponent<Button>();
            Button submitButton = questionnaire.transform.Find("SubmitButton").GetComponent<Button>();
            yield return null;

            Assert.NotNull(canvas);
            //Assert.NotNull(panel);
            Assert.NotNull(questionnaireButton);
            Assert.NotNull(submitButton);
            yield return null;

            questionnaireButton.onClick.Invoke();
            yield return null;

            Assert.AreEqual(0, Time.timeScale);
            yield return new WaitForSeconds(20);

            //submitButton.onClick.Invoke();
            //yield return null;

            //Assert.AreEqual(1, Time.timeScale);
            //yield return new WaitForSeconds(20);


            // GameObject pl = MonoBehaviour.Instantiate((GameObject)Resources.Load("Player"), new Vector3(0, 0, 0), Quaternion.identity);
            // Player player = pl.GetComponent<Player>();
            // player.TakeDamage(20f);
            // yield return null;

            // Assert.AreEqual(80, player.currentHealth);
        }

        [UnityTest]
        public IEnumerator EnemyTakeDamageTest()
        {
            SceneManager.LoadScene("EvolveDungeon");
            yield return null;

            //GameObject enemyObject = MonoBehaviour.Instantiate((GameObject)Resources.Load("Enemy"), new Vector3(0, 1, 0), Quaternion.identity);
            //Enemy enemy = enemyObject.GetComponent<Enemy>();          
            Enemy enemy = new Enemy();
            yield return null;

            enemy.currentEnemyHealth = 100f;
            Debug.Log("Enemy health before damage: " + enemy.currentEnemyHealth);
            enemy.TakeEnemyDamage(20f);
            yield return null;

            Debug.Log("Enemy health after damage: " + enemy.currentEnemyHealth);

            Assert.AreEqual(80f, enemy.currentEnemyHealth);
            //yield return new WaitForSeconds(20);
        }


        [UnityTest]
        public IEnumerator PlayerDamageTest()
        {
            SceneManager.LoadScene("EvolveDungeon");
            yield return null;

            HealthBar healthBar = new HealthBar();
            yield return null;

            Player player = new Player();
            yield return null;
            player.healthBar = healthBar;
            player.currentHealth = 100f;
            Debug.Log("Player health before damage: " + player.currentHealth);
            player.TakeDamage(50f);
            yield return null;
            
            Debug.Log("Player health after damage: " + player.currentHealth);

            Assert.AreEqual(50f, player.currentHealth);
            player.Heal(50f);
            yield return null;

            Debug.Log("Player health after heal: " + player.currentHealth);

            Assert.AreEqual(100f, player.currentHealth);
        }
    }
}
